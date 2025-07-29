////////////////////////////////////////////////////////////////////////////////////////////////////////
//Authors : Mine Dastan, Michele Fiorentino
// V3
//Created On : 3/7/2024 
//Copy Rights : MELAB, DMMM, Politecnico di Bari
//Description : Zoom image rotation
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomFollower : MonoBehaviour
{
    public RotationTracker rotationTracker; // Reference to the v2Rotationtracker script
    public string childName; // The name of the child to follow
    public Vector3 positionOffset; // The offset from the child's position
    public GameObject zoomMagnifier; // Reference to the zoom magnifier object

    void Update()
    {
        FollowTargetChild();
    }

    void FollowTargetChild()
    {
        // Get the current target from the rotation tracker
        GameObject currentTarget = rotationTracker.CurrentTarget;

        // If there is a current target
        if (currentTarget != null)
        {
            // Find the child object with the specified name
            Transform childTransform = currentTarget.transform.Find(childName);

            if (childTransform != null)
            {
                // Set this object's position to the child's position plus the offset
                transform.position = childTransform.position + positionOffset;

                // Get the current rotation of this object
                Quaternion currentRotation = transform.rotation;

                // Get the target child's rotation
                Quaternion targetRotation = childTransform.rotation;

                // Create a new rotation with the current X and Z and target Y
                Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, targetRotation.eulerAngles.y, currentRotation.eulerAngles.z);

                // Apply the new rotation
                transform.rotation = newRotation;
            }
            else
            {
                Debug.LogWarning("Child with name " + childName + " not found in target " + currentTarget.name);
            }
        }
    }

    void OnDisable()
    {
        if (zoomMagnifier != null)
        {
            zoomMagnifier.SetActive(false);
        }
    }
}