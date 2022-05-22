using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter : INoiseFilter
{
    PlanetNoiseSettings.RigidNoiseSettings planetNoiseSettings;
    PlanetNoise planetNoise = new PlanetNoise();

    public RigidNoiseFilter(PlanetNoiseSettings.RigidNoiseSettings planetNoiseSettings)
    {
        this.planetNoiseSettings = planetNoiseSettings;
    }

    public float EvaluateNoise(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = planetNoiseSettings.baseRoughness;
        float amplitude = 1;
        float weight = 1f;

        for (int i = 0; i < planetNoiseSettings.numberOfLayers; i++)
        {
            float v = 1 - Mathf.Abs(planetNoise.Evaluate(point * frequency + planetNoiseSettings.centre));
            v *= v;
            v += weight;
            weight = Mathf.Clamp01(v * planetNoiseSettings.weightMultiplier);

            noiseValue += v * amplitude;
            frequency *= planetNoiseSettings.roughness;
            amplitude *= planetNoiseSettings.persistence;
        }

        noiseValue = noiseValue - planetNoiseSettings.minValue;
        return noiseValue * planetNoiseSettings.strength;
    }
}
