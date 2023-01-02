using System;

namespace Core.TurnSteps
{
    /// <summary>
    /// This is an abstraction of a turn step, i.e. an action occured in a game's turn.
    /// </summary>
    [Serializable]
    public abstract class TurnStep
    {
        public override string ToString()
        {
            return GetType().Name;
        }
    }
}