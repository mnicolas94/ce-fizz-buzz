using System;
using System.Collections.Generic;
using Simulations;
using Simulations.DataExtractors;
using TNRD;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Simulations
{
    public class SimulationsDataVisualizerEditorWindow : EditorWindow
    {
        private VisualizationInputs _inputs;
        private SerializedObject _inputsSerializedObject;
        private SerializedProperty _extractorProperty;
        private SerializedProperty _resultsProperty;
        
        [MenuItem("Simulations/Data visualization")]
        private static void ShowWindow()
        {
            var window = GetWindow<SimulationsDataVisualizerEditorWindow>();
            window.titleContent = new GUIContent("Data visualization");
        }

        private void OnEnable()
        {
            Debug.Log("OnEnable");
            _inputs = CreateInstance<VisualizationInputs>();
            _inputsSerializedObject = new SerializedObject(_inputs);
            _extractorProperty = _inputsSerializedObject.FindProperty("_extractor");
            _resultsProperty = _inputsSerializedObject.FindProperty("_results");
        }

        private void CreateGUI()
        {
            var uxmlPath = "Assets/Scripts/Editor/Simulations/" +
                           "VisualizationEditorWindow/SimulationsDataVisualizerEditorWindow.uxml";
            VisualTreeAsset uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            VisualElement ui = uiAsset.Instantiate();
            rootVisualElement.Add(ui);
            
            /**
             * inputs
             * - extractor
             * - simulations: list
             * outputs
             * - data foreach simulation (in a table)
             */
            var inputsLeftLayout = rootVisualElement.Q("InputsLayoutLeft");
            var inputsRightLayout = rootVisualElement.Q("InputsLayoutRight");
            
            var extractorsField = new PropertyField(_extractorProperty);
            var resultsField = new PropertyField(_resultsProperty);
            extractorsField.Bind(_inputsSerializedObject);
            resultsField.Bind(_inputsSerializedObject);
            
            inputsLeftLayout.Add(extractorsField);
            inputsRightLayout.Add(resultsField);
        }
    }

    [Serializable]
    public class VisualizationInputs : ScriptableObject
    {
        [SerializeField] private SerializableInterface<ISimulationDataExtractor> _extractor = new SerializableInterface<ISimulationDataExtractor>();
        [SerializeField] private List<SimulationResult> _results = new List<SimulationResult>();
    }
}