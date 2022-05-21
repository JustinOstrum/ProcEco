using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseFilterFactory
{
    public static INoiseFilter CreateNoiseFilter(PlanetNoiseSettings settings)
    {
        switch (settings.filterType)
        {
            case PlanetNoiseSettings.FilterType.Simple:
                return new SimpleNoiseFilter(settings.simpleNoiseSettings);

            case PlanetNoiseSettings.FilterType.Rigid:
                return new RigidNoiseFilter(settings.rigidNoiseSettings);
        }

        return null;
    }
}
