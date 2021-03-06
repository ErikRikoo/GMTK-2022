namespace GMTK.UI.PlayerActions.BetTypes.ABetType
{
    public class GreaterBetType : ABetType
    {
        public override float Risk => DiceFace * 1 / 6f;
        public override string DisplayType => ">";

        public override bool IsFaceValid(int _face)
        {
            return _face > DiceFace;
        }
    }
}