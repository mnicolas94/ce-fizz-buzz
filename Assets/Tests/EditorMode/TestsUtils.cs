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
            List<ScoreRule> difficulties = null,
            List<Enemy> enemies = null
            )
        {
            playerHealth = playerHealth != null ? playerHealth : ScriptableObject.CreateInstance<FloatVariable>();
            maxHealth = maxHealth != null ? maxHealth : GetTestMaxHealthConstant();
            scoreVariable = scoreVariable != null ? scoreVariable : ScriptableObject.CreateInstance<IntVariable>();
            rules = rules != null ? rules : ScriptableObject.CreateInstance<GameRules>();
            difficulties = difficulties != null ? difficulties : new List<ScoreRule>();
            enemies = enemies != null ? enemies : new List<Enemy>();

            var context = ScriptableObject.CreateInstance<GameContext>();
            context.PlayerHealth = playerHealth;
            context.MaxHealth = maxHealth;
            context.Score = scoreVariable;
            context.GameRules = rules;
            context.DifficultyScoreMilestones = difficulties;
            context.Enemies = enemies;
            
            var gameController = new GameController(context);

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