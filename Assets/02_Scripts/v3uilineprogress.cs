using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class v3uilineprogress : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    public Transform center;

    public float thickness = 0.1f;
    public Material material;

    private void Start()
    {
        GenerateRing();
    }

    private void GenerateRing()
    {
        // Calculate the angle between the objects
        Vector3 dir1 = object1.position - center.position;
        Vector3 dir2 = object2.position - center.position;
        float angle = Vector3.Angle(dir1, dir2);

        // Calculate the positions of the vertices on the circumference
        Vector3[] vertices = new Vector3[4];
        vertices[0] = Quaternion.Euler(0, 0, -angle / 2) * dir1.normalized * center.localScale.x;
        vertices[1] = Quaternion.Euler(0, 0, angle / 2) * dir2.normalized * center.localScale.x;
        vertices[2] = vertices[0] + Vector3.forward * thickness;
        vertices[3] = vertices[1] + Vector3.forward * thickness;

        // Create the mesh
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = new int[] { 0, 2, 1, 1, 2, 3 };

        // Assign material and render
        GameObject ring = new GameObject("Ring");
        MeshFilter filter = ring.AddComponent<MeshFilter>();
        filter.mesh = mesh;
        MeshRenderer renderer = ring.AddComponent<MeshRenderer>();
        renderer.material = material;
    }
}