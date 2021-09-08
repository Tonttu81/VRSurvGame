using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject[] objects;

    public int objectDensity;

    public float treeMinSpawnHeight;
    public float treeMaxSpawnHeight;

    private void Start()
    {
        GenerateTrees();
    }

    public void GenerateTrees()
    {
        int seed = GameObject.FindObjectOfType<MapGenerator>().seed;


        System.Random trng = new System.Random(seed);

        for (int i = 0; i < objectDensity; i++)
        {
            float x = trng.Next(-900, 900);
            float y = trng.Next(-900, 900);

            RaycastHit treeRay;
            if (Physics.Raycast(new Vector3(x, 100, y), Vector3.down * 100, out treeRay, Mathf.Infinity))
            {
                if (treeRay.point.y > treeMinSpawnHeight && treeRay.point.y < treeMaxSpawnHeight)
                {
                    int treeId = trng.Next(0, objects.Length);
                    float yRotation = trng.Next(0, 360);

                    GameObject tree = Instantiate(objects[treeId], treeRay.point, Quaternion.Euler(new Vector3(0, yRotation, 0)));

                    
                    int rnd = trng.Next(-5, 5);

                    float size = (float)rnd / 10;

                    tree.transform.localScale += new Vector3(size, size, size);
                    
                }
            }
        }
    }
}