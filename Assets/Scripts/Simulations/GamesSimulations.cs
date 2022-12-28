using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Serializables;
using UnityEngine;
using View;

namespace Simulations
{
    public static class GamesSimulations
    {
        public static async Task<SimulationResult> SimulateGames(SimulationInputParameters parameters)
        {
            var context = parameters.Context;
            var ai = parameters.AI;
            var gameController = new GameController(context);
            var games = new List<SerializableGame>();
            
            // for each game
            for (int i = 0; i < parameters.GamesCount; i++)
            {
                //   create serializable game
                var serializableGame = new SerializableGame(context);
                
                //   start game
                var firstTurn = gameController.StartGame();
                var sturn = new SerializableTurn(firstTurn);
                serializableGame.AddTurn(sturn);

                int turnCount = 0;
                bool gameLost = context.PlayerHealth.Value == 0;  // should be false
                while (turnCount < parameters.MaxTurns && !gameLost)
                {
                    // play turn
                    var (enemy, shotClass) = ai.InferAction(context);
                    var turnSteps = gameController.PlayTurn(enemy, shotClass);
                    var serializedTurnSteps = new SerializableTurn(turnSteps);
                    serializableGame.AddTurn(serializedTurnSteps);

                    turnCount++;
                    gameLost = context.PlayerHealth.Value == 0;

                    await Task.Yield();
                }

                games.Add(serializableGame);
                Debug.Log($"Simulated game {i + 1}. Turns: {turnCount}");
            }

            var result = SimulationResult.Create(parameters, games);
            return result;
        }
    }
}