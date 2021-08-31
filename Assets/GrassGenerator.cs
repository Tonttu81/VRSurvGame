using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGenerator : MonoBehaviour
{
    MeshFilter grassMesh;

    Vector3[] vertices;
    int[] triangles;

    Mesh final;

    private void Start()
    {
        grassMesh = GetComponent<MeshFilter>();
    }

    public void GenerateGrass(float maxGrassHeight, float minGrassHeight, Mesh mapMesh)
    {
        /*
        triangles = mapMesh.triangles;
        vertices = mapMesh.vertices;
        final = new Mesh();
        final.triangles = triangles;
        final.vertices = vertices;
        final.RecalculateNormals();
        grassMesh.sharedMesh = final;
        */

        grassMesh.sharedMesh = mapMesh;
    }
}
