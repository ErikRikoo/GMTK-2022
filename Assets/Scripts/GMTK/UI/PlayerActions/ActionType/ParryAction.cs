namespace GMTK.UI.PlayerActions.ActionType
{
    public class ParryAction : APlayerAction
    {
        protected override void ExecuteOn(Player player)
        {
            player.Parry();
        }
    }
}