namespace GMTK.UI.PlayerActions.BetTypes.ABetType
{
    public class LowerBetType : ABetType
    {
        public override float Risk => 1 - (DiceFace - 1) * 1 / 6f;
        public override string DisplayType => "<";

        public override bool IsFaceValid(int _face)
        {
            return _face < DiceFace;
        }
    }
}