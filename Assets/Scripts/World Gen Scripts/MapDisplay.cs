using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRender;
    public MeshCollider meshCollider;
    public GrassGenerator grassGenerator;

    public float minGrassHeight;
    public float maxGrassHeight;


    public void DrawTexture(Texture2D texture)
    {
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, MeshData colliderData/*, Texture2D texture*/)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshFilter.sharedMesh.RecalculateNormals();
        meshFilter.sharedMesh.RecalculateBounds();
        meshFilter.sharedMesh.RecalculateTangents();
        meshCollider.sharedMesh = meshFilter.sharedMesh;
        /*meshCollider.sharedMesh = colliderData.CreateMesh();*/
        //grassGenerator.GenerateGrass(maxGrassHeight, minGrassHeight, meshData.CreateMesh());
        //meshRender.sharedMaterial.mainTexture = texture;  
    }
}