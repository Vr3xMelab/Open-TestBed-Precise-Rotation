////////////////////////////////////////////////////////////////////////////////////////////////////////
//Authors : Mine Dastan, Michele Fiorentino
// V3
//Created On : 3/7/2024 
//Copy Rights : MELAB, DMMM, Politecnico di Bari
//Description : Setup target trigger
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    public RotationTracker rotationTracker; // Reference to the v2Rotationtracker script
    public string childName; // The name of the child to follow

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
                // Set this object's position to the child's position
                transform.position = childTransform.position;

                // Optionally, if you want to match the rotation as well, uncomment the line below
                // transform.rotation = childTransform.rotation;
            }
            else
            {
                Debug.LogWarning("Child with name " + childName + " not found in target " + currentTarget.name);
            }
        }
    }
}