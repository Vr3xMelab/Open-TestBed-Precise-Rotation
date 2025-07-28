using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotator : MonoBehaviour
{
    public GameObject subChild1;    // The first subchild to be activated/deactivated with Image1
    public GameObject subChild2;    // The second subchild to be activated/deactivated with Image2
    public GameObject image1;       // First image object
    public GameObject image2;       // Second image object

    void Update()
    {
        // Check the activation state of image1 and image2
        bool isImage1Active = image1.activeInHierarchy;
        bool isImage2Active = image2.activeInHierarchy;

        // Activate or deactivate subChild1 based on the state of image1
        subChild1.SetActive(isImage1Active);
        // Activate or deactivate subChild2 based on the state of image2
        subChild2.SetActive(isImage2Active);
    }
}