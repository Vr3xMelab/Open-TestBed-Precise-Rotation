using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrigger : MonoBehaviour
{
    [Header("Assign the rotating object here")]
    public GameObject rotatingObject;

    [Header("Assign the object to be activated here")]
    public GameObject objectToActivate;

    private void Start()
    {
        // Ensure the object to be activated is initially inactive
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the rotating object
        if (other.gameObject == rotatingObject)
        {
            // Activate the target object
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Optional: Deactivate the target object when exiting the trigger
        if (other.gameObject == rotatingObject)
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(false);
            }
        }
    }
}