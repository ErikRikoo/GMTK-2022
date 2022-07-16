using GMTK.UI.PlayerActions.ActionType;
using GMTK.UI.PlayerActions.BetTypes.ABetType;
using UnityEngine;

namespace GMTK.UI.PlayerActions.ActionSetter
{
    public class EscapeActionSetter : AActionSetter
    {
        protected override APlayerAction CreateAction(ABetType bet)
        {
            return new EscapeAction
            {
                BetType = bet
            };
        }
    }
}