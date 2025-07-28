using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedalColorChanger : MonoBehaviour
{
    private Renderer rend;
    private Color currentColor;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        // Set initial color
        currentColor = Color.yellow;
        rend.material.color = currentColor;
    }

    private void Update()
    {
        // Check if Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Toggle between yellow and red
            if (currentColor == Color.yellow)
            {
                currentColor = Color.red;
            }
            else
            {
                currentColor = Color.yellow;
            }

            // Apply the new color
            rend.material.color = currentColor;
        }
    }
}