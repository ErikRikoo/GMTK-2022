using GMTK.UI.PlayerActions.BetTypes.ABetType;

namespace GMTK.UI.PlayerActions.ActionType
{
    public abstract class APlayerAction
    {
        public ABetType BetType;

        public void ExecuteIfPossible(int _drawnFace, Player _player)
        {
            if (BetType.IsFaceValid(_drawnFace))
            {
                ExecuteOn(_player);
            }
        }

        protected abstract void ExecuteOn(Player player);
    }
}