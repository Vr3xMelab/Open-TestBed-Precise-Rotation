////////////////////////////////////////////////////////////////////////////////////////////////////////
//Authors : Mine Dastan, Michele Fiorentino
// V3
//Created On : 3/7/2024 
//Copy Rights : MELAB, DMMM, Politecnico di Bari
//Description : Setup image rotation
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageRotator : MonoBehaviour
{
    public RotationTracker rotationTrackerScript; // Reference to the v2Rotationtracker script
    public Transform imageToRotate; // Reference to the image to rotate

    private bool allTargetsCompared = false; // Flag to indicate if all targets have been compared

    void Update()
    {
        // Check if all targets have been compared
        if (!allTargetsCompared)
        {
            // Check if rotation tracker script is assigned and has targets
            if (rotationTrackerScript != null && rotationTrackerScript.targetPrefabs.Count > 0)
            {
                // Check if current target index is within the valid range
                if (rotationTrackerScript.kg_currentTargetIndex >= 0 && rotationTrackerScript.kg_currentTargetIndex < rotationTrackerScript.targetPrefabs.Count)
                {
                    // Get the current target rotation from the rotation tracker script
                    GameObject currentTarget = rotationTrackerScript.targetPrefabs[rotationTrackerScript.kg_currentTargetIndex];
                    Quaternion targetRotation = currentTarget.transform.rotation;

                    // Set only the Y rotation of the image
                    Vector3 eulerRotation = imageToRotate.rotation.eulerAngles;
                    eulerRotation.y = targetRotation.eulerAngles.y; // Set only Y rotation
                    imageToRotate.rotation = Quaternion.Euler(eulerRotation);
                }
               
            }
            else
            {
                // If there are no more targets, deactivate the image rotation
                imageToRotate.gameObject.SetActive(false);
                allTargetsCompared = true; // Set flag to indicate all targets have been compared
            }

            // Check if the current target index is greater than or equal to the count of target prefabs
            if (rotationTrackerScript != null && rotationTrackerScript.kg_currentTargetIndex >= rotationTrackerScript.targetPrefabs.Count)
            {
                // Deactivate the image rotation
                imageToRotate.gameObject.SetActive(false);
                allTargetsCompared = true; // Set flag to indicate all targets have been compared
            }
        }
    }
}