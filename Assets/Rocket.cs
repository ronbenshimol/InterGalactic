using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float mainThrust = 25f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        thrust();
        rotate();        
    }

    void rotate()
    {
        
        rigidbody.freezeRotation = true; // take manual control over rotation

        
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)){
            
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidbody.freezeRotation = false; // resume physics control over toration
    }
    void thrust()
    {
        if (Input.GetKey(KeyCode.Space)) // thrust while rotating
        {
            //float thrustForce = mainThrust * Time.deltaTime;

            rigidbody.AddRelativeForce(Vector3.up * mainThrust);
            if(!audioSource.isPlaying) // avoid multiple sounds
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("ok");
                break;
            
            default:
                print("dead");
                break;
        }
    }
}
