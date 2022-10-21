using System.Collections.Generic;
using Core;
using Core.GameRules;
using UnityAtoms.BaseAtoms;
using UnityEditor;
using UnityEngine;

namespace Tests.EditorMode
{
    public static class TestsUtils
    {
        public static GameController GetGameController(
            FloatVariable playerHealth = null,
            FloatConstant maxHealth = null,
            IntVariable scoreVariable = null,
            GameRules rules = null,
            List<Enemy> enemies = null
            )
        {
            playerHealth = playerHealth != null ? playerHealth : ScriptableObject.CreateInstance<FloatVariable>();
            maxHealth = maxHealth != null ? maxHealth : GetTestMaxHealthConstant();
            scoreVariable = scoreVariable != null ? scoreVariable : ScriptableObject.CreateInstance<IntVariable>();
            rules = rules != null ? rules : ScriptableObject.CreateInstance<GameRules>();
            enemies = enemies != null ? enemies : new List<Enemy>();
            
            var gameController = new GameController(playerHealth, maxHealth, scoreVariable, rules, enemies);

            return gameController;
        }

        public static FloatConstant GetTestMaxHealthConstant()
        {
            var path = "Assets/Tests/EditorMode/Data/TestMaxHealth.asset";
            var maxHealth = AssetDatabase.LoadAssetAtPath<FloatConstant>(path);
            return maxHealth;
        }
    }
}