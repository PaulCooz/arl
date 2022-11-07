using Common.Configs;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.Scripts
{
    [CustomEditor(typeof(CardsConfigObject))]
    public class CardsConfigEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var customInspector = new VisualElement();
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/cards-config.uxml");
            visualTree.CloneTree(customInspector);
            return customInspector;
        }
    }
}