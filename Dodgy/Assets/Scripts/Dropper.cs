using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] float dropTimer = 3;
    MeshRenderer componentRenderer;
    Rigidbody rigidBody;
    void Start()
    {
        componentRenderer = GetComponent<MeshRenderer>();
        rigidBody = GetComponent<Rigidbody>();

        componentRenderer.enabled = false;
        rigidBody.useGravity = false;
    }
    void Update()
    {
        if (Time.time > dropTimer && !componentRenderer.enabled && !rigidBody.useGravity)
        {
            componentRenderer.enabled = true;
            rigidBody.useGravity = true;
        }
    }
}
