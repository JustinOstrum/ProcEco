using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INoiseFilter
{
    float EvaluateNoise(Vector3 point);
}
