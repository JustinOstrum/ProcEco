using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilter : INoiseFilter
{
    PlanetNoiseSettings.SimpleNoiseSettings planetNoiseSettings;
    PlanetNoise planetNoise = new PlanetNoise();

    public SimpleNoiseFilter(PlanetNoiseSettings.SimpleNoiseSettings planetNoiseSettings)
    {
        this.planetNoiseSettings = planetNoiseSettings;        
    }

    public float EvaluateNoise(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = planetNoiseSettings.baseRoughness;
        float amplitude = 1;

        for (int i = 0; i < planetNoiseSettings.numberOfLayers; i++)
        {
            float v = planetNoise.Evaluate(point * frequency + planetNoiseSettings.centre);
            noiseValue += (v + 1) * .5f * amplitude;
            frequency *= planetNoiseSettings.roughness;
            amplitude *= planetNoiseSettings.persistence;
        }

        noiseValue = noiseValue - planetNoiseSettings.minValue;
        return noiseValue * planetNoiseSettings.strength;
    }
}
