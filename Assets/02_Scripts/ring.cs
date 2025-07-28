using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ring : MonoBehaviour
{
    public float radius = 1f;
    public float thickness = 0.1f;
    public int segments = 30;

    private Mesh ringMesh;

    void Start()
    {
        GenerateRing();
    }

    void GenerateRing()
    {
        ringMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = ringMesh;

        int numVertices = segments * 2;
        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[segments * 6];
        Vector2[] uv = new Vector2[numVertices];

        float angleIncrement = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = Mathf.Deg2Rad * angleIncrement * i;
            float x = Mathf.Cos(angle);
            float z = Mathf.Sin(angle);

            vertices[i * 2] = new Vector3(x * (radius - thickness), 0f, z * (radius - thickness));
            vertices[i * 2 + 1] = new Vector3(x * radius, 0f, z * radius);

            uv[i * 2] = new Vector2((float)i / segments, 0f);
            uv[i * 2 + 1] = new Vector2((float)i / segments, 1f);

            int triIndex = i * 6;
            int vertIndex = i * 2;
            triangles[triIndex] = vertIndex;
            triangles[triIndex + 1] = vertIndex + 1;
            triangles[triIndex + 2] = (vertIndex + 2) % numVertices;
            triangles[triIndex + 3] = (vertIndex + 2) % numVertices;
            triangles[triIndex + 4] = vertIndex + 1;
            triangles[triIndex + 5] = (vertIndex + 3) % numVertices;
        }

        ringMesh.vertices = vertices;
        ringMesh.triangles = triangles;
        ringMesh.uv = uv;
        ringMesh.RecalculateNormals();
    }
}