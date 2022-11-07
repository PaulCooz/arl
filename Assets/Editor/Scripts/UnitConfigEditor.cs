using Common.Configs;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.Scripts
{
    [CustomEditor(typeof(UnitConfigObject))]
    public class UnitConfigEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var customInspector = new VisualElement();
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/unit-config.uxml");
            visualTree.CloneTree(customInspector);

            var isPlayer = serializedObject.FindProperty("isPlayer").boolValue;
            if (isPlayer)
            {
                customInspector.Q<PropertyField>("LevelExpField").visible = false;
                customInspector.Q<PropertyField>("SpawnChanceField").visible = false;
                customInspector.Q<PropertyField>("PrefabField").visible = false;
            }

            return customInspector;
        }
    }
}