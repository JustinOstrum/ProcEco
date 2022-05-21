using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    ShapeSettings settings;
    INoiseFilter[] noiseFilters;
    public MinMax elevationMinMax;

    public ShapeGenerator(ShapeSettings settings)
    {
        this.settings = settings;
        noiseFilters = new INoiseFilter[settings.planetNoiseLayers.Length];

        for (int i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(settings.planetNoiseLayers[i].planetNoiseSettings);
        }

        elevationMinMax = new MinMax();
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float firstLayerValue = 0;
        float elevation = 0;

        if(noiseFilters.Length > 0)
        {
            firstLayerValue = noiseFilters[0].EvaluateNoise(pointOnUnitSphere);
            if (settings.planetNoiseLayers[0].enabled)
            {
                elevation = firstLayerValue;
            }
        }

        for (int i = 1; i < noiseFilters.Length; i++)
        {
            if (settings.planetNoiseLayers[i].enabled)
            {
                float mask = (settings.planetNoiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;
                elevation += noiseFilters[i].EvaluateNoise(pointOnUnitSphere) * mask;
            }
        }

        elevation = settings.planetRadius * (1 + elevation);
        elevationMinMax.AddValue(elevation);

        return pointOnUnitSphere * elevation;
    }
}