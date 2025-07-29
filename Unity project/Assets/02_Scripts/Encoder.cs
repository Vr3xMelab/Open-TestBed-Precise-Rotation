////////////////////////////////////////////////////////////////////////////////////////////////////////
//Authors : Mine Dastan, Michele Fiorentino
// V3
//Created On : 3/7/2024 
//Copy Rights : MELAB, DMMM, Politecnico di Bari
//Description : setup for encoder
////////////////////////////////////////////////////////////////////////////////////////////////////////


using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class Encoder : MonoBehaviour
{
    public GameObject objectToRotate;
    public string serialPortName = "COM5"; // Change this to the port your ESP32 is connected to
    public int baudRate = 921600; // Set baud rate to 921600
    public int maxEncoderValue = 199; // Maximum value from your encoder (200-1 = 199 considering 0)
    public int maxRotationAngle = 359; // Maximum angle for rotation (360-1 = 359 considering 0)
    public float g_currentAngle0_359 = 0; // this the current rotation
    public bool g_debugraw = false;
    // privates
    string serial_line;
    int serialByte;
    int encoderValue = 0;

    private SerialPort serialPort;

    void Start()
    {
        // Initialize serial port
        serialPort = new SerialPort(serialPortName, baudRate);
        serialPort.Open();
    }

    void Update()
    {
        // Read data from serial port
        if (serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            try

            {
                serialByte = serialPort.ReadByte();

                if (serialByte == 1) encoderValue += 1;
                else if (serialByte == 255) encoderValue -= 1;

                if (encoderValue > 199) encoderValue = 0;
                else //topping 200
                if (encoderValue < 0) encoderValue = 199;

                if (g_debugraw) print("encoder value=" + encoderValue);
                /*
                serial_line = serialPort.ReadLine();
                encoderValue = int.Parse(serial_line.Trim()); // Trim any whitespace characters   
                 */                                           // Map encoder value to rotation angle
                g_currentAngle0_359 = Map(encoderValue, 0, maxEncoderValue, 0, maxRotationAngle);

                if (g_debugraw) print("Encoder.cs mappedAngle (0-359)=" + g_currentAngle0_359);

                // Rotate object
                objectToRotate.transform.rotation = Quaternion.Euler(0, g_currentAngle0_359, 0);

            }
            catch (System.Exception)
            {
                // Handle exceptions
                Debug.Log("Encoder.cs Exception");
            }
        }
    }

    void OnDestroy()
    {
        // Close serial port when object is destroyed
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    // Function to map a value from one range to another
    float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}