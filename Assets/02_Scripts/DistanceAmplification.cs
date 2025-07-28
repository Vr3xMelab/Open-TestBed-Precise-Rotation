using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAmplification : MonoBehaviour
{
    public GameObject toolObject; // The object whose rotation affects distance adjustment
    public GameObject objectA; // First object to adjust distance from
    public GameObject objectB; // Second object to adjust distance to
    public v2Rotationtracker rotationTracker; // Reference to the rotation tracker script

    public float maxDistance = 20.0f; // Maximum distance to clamp
    public float minDistance = 0.0f; // Minimum distance to clamp

    private Quaternion lastToolRotation;

    void Start()
    {
        if (toolObject != null)
        {
            lastToolRotation = toolObject.transform.rotation;
        }
    }

    void Update()
    {
        if (toolObject != null && objectA != null && objectB != null && rotationTracker != null)
        {
            Quaternion currentToolRotation = toolObject.transform.rotation;
            float currentAngleError = ComputeAngleError();

            AdjustDistance(currentAngleError);

            lastToolRotation = currentToolRotation;
        }
    }

    float ComputeAngleError()
    {
        float totalAngleError = 0;
        int targetCount = 0;

        foreach (var target in rotationTracker.targetPrefabs)
        {
            float angleError = Quaternion.Angle(toolObject.transform.rotation, target.transform.rotation);
            totalAngleError += angleError;
            targetCount++;
        }

        float averageAngleError = targetCount > 0 ? totalAngleError / targetCount : 0;
        return averageAngleError;
    }

    void AdjustDistance(float currentAngleError)
    {
        // Calculate the current distance between objectA and objectB
        float currentDistance = Vector3.Distance(objectA.transform.position, objectB.transform.position);

        // Adjust the distance based on the angle error
        float adjustment = currentAngleError * Time.deltaTime;
        float newDistance = currentDistance + adjustment;

        // Clamp the new distance to a minimum and maximum value to avoid extreme positions
        newDistance = Mathf.Clamp(newDistance, minDistance, maxDistance);

        // Set the new position of objectB relative to objectA
        Vector3 direction = (objectB.transform.position - objectA.transform.position).normalized;
        objectB.transform.position = objectA.transform.position + direction * newDistance;
    }
}