using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Mods.Units
{
    public class UnitsWindow : EditorWindow
    {
        private const string UnitsFolder = "Assets/Resources/configs/units";

        private static VisualTreeAsset WindowPrefab =>
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Mods/Units/units_window.uxml");

        private ScrollView _content;
        private TextField _addUnit;

        [MenuItem("Mods/Units")]
        private static void ShowWindow()
        {
            GetWindow<UnitsWindow>().titleContent = new GUIContent("Units");
        }

        private void CreateGUI()
        {
            var windowUxml = WindowPrefab;
            var container = windowUxml.CloneTree();

            _content = container.Q<ScrollView>("Content");

            var files = Directory.GetFiles(UnitsFolder, "*.json", SearchOption.AllDirectories);
            foreach (var path in files)
            {
                _content.Add(new Unit(path));
            }

            _addUnit = container.Q<TextField>("AddUnit");
            _addUnit.Q<Button>("AddUnitButton").clicked += AddUnitButton;

            container.style.flexGrow = 1f;
            rootVisualElement.Add(container);
        }

        private void AddUnitButton()
        {
            var path = $"{Path.Combine(UnitsFolder, _addUnit.text)}.json";

            File.WriteAllText(path, "{}");
            _content.Add(new Unit(path));
        }
    }
}