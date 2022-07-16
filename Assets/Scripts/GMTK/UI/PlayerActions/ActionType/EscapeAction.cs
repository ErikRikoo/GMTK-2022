namespace GMTK.UI.PlayerActions.ActionType
{
    public class EscapeAction : APlayerAction
    {
        protected override void ExecuteOn(Player player)
        {
            player.escape();
        }
    }
}