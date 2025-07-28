using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class v2progressbar : MonoBehaviour
{
    public v2Rotationtracker rotationTracker; // Reference to the RotationTracker script
    public GameObject objectToRotate;
    public Image image1; // Reference to the first UI image
    public Image image2; // Reference to the second UI image
    public float maxRotationAngle = 360f; // Maximum rotation angle needed to fill the progress bar

    private void Update()
    {
        // Check if rotation tracker reference is set
        if (rotationTracker == null)
        {
            Debug.LogError("Rotation tracker reference not set!");
            return;
        }

        // Get the current target rotation normalized to 0 degrees
        float targetRotationY = rotationTracker.g_currentTarget.transform.rotation.eulerAngles.y;

        // Get the current object rotation
        float currentRotationY = objectToRotate.transform.rotation.eulerAngles.y;

        // Calculate the shortest angle between the object's rotation and the target rotation
        float currentAngle = Mathf.DeltaAngle(currentRotationY, targetRotationY);

        // Calculate the progress percentage based on the absolute value of the current angle
        float progress = Mathf.Clamp01(Mathf.Abs(currentAngle) / maxRotationAngle);

        // Determine if the object's angle is less than or greater than the target angle
        bool isObjectAngleLess = currentAngle > 0;

        // Update the fill amount and active state of the images
        if (isObjectAngleLess)
        {
            image1.fillAmount = progress;
            image2.fillAmount = 0f;
            image1.gameObject.SetActive(true);
            image2.gameObject.SetActive(false);
        }
        else
        {
            image1.fillAmount = 0f;
            image2.fillAmount = progress;
            image1.gameObject.SetActive(false);
            image2.gameObject.SetActive(true);
        }

        // Deactivate images if the object angle matches the target angle
        if (Mathf.Approximately(currentAngle, 0f))
        {
            image1.gameObject.SetActive(false);
            image2.gameObject.SetActive(false);
        }
    }
}