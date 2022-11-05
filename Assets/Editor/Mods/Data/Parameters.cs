using System;
using Common;
using UnityEditor;
using UnityEngine;

namespace Editor.Mods.Data
{
    [Serializable]
    public struct Parameter
    {
        [Serializable]
        public enum Types
        {
            Number,
            String,
            Boolean,
            Script
        }

        public string name;
        public Types type;
        public string description;

        public Pair<string, string>[] scriptContext;
    }

    [CreateAssetMenu]
    public class Parameters : ScriptableObject
    {

        [SerializeField]
        private Parameter[] parameters;

        public static Parameter[] Units => GetParameters("UnitParameters");

        private static Parameter[] GetParameters(in string file)
        {
            return AssetDatabase.LoadAssetAtPath<Parameters>($"Assets/Editor/Mods/{file}.asset").parameters;
        }
    }
}