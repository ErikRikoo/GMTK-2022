namespace GMTK.UI.PlayerActions.BetTypes.ABetType
{
    
    public abstract class ABetType
    {
        public int DiceFace;

        public abstract float Risk
        {
            get;
        }

        public abstract string DisplayType { get; }

        public abstract bool IsFaceValid(int _face);
    }
}