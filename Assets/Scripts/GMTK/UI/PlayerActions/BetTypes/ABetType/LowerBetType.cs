﻿namespace GMTK.UI.PlayerActions.BetTypes.ABetType
{
    public class LowerBetType : ABetType
    {
        public override float Risk => (DiceFace - 1) * 1 / 6f;
        public override bool IsFaceValid(int _face)
        {
            return _face < DiceFace;
        }
    }
}