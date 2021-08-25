using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShaderVars
{
    public static void UpdateMeshHeights(Material material, float meshHeightMultiplier, AnimationCurve meshHeightCurve)
    {
        float minHeight = meshHeightMultiplier * meshHeightCurve.Evaluate(0);
        float maxHeight = meshHeightMultiplier * meshHeightCurve.Evaluate(1);


        material.SetFloat("minHeight", minHeight);
        material.SetFloat("maxHeight", maxHeight);
    }
}
