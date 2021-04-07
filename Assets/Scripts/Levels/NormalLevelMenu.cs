namespace Levels
{
    public class NormalLevelMenu : LevelMenu
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            StarsCollector.Collected += OnStarsCollected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            StarsCollector.Collected -= OnStarsCollected;
        }
    }
}
