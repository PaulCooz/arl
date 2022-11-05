using System;
using System.Collections.Generic;
using Common;
using Newtonsoft.Json.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Mods.Data
{
    public class ParameterElement : VisualElement
    {
        public ParameterElement
            (in KeyValuePair<string, JToken> parameter, Parameter data, Action<Pair<string, string>[]> onContext)
        {
            style.flexDirection = FlexDirection.Row;

            var field = (BindableElement) null;

            var buttons = new VisualElement();
            buttons.Add(new Button() {text = "reset", style = {maxHeight = 24}});

            switch (data.type)
            {
                case Parameter.Types.Number:
                    field = new DoubleField(parameter.Key)
                    {
                        value = parameter.Value.ToObject<double>(),
                        style = {flexGrow = 1f, fontSize = 20}
                    };
                    break;

                case Parameter.Types.String:
                    field = new TextField(parameter.Key)
                    {
                        value = parameter.Value.ToObject<string>(),
                        style = {flexGrow = 1f, fontSize = 20}
                    };
                    break;

                case Parameter.Types.Boolean:
                    field = new Toggle(parameter.Key)
                    {
                        value = parameter.Value.ToObject<bool>(),
                        style = {flexGrow = 1f, fontSize = 20}
                    };
                    break;

                case Parameter.Types.Script:
                    field = new TextField(parameter.Key)
                    {
                        value = parameter.Value.ToObject<string>(),
                        multiline = true,
                        style = {flexGrow = 1f, fontSize = 20, minHeight = 100}
                    };
                    buttons.Add
                    (
                        new Button
                        (
                            () => onContext.Invoke(data.scriptContext)
                        ) {text = "context", style = {maxHeight = 24}}
                    );
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(data), data, null);
            }

            field.tooltip =
                $"{data.type.ToString()} {(string.IsNullOrEmpty(data.description) ? "" : $"| {data.description}")}";

            field.style.borderBottomWidth = field.style.borderLeftWidth = field.style.borderTopWidth =
                field.style.borderRightWidth = 1;
            field.style.borderBottomColor = field.style.borderLeftColor = field.style.borderTopColor =
                field.style.borderRightColor = new StyleColor(new Color(0.35f, 0.35f, 0.35f, 1));

            field.style.flexWrap = Wrap.Wrap;

            field.Add(buttons);

            Add(field);
        }
    }
}