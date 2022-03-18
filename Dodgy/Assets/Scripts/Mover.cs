using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] int speed = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
    }
    void playerMovement()
    {
        float translateX = speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        float translateZ = speed * Input.GetAxis("Vertical") * Time.deltaTime;

        transform.Translate(translateX, 0, translateZ);
    }
}
