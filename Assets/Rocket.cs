﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        processInput();        
    }

    void processInput()
    {
        if (Input.GetKey(KeyCode.Space)) // thrust while rotating
        {
            rigidbody.AddRelativeForce(Vector3.up);
            if(!audioSource.isPlaying) // avoid multiple sounds
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward);
        }
        else if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward);
        }
    }
}
