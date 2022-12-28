using System;
using System.IO;
using Simulations;
using UnityEditor;
using Utils.Editor;

namespace Editor.Simulations
{
    public static class EditorGamesSimulations
    {
        private static readonly string DataFolder = "Assets/Data/Editor/SimulationsResults";
        
        [MenuItem("Simulations/Simulate games")]
        public static async void SimulateGames()
        {
            // get inputs
            var dialogInputContainer = EditorInputDialog.ShowModal<ParametersDialogInput>(
                "Simulation",
                "Set simulation parameters");
            
            // start simulations
            var result = await GamesSimulations.SimulateGames(dialogInputContainer.Parameters);

            // save data
            var dateString = DateTime.Now.ToString("yyyy-M-dd--HH-mm-ss");
            var fileName = $"{dateString}.asset";
            var path = Path.Join(DataFolder, fileName);
            AssetDatabase.CreateAsset(result, path);

            EditorGUIUtility.PingObject(result);
            Selection.activeObject = result;
        }
    }
}