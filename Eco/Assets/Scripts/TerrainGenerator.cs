using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;

    public float noiseScale;

    public TerrainDisplay terrainDisplay;
    
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);
        terrainDisplay.DrawNoiseMap(noiseMap);
    }
}
