using GMTK.UI.PlayerActions.BetTypes.ABetType;
using UnityEngine;

namespace GMTK.UI.PlayerActions.ActionType
{
    public abstract class APlayerAction
    {
        public ABetType BetType;

        public void ExecuteIfPossible(int _drawnFace, Player _player)
        {
            if (BetType.IsFaceValid(_drawnFace))
            {
                RiskGain(_player, BetType.Risk);
                Debug.Log("damage = "+_player.damage+" heal : "+_player.var_heal + " score = "+_player.score);
                ExecuteOn(_player);
            }
        }

        protected abstract void ExecuteOn(Player player);

        public void RiskGain(Player player_, float risk)
        {
           
            if (risk < 0.2) // le plus petit risque
            {
                player_.damage = 1;
                player_.var_heal = 1;
                player_.score += 0;
            }
            else if (risk < 0.4)
            {
                player_.damage = 2;
                player_.var_heal = 2;
                player_.score += 5;
            }
            else if (risk < 0.55)
            {
                player_.damage = 3;
                player_.var_heal = 3;
                player_.score += 10;
            }
            else if (risk < 0.7)
            {
                player_.damage = 4;
                player_.var_heal = 4;
                player_.score += 100;
            }
            else
            {
                player_.damage = 5;
                player_.var_heal = 5;
                player_.score += 1000;
            }

        }
    }
}