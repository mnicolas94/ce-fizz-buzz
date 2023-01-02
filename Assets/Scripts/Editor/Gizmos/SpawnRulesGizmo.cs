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
            var color = new Color(1.0f, 0.2f, 0);
            var textColor = new Color(0.8f, 0.0f, 0.5f);
            Handles.color = color;
            var gameRules = controllerView.GameContext.GameRules;
            var spawnRules = gameRules.SpawnRules;
            
            // draw spawn distances (circles)
            var firstRadius = spawnRules.DistanceToSpawnEnemy;
            var stopRadius = gameRules.HealthRules.DistanceToDamagePlayer;
            var step = gameRules.MoveDistancePerTurn;

            if (step == 0)
            {
                return;  // do not draw gizmo if step is zero
            }

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
            
            // draw arc distances
            var firstAngle = 0;
            var secondAngle = angularStep;
            var labelStyle = new GUIStyle(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            labelStyle.normal.textColor = textColor;
            
            for (float radius = firstRadius; radius > stopRadius; radius -= step)
            {
                var from = Utils.MathUtils.FromPolar(radius, firstAngle);
                var to = Utils.MathUtils.FromPolar(radius, secondAngle);
                var fromTo = to - from;
                var distance = fromTo.magnitude;
                var center = (fromTo / 2) + from;
                Handles.Label(center, $"{distance:0.###}", labelStyle);
            }
        }
    }
}