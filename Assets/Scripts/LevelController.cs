using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    class LevelController: MonoBehaviour
    {
        public ChildhoodLevel Childhood;
        public YouthLevel Youth;

        [NonSerialized] public static BaseLevel _currentLevel;

        public static BaseLevel CurrentLevel
        {
            get => _currentLevel;
            set
            {
                _currentLevel = value;
                _currentLevel.EnterLevel();
            }
        }
        void Start()
        {
            DontDestroyOnLoad(this);
            CurrentLevel = Childhood;
            CurrentLevel.EnterLevel();
            BaseLevel.eLevelFinished += ChangeLevel;
        }

        private void ChangeLevel(BaseLevel obj)
        {
            CurrentLevel.ExitLevel();
        }
    }
}
