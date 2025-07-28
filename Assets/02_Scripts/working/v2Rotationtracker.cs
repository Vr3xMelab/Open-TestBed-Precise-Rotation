////////////////////////////////////////////////////////////////////////////////////////////////////////
//Authors : Mine Dastan, Michele Fiorentino
// V3
//Created On : 3/7/2024 
//Copy Rights : MELAB, DMMM, Politecnico di Bari
//Description : Class for serialize in excel rotation data from rotational device
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;
using static UnityEngine.GraphicsBuffer;

public class v2Rotationtracker : MonoBehaviour
{
    // key variables global
    float kg_currentAngle;
    float kg_targetAngle;
    float kg_missing_angle;
    float kg_elapsedFromTargetStart;
    public int kg_currentTargetIndex = 0; // Index of the current target in the list
    public string kg_sceneName; // Name of the scene for file naming purposes

    float kg_startAngle;
    float kg_angularVelocity;
    float previousAngle; // To store the previous angle for velocity calculation
    DateTime previousTime; // To store the previous time for velocity calculation

    // Public variables accessible from Unity Editor
    public GameObject objectToRotate; // GameObject to be rotated by the player
    public List<GameObject> targetPrefabs = new List<GameObject>(); // List of target GameObjects to compare rotation with
    public AudioClip targetConfirmedSound; // Sound played when a target rotation is confirmed
    public AudioClip allTargetsComparedSound; // Sound played when all targets are compared
    public Canvas uiCanvas; // Canvas for UI elements
    public GameObject zoomMagnifier; // Optional GameObject for zoom functionality

    // Private variables
    private AudioSource audioSource; // AudioSource component for playing audio clips
    public List<Tuple<DateTime, float, float, string, int>> g_rotationErrors_list = new List<Tuple<DateTime, float, float, string, int>>(); // List to store rotation error data

    public GameObject CurrentTarget => kg_currentTargetIndex < targetPrefabs.Count ? targetPrefabs[kg_currentTargetIndex] : null; // Property to get the current target GameObject
    public DateTime previousEnterPressTime; // Time stamp of the previous Enter key press
    public GameObject g_currentTarget; // Reference to the current target GameObject

    StreamWriter g_testWriter; // writer fo 

    // Targert data saving 
    // couroutine
    private Coroutine g_captureCoroutine; // Coroutine reference for capturing data
    private float g_captureInterval = 0.01f; // Interval between captures in seconds (100 milliseconds)
    private bool allTargetsCompared = false; // Flag to track if all targets have been compared
    //private float g_captureElapsed = 0f;
    StreamWriter g_intervalWriter;

    void Start()
    {
        // Get AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Set initial previousEnterPressTime to current time
        previousEnterPressTime = DateTime.Now;

        // Shuffle the list of targetPrefabs
        Shuffle(targetPrefabs);

        // Activate the first target prefab if available
        if (targetPrefabs.Count > 0)
        {
            g_currentTarget = targetPrefabs[kg_currentTargetIndex];
            g_currentTarget.SetActive(true);
        }
        else
        {
            Debug.LogError("No target prefabs available."); // Log error if no target prefabs are available
        }

        openTargetStream();

        // Initialize previousAngle and previousTime for angular velocity calculation
        previousAngle = NormalizeAngle_0_359(objectToRotate.transform.rotation.eulerAngles.y);
        previousTime = DateTime.Now;
    }

    void openTargetStream()
    {
        // Get desktop path and create directory if it doesn't exist
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string directoryPath = Path.Combine(desktopPath, "TARGETS");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Generate unique file names based on scene name and current timestamp
        string intervalFileName = $"Interval_TargetData_{kg_sceneName}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
        string intervalFilePath = Path.Combine(directoryPath, intervalFileName);

        string targetFileName = $"Single_TargetData_{kg_sceneName}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
        string targetFilePath = Path.Combine(directoryPath, targetFileName);

        // Initialize StreamWriters for target data and error data
        g_intervalWriter = new StreamWriter(intervalFilePath);
        Debug.Log("Saving to " + intervalFilePath);
        g_intervalWriter.WriteLine("Trial ID Count, Trial Name, Missing Angle, Elapsed Time, Normalized Trial Angle, Normalized Current Angle, Start Angle, Angular Velocity"); // Write header line

        g_testWriter = new StreamWriter(targetFilePath);
        Debug.Log("Saving to " + targetFilePath);
        g_testWriter.WriteLine("Error of Rotation,Time Difference (seconds),Target Name"); // Write header line
    }

