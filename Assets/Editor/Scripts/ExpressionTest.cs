using Common.Interpreters;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.Scripts
{
    public class ExpressionTest : EditorWindow
    {
        private TextField _inputTextField;
        private TextField _outputTextField;

        [MenuItem("Test/Expression Test")]
        public static void Init()
        {
            GetWindow<ExpressionTest>("Expression Test").Show();
        }

        private void CreateGUI()
        {
            _inputTextField = new TextField {style = {fontSize = 30}};
            _outputTextField = new TextField {isReadOnly = true, style = {fontSize = 30}};

            rootVisualElement.Add(_inputTextField);
            rootVisualElement.Add(new Button(Execute) {text = "execute"});
            rootVisualElement.Add(_outputTextField);
        }

        private void Execute()
        {
            _outputTextField.value = new Interpreter(_inputTextField.value).Value.StringValue;
        }
    }
}