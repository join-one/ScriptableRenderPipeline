#if UNITY_EDITOR //file must be in realtime assembly folder to be found in HDRPAsset
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace UnityEngine.Experimental.Rendering.HDPipeline
{
    public partial class HDRenderPipelineEditorResources : ScriptableObject
    {
        public GameObject defaultScene;
        public VolumeProfile defaultVolumeProfile;
        public PostProcessProfile defaultPostProcessProfile;
        public DiffusionProfileSettings defaultDiffusionProfileSettings;

        [Serializable]
        public sealed class ShaderResources
        {
            public Shader defaultSpeedTree7Shader;
            public Shader defaultSpeedTree8Shader;
        }

        [Serializable]
        public sealed class MaterialResources
        {
            // Defaults
            public Material defaultDiffuseMat;
            public Material defaultMirrorMat;
            public Material defaultDecalMat;
            public Material defaultTerrainMat;
        }

        [Serializable]
        public sealed class TextureResources
        {
        }

        [Serializable]
        public sealed class ShaderGraphResources
        {
            public Shader autodeskInteractive;
            public Shader autodeskInteractiveMasked;
            public Shader autodeskInteractiveTransparent;
        }

        public ShaderResources shaders;
        public MaterialResources materials;
        public TextureResources textures;
        public ShaderGraphResources shaderGraphs;

        // Note: move this to a static using once we can target C#6+
        T Load<T>(string path) where T : UnityEngine.Object
        {
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        public void Init()
        {
            // Load default renderPipelineResources / Material / Shader
            string HDRenderPipelinePath = HDUtils.GetHDRenderPipelinePath() + "Runtime/";

            // Shaders
            shaders = new ShaderResources
            {
                defaultSpeedTree7Shader = Load<Shader>(HDRenderPipelinePath + "Material/Nature/SpeedTree7Lit.shader"),
                defaultSpeedTree8Shader = Load<Shader>(HDRenderPipelinePath + "Material/Nature/SpeedTree7Lit.shader"),
            };

            // Materials
            materials = new MaterialResources
            {
                // Defaults
                defaultDiffuseMat = Load<Material>(HDRenderPipelinePath + "RenderPipelineResources/Material/DefaultHDMaterial.mat"),
                defaultMirrorMat = Load<Material>(HDRenderPipelinePath + "RenderPipelineResources/Material/DefaultHDMirrorMaterial.mat"),
                defaultDecalMat = Load<Material>(HDRenderPipelinePath + "RenderPipelineResources/Material/DefaultHDDecalMaterial.mat"),
                defaultTerrainMat = Load<Material>(HDRenderPipelinePath + "RenderPipelineResources/Material/DefaultHDTerrainMaterial.mat"),
            };

            // Textures
            textures = new TextureResources
            {
            };

            // ShaderGraphs
            shaderGraphs = new ShaderGraphResources
            {
                //autodesk interactive
                autodeskInteractive = Load<Shader>(HDRenderPipelinePath + "RenderPipelineResources/ShaderGraph/AutodeskInteractive.ShaderGraph"),
                autodeskInteractiveMasked = Load<Shader>(HDRenderPipelinePath + "RenderPipelineResources/ShaderGraph/AutodeskInteractiveMasked.ShaderGraph"),
                autodeskInteractiveTransparent = Load<Shader>(HDRenderPipelinePath + "RenderPipelineResources/ShaderGraph/AutodeskInteractiveTransparent.ShaderGraph"),
            };
        }
    }
}
#endif
