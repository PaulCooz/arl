using Common.Configs;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.Scripts
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(GunConfigObject))]
    public class GunConfigEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var customInspector = new VisualElement();
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/gun-config.uxml");
            visualTree.CloneTree(customInspector);
            return customInspector;
        }
    }
}