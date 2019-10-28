Material CreateMaterial(BSDFData bsdfData, float3 V)
{
    Material mtl;

    float  NdotV = dot(bsdfData.normalWS, V);
    float3 F = F_Schlick(bsdfData.fresnel0, NdotV);

    // If N.V < 0 (can happen with normal mapping) we want to avoid spec sampling
    mtl.specProb = NdotV > 0.001 ? Luminance(F) : 0.0;
    mtl.diffProb = Luminance(bsdfData.diffuseColor);

    // If we are basically black, no need to compute anything else for this material
    if (!IsBlack(mtl))
    {
        mtl.specProb /= mtl.diffProb + mtl.specProb;
        mtl.diffProb = 1.0 - mtl.specProb;

        // Keep these around, rather than passing them to all methods
        mtl.bsdfData = bsdfData;
        mtl.V = V;
    }

    return mtl;
}

bool SampleMaterial(Material mtl, float3 inputSample, out float3 sampleDir, out MaterialResult result)
{
    if (inputSample.z < mtl.specProb)
    {
        if (!SampleGGX(mtl, inputSample, sampleDir, result.specValue, result.specPdf))
            return false;

        EvaluateDiffuse(mtl, sampleDir, result.diffValue, result.diffPdf);
    }
    else
    {
        if (!SampleDiffuse(mtl, inputSample, sampleDir, result.diffValue, result.diffPdf))
            return false;

        EvaluateGGX(mtl, sampleDir, result.specValue, result.specPdf);
    }

    result.diffPdf *= mtl.diffProb;
    result.specPdf *= mtl.specProb;

    return true;
}

void EvaluateMaterial(Material mtl, float3 sampleDir, out MaterialResult result)
{
    EvaluateDiffuse(mtl, sampleDir, result.diffValue, result.diffPdf);
    EvaluateGGX(mtl, sampleDir, result.specValue, result.specPdf);
    result.diffPdf *= mtl.diffProb;
    result.specPdf *= mtl.specProb;
}
