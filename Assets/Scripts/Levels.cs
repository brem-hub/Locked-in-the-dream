using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    [Serializable]
    public class ChildhoodLevel : BaseLevel
    {

        [SerializeField] private PlaceSpot[] spotsToFill;

        [SerializeField] private GameObject[] dustClouds;

        [SerializeField] private UIFader _flasher;

        [SerializeField] private GameObject[] _monsters;

        [SerializeField] public string _levelName = "childhood";
        
        private BaseLevel _nextLevel = new YouthLevel();

        public override string LevelName { get => LevelName; set => _levelName = value; }

        public override void EnterLevel()
        {
            foreach (var cloud in dustClouds)
            {
                cloud.SetActive(false);
            }

            foreach (var placeSpot in spotsToFill)
            {
                placeSpot.gameObject.SetActive(false);
            }

            PlaceSpot.ItemPlaced += UpdateLevelStatus;
        }

        public override void ExitLevel()
        {

            _flasher.StableStartCoroutine(StartFinishScene());
            
            foreach (var placeSpot in spotsToFill)
            {
                placeSpot.Item.gameObject.SetActive(false);
            }

            foreach (var monster in _monsters)
            {
                monster.SetActive(false);
            }
        }

        public void UpdateLevelStatus(PlaceSpot a)
        {
            if (SpotsAreFilled())
            {
                foreach (var cloud in dustClouds)
                {
                    cloud.SetActive(true);
                }
                Debug.Log("LEVEL IS FINISHED!!!!!");
                InvokeLevelFinished(this);
            }
        }

        public bool SpotsAreFilled()
        {
            foreach (var placeSpot in spotsToFill)
            {
                if (!placeSpot.IsItemPlaced || placeSpot.Item == null)
                    return false;
            }
            return true;
        }

        IEnumerator StartFinishScene()
        {
            _flasher.FadeIn();

            yield return new WaitForSeconds(2);
            PlaceSpot.ItemPlaced -= UpdateLevelStatus;

            _flasher.StartCoroutine(FadeOut());

        }
        IEnumerator FadeOut()
        {
            _flasher.FadeOut();
            
            yield return new WaitForSeconds(3);
            
            LevelController.CurrentLevel = _nextLevel;

        }

    }


    [Serializable]
    public class YouthLevel : BaseLevel
    {
        [SerializeField] string _levelName = "youth";

        public override string LevelName { get => _levelName; set => _levelName = value; }

        public override void EnterLevel()
        {
            Debug.Log("YEAA");
            SceneManager.LoadScene(LevelController.CurrentLevel.LevelName);
        }

        public override void ExitLevel()
        {
            //throw new NotImplementedException();
        }
    }

}