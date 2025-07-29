////////////////////////////////////////////////////////////////////////////////////////////////////////
//Authors : Mine Dastan, Michele Fiorentino
// V3
//Created On : 3/7/2024 
//Copy Rights : MELAB, DMMM, Politecnico di Bari
//Description : Linear rotation
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// future have different easings
// https://forum.unity.com/threads/logarithmic-interpolation.354344/
//https://github.com/lordofduct/spacepuppy-unity-framework/blob/master/SpacepuppyBase/Tween/Easing.cs


public class nonLinearCol : MonoBehaviour
{
    public GameObject topCollimator;
    public GameObject bottomCollimator;

    public GameObject toolObject;
    public RotationTracker rotationTracker; // Reference to the v2rotationtracker script

    public float multiplier = 0.01f; // Multiplier to scale the angle difference to distance between collimators
    public float maxAngle = 45f; // Maximum angle difference

    public Material outboundsMat; // Material for out of bounds collimators
    public Material normalMat;    // Material for normal collimators

    private void Update()
    {
        GameObject currentTarget = rotationTracker.CurrentTarget; // Get the current target from the rotationTracker

        if (currentTarget != null)
        {
            // Calculate angle difference only around the y-axis
            float angleDifference = Mathf.Abs(toolObject.transform.eulerAngles.y - currentTarget.transform.eulerAngles.y);

            // Clamp the angle difference to the maximum angle
            angleDifference = Mathf.Min(angleDifference, maxAngle);

            // Calculate the distance between collimators based on the clamped angular difference
            float distance = angleDifference * multiplier;

            // Update collimator positions
            Vector3 topCollimatorPos = new Vector3(0f, distance / 2f, 0f);
            Vector3 bottomCollimatorPos = new Vector3(0f, -distance / 2f, 0f);

            // Update collimator positions
            topCollimator.transform.localPosition = topCollimatorPos;
            bottomCollimator.transform.localPosition = bottomCollimatorPos;

            // Activate collimators and set materials
            topCollimator.SetActive(true);
            bottomCollimator.SetActive(true);
            topCollimator.GetComponent<MeshRenderer>().material = normalMat;
            bottomCollimator.GetComponent<MeshRenderer>().material = normalMat;
        }
        else
        {
            // No current target, deactivate collimators
            topCollimator.SetActive(false);
            bottomCollimator.SetActive(false);
        }
    }
}