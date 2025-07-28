using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeRotation : MonoBehaviour
{
    public float rotationSpeed = 50f;
    private bool isRotating = false;

    void Update()
    {
        // Check for input from the Enter button
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            // Toggle rotation
            isRotating = !isRotating;

            // If rotating, start rotating the cube
            if (isRotating)
            {
                Debug.Log("Cube rotation started.");
                StartRotating();
            }
            // If not rotating, stop rotating the cube
            else
            {
                Debug.Log("Cube rotation stopped.");
                StopRotating();
            }
        }
    }

    // Rotate the cube continuously
    void StartRotating()
    {
        isRotating = true;
    }

    // Stop rotating the cube
    void StopRotating()
    {
        isRotating = false;
    }

    void FixedUpdate()
    {
        // Rotate the cube if isRotating is true
        if (isRotating)
        {
            float rotateAmount = rotationSpeed * Time.fixedDeltaTime;
            transform.Rotate(Vector3.up, rotateAmount);
        }
    }
}