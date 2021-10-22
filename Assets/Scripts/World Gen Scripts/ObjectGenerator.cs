using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public Object[] objects;

    public int objectDensity;

    List<GameObject> spawnedObjects;

    private void Start()
    {
        GenerateObjects();
    }

    private void Update()
    {
        /*
        for (int i = 0; i < spawnedObjects.Count; i++)
        {

        }
        */
    }

    public void GenerateObjects()
    {
        int seed = GameObject.FindObjectOfType<MapGenerator>().seed;

        System.Random orng = new System.Random(seed);

        for (int i = 0; i < objects.Length; i++)
        {
            for (int j = 0; j < objects[i].objectDensity; j++)
            {
                float x = orng.Next(-900, 900);
                float y = orng.Next(-900, 900);

                //int objectId = orng.Next(0, objects.Length);

                RaycastHit objectRay;
                if (Physics.Raycast(new Vector3(x, 100, y), Vector3.down * 100, out objectRay, Mathf.Infinity))
                {
                    if (objectRay.point.y > objects[i].objMinSpawnHeight && objectRay.point.y < objects[i].objMaxSpawnHeight)
                    {

                        float yRotation = orng.Next(0, 360);

                        GameObject tree = Instantiate(objects[i].obj, objectRay.point, Quaternion.Euler(new Vector3(0, yRotation, 0)));
                        spawnedObjects.Add(tree);


                        int rnd = orng.Next(-5, 5);

                        float size = (float)rnd / 10;

                        tree.transform.localScale += new Vector3(size, size, size);

                    }
                }
            }
        }
    }

    [System.Serializable]
    public struct Object
    {
        public GameObject obj;
        public float objMinSpawnHeight;
        public float objMaxSpawnHeight;
        public float objectDensity;

        public Object(GameObject _obj, float _objMinSpawnHeight, float _objMaxSpawnHeight, float _objectDensity)
        {
            obj = _obj;
            objMinSpawnHeight = _objMinSpawnHeight;
            objMaxSpawnHeight = _objMaxSpawnHeight;
            objectDensity = _objectDensity;
        }
    }
}