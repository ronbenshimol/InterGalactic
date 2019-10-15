using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float mainThrust;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;
    
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;
    

    bool isTransitioning = false;

    [SerializeField] bool collisionsDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isTransitioning)
        {
            respondToThrustInput();
            respondToRotateInput();
            RespondToRestartInput();
            
        }

        if(Debug.isDebugBuild){
            RespondToDebugKeys();
        }
    }
    private void RespondToRestartInput()
    {
       if (Input.GetKey(KeyCode.R))
        {
            Invoke("LoadFirstLevel", levelLoadDelay); 
        }
        //LoadFirstLevel
    }

    private void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        } else if(Input.GetKeyDown(KeyCode.C)){
            collisionsDisabled = !collisionsDisabled;
        }
    }

    void respondToRotateInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateManually(rcsThrust * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
           RotateManually(-rcsThrust * Time.deltaTime);
        }
       
    }
    private void RotateManually(float rotationThisFrame)
    {
        rigidbody.freezeRotation = true; // take manual control over rotation
        transform.Rotate(Vector3.forward * rotationThisFrame);
        rigidbody.freezeRotation = false; // resume physics control over toration
    }
    void respondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space)) // thrust while rotating
        {
            applyThrust();
        }
        else
        {
            StopApplyingThrust();
        }
    }

    private void StopApplyingThrust()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void applyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying) // avoid multiple sounds
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionsDisabled)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }
private void StartDeathSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", levelLoadDelay);
    }
    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex % SceneManager.sceneCountInBuildSettings);
        
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
}
