using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, -100, 0); // Degrees per second

    // Update is called once per frame
    void Update()
    {
        // Rotate based on rotationSpeed and time
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