    void SaveTargetParameters()
    {
        if (g_intervalWriter != null)
        {
            // Calculate angular velocity
            TimeSpan timeSpan = DateTime.Now - previousTime;
            kg_angularVelocity = (kg_currentAngle - previousAngle) / (float)timeSpan.TotalSeconds;

            // Write data to CSV file
            g_intervalWriter.WriteLine($"{kg_currentTargetIndex}, {g_currentTarget.name}, {kg_missing_angle}, {kg_elapsedFromTargetStart}, {kg_targetAngle}, {kg_currentAngle}, {kg_startAngle}, {kg_angularVelocity}");

            // Update previousAngle and previousTime for next velocity calculation
            previousAngle = kg_currentAngle;
            previousTime = DateTime.Now;
        }
    }

    void Update()
    {
        calculateParameters(); // Call calculateParameters method

        if (Input.GetKeyDown(KeyCode.Return))    // Check for Enter key press
        {
            // Store rotation error data (time of click, error of rotation, time kg_missing_angle, target name)
            g_rotationErrors_list.Add(new Tuple<DateTime, float, float, string, int>(DateTime.Now, kg_missing_angle, kg_elapsedFromTargetStart, g_currentTarget.name, kg_currentTargetIndex));

            // Set the start angle for the next target
            kg_startAngle = kg_currentAngle;

            nextTarget();
        }
        else if (kg_elapsedFromTargetStart > g_captureInterval)
        {
            SaveTargetParameters();
        }
    }

    void calculateParameters()
    {
        // Check if all targets have been compared
        if (kg_currentTargetIndex >= targetPrefabs.Count)
        {
            Debug.Log("All targets compared."); // Log message indicating all targets have been compared
            HandleAllTargetsCompared(); // Call method to handle completion of all targets comparison
            return;
        }

        // Get current and target rotation angles, normalize them
        kg_currentAngle = NormalizeAngle_0_359(objectToRotate.transform.rotation.eulerAngles.y);
        kg_targetAngle = NormalizeAngle_0_359(g_currentTarget.transform.rotation.eulerAngles.y);

        // Calculate kg_missing_angle as the difference between normalized angles
        kg_missing_angle = kg_targetAngle - kg_currentAngle;
        // Normalize missing angle to be within -180 and 180 degrees
        if (kg_missing_angle > 180f)
        {
            kg_missing_angle -= 360f;
        }
        else if (kg_missing_angle < -180f)
        {
            kg_missing_angle += 360f;
        }

        // Calculate time elapsed from the start of target comparison
        TimeSpan timeDifference = DateTime.Now - previousEnterPressTime;
        kg_elapsedFromTargetStart = (float)timeDifference.TotalSeconds;
    }

    void nextTarget()
    {
        previousEnterPressTime = DateTime.Now; // Update previousEnterPressTime to current time

        // Deactivate current target
        g_currentTarget.SetActive(false);
        kg_currentTargetIndex++; // Move to the next target index

        // Activate next target if available, play confirmation sound
        if (kg_currentTargetIndex < targetPrefabs.Count)
        {
            g_currentTarget = targetPrefabs[kg_currentTargetIndex];
            g_currentTarget.SetActive(true);
            audioSource.PlayOneShot(targetConfirmedSound);
        }
        else
        {
            Debug.Log("All targets compared."); // Log message indicating all targets have been compared
            HandleAllTargetsCompared(); // Call method to handle completion of all targets comparison
        }
    }

    // Method to normalize p_angle between 0 and 360 degrees
    float NormalizeAngle_0_359(float p_angle)
    {
        p_angle %= 360f;
        if (p_angle < 0)
        {
            p_angle += 360f;
        }
        return p_angle;
    }

    // Method to handle completion of all targets comparison
    void HandleAllTargetsCompared()
    {
        // Check if all targets have already been compared
        if (allTargetsCompared)
            return;

        // Set the flag to true to indicate all targets have been compared
        allTargetsCompared = true;

        // Your existing logic for handling completion of all targets
        audioSource.PlayOneShot(allTargetsComparedSound); // Play completion sound
        uiCanvas.gameObject.SetActive(false); // Disable UI canvas
        if (zoomMagnifier != null)
        {
            zoomMagnifier.SetActive(false); // Disable zoom magnifier if it exists
        }

        SaveRotationErrorsToCSV(); // Call method to save rotation error data to CSV file

        // Close the StreamWriter for target data
        if (g_intervalWriter != null)
        {
            g_intervalWriter.Close();
            g_intervalWriter = null;
        }

        // Close the StreamWriter for error data
        if (g_testWriter != null)
        {
            g_testWriter.Close();
            g_testWriter = null;
        }
    }

    // Method to save rotation error data to CSV file
    void SaveRotationErrorsToCSV()
    {
        if (g_testWriter != null)
        {
            foreach (var entry in g_rotationErrors_list)
            {
                g_testWriter.WriteLine($"{entry.Item2},{entry.Item3},{entry.Item4}"); // Write each data entry
            }
            g_testWriter.Flush();
        }
        Debug.Log("Rotation errors CSV file saved.");
    }

    // UTILITY FUNCTIONS

    // Method to shuffle a list using Fisher-Yates algorithm
    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
