namespace GMTK.UI.PlayerActions.BetTypes.ABetType
{
    public class EqualBetType : ABetType
    {
        public override float Risk => 1 / 6f;
        public override bool IsFaceValid(int _face)
        {
            return _face == DiceFace;
        }
    }
}