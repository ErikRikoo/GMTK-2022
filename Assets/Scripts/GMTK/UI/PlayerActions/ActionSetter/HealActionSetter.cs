using GMTK.UI.PlayerActions.ActionType;
using GMTK.UI.PlayerActions.BetTypes.ABetType;
using UnityEngine;

namespace GMTK.UI.PlayerActions.ActionSetter
{
    public class HealActionSetter : AActionSetter
    {
        protected override APlayerAction CreateAction(ABetType bet)
        {
            return new HealAction 
            {
                BetType = bet
            };
        }
    }
}