using GMTK.UI.PlayerActions.ActionSetter;
using GMTK.UI.PlayerActions.BetTypes.ABetType;
using UnityEngine;

namespace GMTK.UI.PlayerActions.ActionType
{
    public class ParryActionSetter : AActionSetter
    {
        protected override APlayerAction CreateAction(ABetType bet)
            => new ParryAction()
            {
                BetType = bet
            };
    }
}