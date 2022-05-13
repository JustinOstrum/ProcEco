using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour
{
    public const float maxViewDistance = 450;
    public Transform viewer;

    public static Vector2 viewPosition;
    int chunkSize;
    int chunksVisibleInViewDistance;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

    private void Start()
    {
        chunkSize = TerrainGenerator.mapChunkSize - 1;
        chunksVisibleInViewDistance = Mathf.RoundToInt(maxViewDistance / chunkSize);
    }

    private void Update()
    {
        viewPosition = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisibleChunks();
    }

    void UpdateVisibleChunks()
    {
        for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++)
        {
            terrainChunksVisibleLastUpdate[i].SetVisible(false);
        }

        terrainChunksVisibleLastUpdate.Clear();

        int currentChunkCoordX = Mathf.RoundToInt(viewPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewPosition.y / chunkSize);

        for (int yOffset = -chunksVisibleInViewDistance; yOffset <= chunksVisibleInViewDistance; yOffset++)
        {
            for (int xOffset = -chunksVisibleInViewDistance; xOffset <= chunksVisibleInViewDistance; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                if (terrainChunkDictionary.ContainsKey(viewedChunkCoord))
                {
                    terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
                    if (terrainChunkDictionary[viewedChunkCoord].IsVisible())
                    {
                        terrainChunksVisibleLastUpdate.Add(terrainChunkDictionary[viewedChunkCoord]);
                    }
                }

                else
                {
                    terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize, transform));
                }
            }
        }
    }

    public class TerrainChunk
    {
        GameObject meshObj;
        Vector2 position;
        Bounds bounds;

        public TerrainChunk(Vector2 coord, int size, Transform parent)
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);

            meshObj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            meshObj.transform.position = positionV3;
            meshObj.transform.localScale = Vector3.one * size / 10;
            meshObj.transform.parent = parent;
            SetVisible(false);
        }

        public void UpdateTerrainChunk()
        {
            float viewDistanceFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewPosition));
            bool visible = viewDistanceFromNearestEdge <= maxViewDistance;
            SetVisible(visible);
        }

        public void SetVisible(bool visible)
        {
            meshObj.SetActive(visible);
        }

        public bool IsVisible()
        {
            return meshObj.activeSelf;
        }
    }
}
