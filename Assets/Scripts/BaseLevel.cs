using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public abstract class BaseLevel
    {
        public static event Action<BaseLevel> eLevelFinished;

        public abstract string LevelName { get; set; }
        protected void InvokeLevelFinished(BaseLevel level)
        {
            eLevelFinished?.Invoke(level);
        }
        public abstract void EnterLevel();
        public abstract void ExitLevel();
    }

}