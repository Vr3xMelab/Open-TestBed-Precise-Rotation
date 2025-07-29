////////////////////////////////////////////////////////////////////////////////////////////////////////
//Authors : Mine Dastan, Michele Fiorentino
// V3
//Created On : 3/7/2024 
//Copy Rights : MELAB, DMMM, Politecnico di Bari
//Description : Offset contreller
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOffset : MonoBehaviour
{
    public Transform target; // The target object to follow
    public float xOffset = 0f; // The offset in the x-axis
    public float yOffset = 0f; // The offset in the y-axis
    public float zOffset = -0.15f; // The offset in the z-axis

    private Quaternion originalRotation;
    private Vector3 originalPosition; // Store the original position

    void Start()
    {
        // Store the original position and rotation of the following object
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Create the offset vector in world space
            Vector3 offset = new Vector3(xOffset, yOffset, zOffset);

            // Calculate the new position in world space using the target's position and the offset
            Vector3 newPosition = target.position + offset;

            // Apply the new position
            transform.position = newPosition;

            // Maintain the original rotation
            transform.rotation = originalRotation;
        }
    }

    void OnDestroy()
    {
        // Restore original position on destruction (optional, but good practice)
        transform.position = originalPosition;
    }
}