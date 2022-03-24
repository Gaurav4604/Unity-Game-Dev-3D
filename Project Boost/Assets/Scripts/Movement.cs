using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] AudioClip mainEngine;
    [SerializeField] float mainThrust = 2;
    [SerializeField] float rotationThrust = 2;

    [SerializeField] ParticleSystem mainBooster, rightBooster, leftBooster;

    Rigidbody rbody;
    AudioSource audioSource;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
    }
    //! late update is used to compute physics based calculations after update is called
    void LateUpdate()
    {
        ProcessRotation();

    }
    //! to reduce choppiness of the game, using late update, to apply transform after physics based computations are done

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            rbody.AddRelativeForce(Vector3.up * mainThrust);
            // audio cue for thrust
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(mainEngine);
            // visual cue for thrust
            if (!mainBooster.isPlaying)
                mainBooster.Play();
        }
        else
        {
            audioSource.Stop();
            mainBooster.Stop();

        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(rotationThrust);
            //* push left to go right
            if (!leftBooster.isPlaying)
                leftBooster.Play();
        }
        //* push right to go left
        else if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-rotationThrust);
            if (!rightBooster.isPlaying)
                rightBooster.Play();
        }
        else
        {
            rightBooster.Stop();
            leftBooster.Stop();
            rbody.angularVelocity = Vector3.zero;

        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        // rbody.freezeRotation = true; //* this turns off the physics system based rotation on custom input
        rbody.AddRelativeTorque(-Vector3.forward * rotationThisFrame);
        // rbody.freezeRotation = false; //* enable the physics system

        //! apply changes to the GUI based rigidbody on drag (to enable less momentum carried by the object)
        //! using gravity from the physics system controls, gravity can be applied in any axis (even to simulate wind)
    }
}
