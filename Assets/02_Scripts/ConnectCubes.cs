using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectCubes : MonoBehaviour
{
    public Transform objectToRotate; // Reference to the object you want to rotate
    public LineRenderer indicatorLine; // Reference to the line indicator
    public float circleRadius = 1f; // Radius of the circle
    public float lineLengthMultiplier = 1f; // Multiplier to adjust line length

    void Update()
    {
        // Check if the object to rotate is assigned
        if (objectToRotate == null)
        {
            Debug.LogError("Object to rotate is not assigned!");
            return;
        }

        // Check if the indicator line is assigned
        if (indicatorLine == null)
        {
            Debug.LogError("Indicator line is not assigned!");
            return;
        }

        // Get the rotation of the object along the y-axis
        float rotationY = objectToRotate.rotation.eulerAngles.y;

        // Calculate the position of the indicator line on the circle based on the rotation
        Vector3 linePosition = new Vector3(Mathf.Sin(rotationY * Mathf.Deg2Rad) * circleRadius * lineLengthMultiplier, 0f, Mathf.Cos(rotationY * Mathf.Deg2Rad) * circleRadius * lineLengthMultiplier);

        // Update the line indicator's positions
        indicatorLine.SetPosition(0, Vector3.zero);
        indicatorLine.SetPosition(1, linePosition);
    }
}