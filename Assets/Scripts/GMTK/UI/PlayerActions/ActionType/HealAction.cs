namespace GMTK.UI.PlayerActions.ActionType
{
    public class HealAction : APlayerAction
    {
        protected override void ExecuteOn(Player player)
        {
            player.Heal(player.var_heal);
        }
    }
}