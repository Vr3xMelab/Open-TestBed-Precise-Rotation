using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedLineRenderer : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    public int numberOfPoints = 100;
    public float curveRadius = 2f;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numberOfPoints;
    }

    void Update()
    {
        DrawCurvedLine();
    }

    void DrawCurvedLine()
    {
        // Calculate the midpoint between the two objects
        Vector3 midPoint = (object1.position + object2.position) / 2f;

        // Calculate the direction vector from object1 to object2
        Vector3 direction = (object2.position - object1.position).normalized;

        // Calculate the distance between the objects
        float distance = Vector3.Distance(object1.position, object2.position);

        // Calculate the center of the curve
        Vector3 center = midPoint + direction * (distance / 2f) + Vector3.up * curveRadius;

        // Calculate the angle between the objects
        float angle = Vector3.Angle(object2.position - center, object1.position - center);

        // Calculate the points for the Line Renderer
        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i / (float)(numberOfPoints - 1);
            float theta = Mathf.Lerp(0f, angle, t);
            Quaternion rotation = Quaternion.AngleAxis(theta, Vector3.up);
            Vector3 pointOnCurve = center + rotation * (object1.position - center);

            lineRenderer.SetPosition(i, pointOnCurve);
        }
    }
}