using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectOnRotation : MonoBehaviour
{
    public GameObject objectY; // Assign object Y in the Inspector
    public float activationTime = 10f; // Time object Y remains active after activation
    public float fadeOutTime = 1f; // Time it takes for object Y to fade out after deactivation

    private Quaternion previousRotation;
    private bool isManipulating;
    private float manipulationTimer;
    private Color originalColorY;
    private Material objectYMaterial;
    private Coroutine fadeOutCoroutine;

    private void Start()
    {
        previousRotation = transform.rotation;
        if (objectY != null)
        {
            Renderer renderer = objectY.GetComponent<Renderer>();
            if (renderer != null)
            {
                objectYMaterial = renderer.material;
                originalColorY = objectYMaterial.color;
            }
        }
    }

    private void Update()
    {
        // Check if object X is being manipulated
        if (IsManipulating())
        {
            isManipulating = true;
            manipulationTimer = 0f;

            // If object Y exists and is inactive, activate it
            if (objectY != null && !objectY.activeSelf)
            {
                objectY.SetActive(true);
            }
        }
        else if (isManipulating)
        {
            manipulationTimer += Time.deltaTime;

            // If manipulation has stopped and object Y is active for more than activationTime, deactivate it
            if (manipulationTimer >= activationTime)
            {
                isManipulating = false;

                // If object Y exists and is active, deactivate it with fade out
                if (objectY != null && objectY.activeSelf)
                {
                    if (fadeOutCoroutine != null)
                    {
                        StopCoroutine(fadeOutCoroutine);
                    }
                    fadeOutCoroutine = StartCoroutine(FadeOutObjectY());
                }
            }
        }

        // Update previousRotation to current rotation
        previousRotation = transform.rotation;
    }

    private bool IsManipulating()
    {
        // Check if object X is being manipulated (rotated)
        Quaternion deltaRotation = Quaternion.Inverse(previousRotation) * transform.rotation;
        float angle = Quaternion.Angle(Quaternion.identity, deltaRotation);
        return angle > 0.01f; // Adjust the threshold if needed
    }

    private IEnumerator FadeOutObjectY()
    {
        float timer = 0f;

        while (timer < fadeOutTime)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeOutTime;
            Color newYColor = originalColorY;
            newYColor.a = Mathf.Lerp(1f, 0f, progress); // Fade out the transparency
            objectYMaterial.color = newYColor;
            yield return null;
        }

        // Deactivate object Y
        objectY.SetActive(false);
        // Restore the material color to its original color
        if (objectYMaterial != null)
        {
            objectYMaterial.color = originalColorY;
        }
    }
}