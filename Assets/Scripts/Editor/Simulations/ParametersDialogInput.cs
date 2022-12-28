using Simulations;
using UnityEngine;

namespace Editor.Simulations
{
    public class ParametersDialogInput : ScriptableObject
    {
        [SerializeField] private SimulationInputParameters _parameters;

        public SimulationInputParameters Parameters => _parameters;
    }
}