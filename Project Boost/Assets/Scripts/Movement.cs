using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] AudioClip mainEngine;
    [SerializeField] float mainThrust = 2;
    [SerializeField] float rotationThrust = 2;

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
        ProcessRotation();
        ProcessThrust();
    }
    //! to reduce choppiness of the game, using late update, to apply transform after physics based computations are done

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            rbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rbody.AddRelativeForce(0, -1, 0);
        }
        else
        {
            audioSource.Stop();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rbody.freezeRotation = true; //* this turns off the physics system based rotation on custom input
        transform.Rotate(-Vector3.forward * rotationThisFrame * Time.deltaTime);
        rbody.freezeRotation = false; //* enable the physics system

        //! apply changes to the GUI based rigidbody on drag (to enable less momentum carried by the object)
        //! using gravity from the physics system controls, gravity can be applied in any axis (even to simulate wind)
    }
}
