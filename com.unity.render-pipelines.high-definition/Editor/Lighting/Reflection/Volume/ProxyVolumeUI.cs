using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

namespace UnityEditor.Experimental.Rendering.HDPipeline
{
    using CED = CoreEditorDrawer<SerializedProxyVolume>;

    class ProxyVolumeUI
    {
        internal static GUIContent shapeContent = CoreEditorUtils.GetContent("Shape|The shape of the Proxy.\nInfinite is compatible with any kind of InfluenceShape.");
        internal static GUIContent boxSizeContent = CoreEditorUtils.GetContent("Box Size|The size of the box.");
        internal static GUIContent sphereRadiusContent = CoreEditorUtils.GetContent("Sphere Radius|The radius of the sphere.");

        public static readonly CED.IDrawer SectionShape = CED.Group((serialized, owner) =>
        {
            if (serialized.shape.hasMultipleDifferentValues)
            {
                EditorGUI.showMixedValue = true;
                EditorGUILayout.PropertyField(serialized.shape, shapeContent);
                EditorGUI.showMixedValue = false;
                return;
            }
            else
                EditorGUILayout.PropertyField(serialized.shape, shapeContent);

            switch ((ProxyShape)serialized.shape.intValue)
            {
                case ProxyShape.Box:
                    EditorGUILayout.PropertyField(serialized.boxSize, boxSizeContent);
                    break;
                case ProxyShape.Sphere:
                    EditorGUILayout.PropertyField(serialized.sphereRadius, sphereRadiusContent);
                    break;
                case ProxyShape.Infinite:
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        });
    }
}
