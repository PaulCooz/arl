using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Mods.Data
{
    public class ConfigWindow : EditorWindow
    {
        private VisualElement _floatWindow;
        private VisualElement _context;

        private List<TextField> _showContext = new();

        private bool ContextShow
        {
            get => _floatWindow.visible;
            set
            {
                if (value)
                {
                    _floatWindow.visible = true;
                    _floatWindow.style.width = new StyleLength(StyleKeyword.Auto) {value = 200};
                    _floatWindow.style.flexGrow = 1f;
                }
                else
                {
                    _floatWindow.visible = false;
                    _floatWindow.style.width = 0f;
                    _floatWindow.style.flexGrow = 0f;
                }
            }
        }

        public static ConfigWindow CreateWindow(in Config config, in Parameter[] parameters)
        {
            var window = GetWindow<ConfigWindow>();
            window.titleContent = new GUIContent("config");
            window.Setup(config, parameters);

            return window;
        }

        private void Setup(in Config config, in Parameter[] parameters)
        {
            _showContext = new List<TextField>();

            _floatWindow = new VisualElement {style = {flexGrow = 1f}};
            _context = new VisualElement {style = {flexGrow = 1f}};
            _floatWindow.Add(_context);
            _floatWindow.Add(new Button(ClearContext) {text = "hide"});

            var container = new VisualElement {style = {flexGrow = 1f}};
            foreach (var token in config)
            {
                var data = parameters.First(p => p.name == token.Key);

                container.Add(new ParameterElement(token, data, ShowContext));
            }

            rootVisualElement.style.flexDirection = FlexDirection.Row;
            rootVisualElement.Add(container);
            rootVisualElement.Add(_floatWindow);

            ContextShow = false;
        }

        private void ClearContext()
        {
            foreach (var cont in _showContext)
            {
                _context.Remove(cont);
            }

            _showContext.Clear();
            ContextShow = false;
        }

        private void ShowContext(Pair<string, string>[] context)
        {
            ClearContext();

            foreach (var val in context)
            {
                var contElement = new TextField
                {
                    value = val.key,
                    isReadOnly = true,
                    tooltip = val.value
                };

                _context.Add(contElement);
                _showContext.Add(contElement);
            }

            ContextShow = context.Length > 0;
        }
    }

}