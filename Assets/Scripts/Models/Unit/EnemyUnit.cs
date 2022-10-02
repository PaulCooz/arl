namespace Models.Unit
{
    public class EnemyUnit : BaseUnit
    {
        override public void Initialization()
        {
            base.Initialization();

            GameMaster.OnLevelDone += Die;
        }

        protected override void Die()
        {
            base.Die();

            GameMaster.OnLevelDone -= Die;
        }
    }
}