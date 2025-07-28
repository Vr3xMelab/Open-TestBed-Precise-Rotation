using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RotationTracker : MonoBehaviour
{
    public GameObject objectToRotate;
    public List<float> targetRotations = new List<float>() { 30f, 45f, 60f }; // List of target rotations
    private List<Tuple<DateTime, float, float>> rotationErrors = new List<Tuple<DateTime, float, float>>(); // List to store rotation errors, time of click, and time difference
    private int currentTargetIndex = 0; // Index to keep track of the current target rotation
    private DateTime previousEnterPressTime; // Time of the previous Enter key press

    void Start()
    {
        previousEnterPressTime = DateTime.Now;
    }

    void Update()
    {
        // Check for the Enter key press
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CompareRotations();
        }
    }

    void CompareRotations()
    {
        // Check if all targets have been compared
    if (currentTargetIndex >= targetRotations.Count)
        {
            Debug.Log("All targets compared.");
            return;
        }

        // Get the current rotation of the object
        float currentRotationY = objectToRotate.transform.rotation.eulerAngles.y;

        // Calculate the difference between the current rotation and the current target rotation
        float difference = Mathf.Abs(targetRotations[currentTargetIndex] - currentRotationY);

        // Set a threshold for considering the rotation on target
        float threshold = 1.0f; // Adjust this threshold as needed

        // Check if the difference is within the threshold
        if (difference <= threshold)
        {
            // If the rotation is within the threshold, still record it as an error
            Debug.Log("Target Rotation: " + targetRotations[currentTargetIndex] + ", Difference in rotation: " + difference);
        }
        else
        {
            // Print the difference to the console
            Debug.Log("Target Rotation: " + targetRotations[currentTargetIndex] + ", Difference in rotation: " + difference);
        }

        // Calculate time difference between two consecutive Enter key presses
        TimeSpan timeDifference = DateTime.Now - previousEnterPressTime;
        float timeDifferenceSeconds = (float)timeDifference.TotalSeconds;
        Debug.Log("Time Difference between two Enter presses: " + timeDifferenceSeconds + " seconds");

        // Add the time of click, error of rotation, and time difference to the list
        rotationErrors.Add(new Tuple<DateTime, float, float>(DateTime.Now, difference, timeDifferenceSeconds));

        // Update the time of the previous Enter key press
        previousEnterPressTime = DateTime.Now;

        // Move to the next target
        currentTargetIndex++;
    }

    void OnApplicationQuit()
    {
        // Create a CSV file and save rotation errors, time of click, and time difference
        SaveRotationErrorsToCSV();
    }

    void SaveRotationErrorsToCSV()
    {
        // Create a directory for saving CSV files if it doesn't exist
        string directoryPath = Application.dataPath + "/SAVED";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Generate file name with a timestamp
        string fileName = "RotationErrors_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
        string filePath = Path.Combine(directoryPath, fileName);

        // Write data to CSV file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write header
            writer.WriteLine("Time of Click,Error of Rotation,Time Difference (seconds)");

            // Write rotation errors, time of click, and time difference
            foreach (var entry in rotationErrors)
            {
                writer.WriteLine(entry.Item1.ToString("yyyy-MM-dd HH:mm:ss") + "," + entry.Item2 + "," + entry.Item3);
            }
        }

        Debug.Log("CSV file saved: " + filePath);
    }
}