using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NoiseData : UpdatableData
{
    public Noise.NormalizeMode normalizeMode;

    public float noiseScale;

    [Range(1,10)]
    public int octaves;

    [Range(0, 1)]
    public float persistence;
    [Range(1,10)]
    public float lacunarity;

    public int seed;
    public Vector2 offset;
}
