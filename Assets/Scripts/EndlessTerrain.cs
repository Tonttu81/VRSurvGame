using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class EndlessTerrain : MonoBehaviour
{
    public const float maxViewDst = 450;
    public Transform viewer;

    public static Vector2 viewerPosition;
    int ChunkSize;
    int ChunkVisableInViewDist;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> TerrainChunksVisableLastUpdate = new List<TerrainChunk>();

    private void Start()
    {
        ChunkSize = MapGenerator.mapChunkSize - 1;
        ChunkVisableInViewDist = Mathf.RoundToInt(maxViewDst / ChunkSize); 
    }

    private void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisableChunks();
    }

    void UpdateVisableChunks()
    {
        for (int i = 0; i < TerrainChunksVisableLastUpdate.Count; i++)
        {
            TerrainChunksVisableLastUpdate[i].SetVisable(false);
        }
        TerrainChunksVisableLastUpdate.Clear();

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / ChunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / ChunkSize);

        for (int yOffset = -ChunkVisableInViewDist; yOffset <= ChunkVisableInViewDist; yOffset++)
        {
            for (int xOffset = -ChunkVisableInViewDist; xOffset <= ChunkVisableInViewDist; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                if (terrainChunkDictionary.ContainsKey(viewedChunkCoord))
                {
                    terrainChunkDictionary[viewedChunkCoord].Update();
                    if (terrainChunkDictionary[viewedChunkCoord].IsVisable())
                    {
                        TerrainChunksVisableLastUpdate.Add(terrainChunkDictionary[viewedChunkCoord]);
                    }
                }
                else
                {
                    terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, ChunkSize, transform));
                }
            }
        }
    }

    public class TerrainChunk
    {
        GameObject meshObject;
        Vector2 position;
        Bounds bounds;

        public TerrainChunk(Vector2 coord, int size, Transform parent)
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);

            meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            meshObject.transform.position = positionV3;
            meshObject.transform.localScale = Vector3.one * size / 10f;
            meshObject.transform.parent = parent;
            SetVisable(false);

        }
        public void Update()
        {
            float viewerDistFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
            bool visable = viewerDistFromNearestEdge <= maxViewDst;
            SetVisable(visable);
        }

        public void SetVisable(bool visable)
        {
            meshObject.SetActive(visable);
        }
        public bool IsVisable()
        {
            return meshObject.activeSelf;
        }

    }
}
