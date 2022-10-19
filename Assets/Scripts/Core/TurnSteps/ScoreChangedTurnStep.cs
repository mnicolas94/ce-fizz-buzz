namespace Core.TurnSteps
{
    public class ScoreChangedTurnStep : TurnStep
    {
        private int _scoreChange;

        public int ScoreChange => _scoreChange;

        public ScoreChangedTurnStep(int scoreChange)
        {
            _scoreChange = scoreChange;
        }
    }
}