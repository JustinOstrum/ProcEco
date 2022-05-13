using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Note: This is not my original code.
//Sebastian Lague (https://www.youtube.com/playlist?list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3) is the creator of this code.
//I merely followed a tutorial.

[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TerrainGenerator terrainGenerator = (TerrainGenerator)target;

        if (DrawDefaultInspector())
        {
            if (terrainGenerator.autoUpdate)
            {
                terrainGenerator.DrawMapInEditor();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            terrainGenerator.DrawMapInEditor();
        }
    }
}
