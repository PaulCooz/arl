namespace Models.Unit
{
    public class EnemyUnit : BaseUnit
    {
        protected override void Awake()
        {
            base.Awake();

            GameMaster.OnLevelDone += Die;
        }

        protected override void Die()
        {
            base.Die();

            GameMaster.OnLevelDone -= Die;
        }
    }
}