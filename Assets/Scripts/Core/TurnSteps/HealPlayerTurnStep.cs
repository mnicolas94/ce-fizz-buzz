namespace Core.TurnSteps
{
    public class HealPlayerTurnStep : TurnStep
    {
        private float _heal;

        public float Heal => _heal;

        public HealPlayerTurnStep(float heal)
        {
            _heal = heal;
        }
    }
}