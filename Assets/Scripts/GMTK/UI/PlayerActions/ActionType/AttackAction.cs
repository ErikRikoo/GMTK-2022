namespace GMTK.UI.PlayerActions.ActionType
{
    public class AttackAction : APlayerAction
    {
        public Enemies Enemy;
        
        protected override void ExecuteOn(Player player)
        {
            player.Attack(Enemy);
        }

        public override string ToString()
        {
            return Enemy.name;
        }
    }
}