using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private SerializableInterface<ISimulationDataExtractor> _extractor = new SerializableInterface<ISimulationDataExtractor>();
        [SerializeField] private List<SimulationResult> _results = new List<SimulationResult>();

        private Label _resultsLabel;
        
        [MenuItem("Simulations/Data visualization")]
        private static void ShowWindow()
        {
            var window = GetWindow<SimulationsDataVisualizerEditorWindow>();
            window.titleContent = new GUIContent("Data visualization");
        }
        
        private void CreateGUI()
        {
            SetupUxml();

            // input fields
            var extractorsField = rootVisualElement.Q("ExtractorPropertyField");
            var resultsField = rootVisualElement.Q("ResultsPropertyField");
            var so = new SerializedObject(this);
            extractorsField.Bind(so);
            resultsField.Bind(so);
            
            // compute data fields
            var computeButton = rootVisualElement.Q<Button>("ComputeButton");
            computeButton.clicked += ComputeData;

            // visualization fields
            _resultsLabel = rootVisualElement.Q<Label>("ResultsLabel");
        }

        private void SetupUxml()
        {
            var uxmlPath = "Assets/Scripts/Editor/Simulations/" +
                           "VisualizationEditorWindow/SimulationsDataVisualizerEditorWindow.uxml";
            VisualTreeAsset uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            VisualElement ui = uiAsset.Instantiate();
            rootVisualElement.Add(ui);
        }

        private void ComputeData()
        {
            var results = _results.Select(simulationResult =>
            {
                var data = _extractor.Value.ExtractGamesData(simulationResult);
                return (result: simulationResult, data);
            });
            
            VisualizeData(results.ToList());
        }

        private void VisualizeData(List<(SimulationResult, Dictionary<string, object>)> data)
        {
            var columnHeaders = new List<string> { "Names\t\t\t" };
            columnHeaders.AddRange(data[0].Item2.Keys);
            string columns = string.Join("\t", columnHeaders);
            
            var strings = data.Select(tuple =>
            {
                var (simulationResult, simulationData) = tuple;
                var dataValues = simulationData.Values;

                var strData = string.Join(";\t\t", dataValues);

                var str = $"{simulationResult.name}: \t{strData}";
                return str;
            }).ToList();

            strings.Insert(0, columns);
            
            _resultsLabel.text = string.Join("\n", strings);
        }
    }

    public class VisualizationInputs : ScriptableObject
    {
        [SerializeField] private SerializableInterface<ISimulationDataExtractor> _extractor = new SerializableInterface<ISimulationDataExtractor>();
        [SerializeField] private List<SimulationResult> _results = new List<SimulationResult>();

        public SerializableInterface<ISimulationDataExtractor> Extractor => _extractor;

        public List<SimulationResult> Results => _results;
    }
}