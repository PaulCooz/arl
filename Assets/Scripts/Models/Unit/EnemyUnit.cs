namespace Models.Unit
{
    public class EnemyUnit : BaseUnit
    {
        private void Awake()
        {
            GameMaster.OnClearOldLevel += UnitDestroy;
        }

        private void OnDestroy()
        {
            GameMaster.OnClearOldLevel -= UnitDestroy;
        }
    }
}