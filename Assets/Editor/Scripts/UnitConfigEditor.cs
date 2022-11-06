using Common.Configs;
using UnityEditor;
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
            return customInspector;
        }
    }
}