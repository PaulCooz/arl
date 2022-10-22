namespace Models.Unit
{
    public class EnemyUnit : BaseUnit
    {
        private void Awake()
        {
            GameMaster.OnLevelDone += UnitDestroy;
        }

        private void OnDestroy()
        {
            GameMaster.OnLevelDone -= UnitDestroy;
        }
    }
}