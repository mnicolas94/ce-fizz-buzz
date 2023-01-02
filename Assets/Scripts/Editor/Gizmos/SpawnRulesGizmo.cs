using UnityEditor;
using UnityEngine;
using View;

namespace Editor.Gizmos
{
    public static class SpawnRulesGizmo
    {
        [DrawGizmo(GizmoType.Selected)]
        public static void DrawGizmo(GameControllerView controllerView, GizmoType gizmoType)
        {
            Handles.color = new Color(1.0f, 0.2f, 0);
            var gameRules = controllerView.GameContext.GameRules;
            var spawnRules = gameRules.SpawnRules;
            
            // draw spawn distances (circles)
            var firstRadius = spawnRules.DistanceToSpawnEnemy;
            var stopRadius = gameRules.HealthRules.DistanceToDamagePlayer;
            var step = gameRules.MoveDistancePerTurn;

            var thickness = 2;
            for (float radius = firstRadius; radius > stopRadius; radius -= step)
            {
                Handles.DrawWireDisc(Vector3.zero, Vector3.forward, radius, thickness);
            }
            
            // draw angular position (lines)
            var angularStep = 360.0f / spawnRules.CircularSectors;
            for (float angle = 0; angle < 360; angle += angularStep)
            {
                var from = Utils.MathUtils.FromPolar(stopRadius, angle);
                var to = Utils.MathUtils.FromPolar(firstRadius, angle);
                // Handles.DrawDottedLine(from, to, thickness);
                Handles.DrawBezier(from, to, from, to, Color.red, null, thickness);
            }
        }
    }
}