using System.Collections.Generic;
using Core;
using Core.GameRules;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Tests.EditorMode
{
    public static class TestsUtils
    {
        public static GameController GetGameController(IList<Enemy> enemies, GameRules rules)
        {
            var playerHealth = ScriptableObject.CreateInstance<FloatVariable>();
            var scoreVariable = ScriptableObject.CreateInstance<IntVariable>();
            
            var gameController = new GameController(playerHealth, scoreVariable, enemies, rules);

            return gameController;
        }
    }
}