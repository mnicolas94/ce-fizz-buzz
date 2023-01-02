using System.Collections.Generic;

namespace Simulations.DataExtractors
{
    public interface ISimulationDataExtractor
    {
        Dictionary<string, object> ExtractGamesData(SimulationResult simulationResult);
    }
}