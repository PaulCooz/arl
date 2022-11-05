using System.Collections.Generic;
using System.IO;
using Editor.Mods.Data;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.Mods.Units
{
    public class Unit : VisualElement
    {
        private const string NullParent = "-";

        private static VisualTreeAsset UnitPrefab =>
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Mods/Units/unit.uxml");

        private static readonly List<string> Units = new() {NullParent};

        private readonly Config _config;

        private readonly Button _root;
        private readonly DropdownField _parent;

        public Unit(in string path)
        {
            var unit = Path.GetFileNameWithoutExtension(path);
            var json = File.ReadAllText(path);

            _config = new Config(json);
            _root = UnitPrefab.CloneTree().Q<Button>("Root");
            _parent = _root.Q<DropdownField>("Parent");

            _parent.choices = Units;

            var parentUnit = _config.GetString("parent");
            if (string.IsNullOrEmpty(parentUnit)) parentUnit = NullParent;
            _parent.value = parentUnit;

            _root.text = unit;
            _root.clicked += ShowConfig;

            Add(_root);

            Units.Add(unit);
        }

        private void ShowConfig()
        {
            ConfigWindow.CreateWindow(_config, Parameters.Units);
        }
    }
}