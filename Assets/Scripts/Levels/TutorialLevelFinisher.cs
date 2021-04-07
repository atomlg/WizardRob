using UnityEngine;
using UnityEngine.Playables;

namespace Levels
{
    public class TutorialLevelFinisher : LevelFinisher
    {
        private const int StarsCount = 3;
        
        [SerializeField] private PlayableDirector _playableDirector;

        private void OnEnable()
        {
            _playableDirector.stopped += OnPlayableDirectorStopped;
        }

        private void OnDisable()
        {
            _playableDirector.stopped -= OnPlayableDirectorStopped;
        }

        private void OnPlayableDirectorStopped(PlayableDirector playableDirector)
        {
            WonGame(StarsCount);
        }
    }
}
