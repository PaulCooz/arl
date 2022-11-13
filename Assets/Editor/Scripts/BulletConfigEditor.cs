﻿using Common.Configs;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.Scripts
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BulletConfigObject))]
    public class BulletConfigEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var customInspector = new VisualElement();
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/bullet-config.uxml");
            visualTree.CloneTree(customInspector);
            return customInspector;
        }
    }
}