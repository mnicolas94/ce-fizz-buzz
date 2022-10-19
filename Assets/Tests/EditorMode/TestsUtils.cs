﻿using Core;
using Core.GameRules;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Tests.EditorMode
{
    public static class TestsUtils
    {
        public static GameController GetGameController(GameRules rules)
        {
            var playerHealth = ScriptableObject.CreateInstance<FloatVariable>();
            var scoreVariable = ScriptableObject.CreateInstance<IntVariable>();
            
            var gameController = new GameController(playerHealth, scoreVariable, rules);

            return gameController;
        }
    }
}