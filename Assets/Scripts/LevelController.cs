using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseLevel
{
    public static event Action<BaseLevel> eLevelFinished;

    protected void InvokeLevelFinished(BaseLevel level)
    {
        eLevelFinished?.Invoke(level);
    }
    public abstract void EnterLevel();
    public abstract void ExitLevel();
}

[Serializable]
public class ChildHoodLevel : BaseLevel
{
    
    [SerializeField] private PlaceSpot[] spotsToFill;

    [SerializeField] private string nextLevel;

    [SerializeField] private GameObject[] dustClouds;

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
        PlaceSpot.ItemPlaced -= UpdateLevelStatus;
        foreach (var placeSpot in spotsToFill)
        {
            placeSpot.Item.gameObject.SetActive(false);
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
}

[Serializable]
class LevelController
{
    public ChildHoodLevel _currentLevel;

    public void Start()
    {
        _currentLevel.EnterLevel();
        BaseLevel.eLevelFinished += ChangeLevel;
    }

    private void ChangeLevel(BaseLevel obj)
    {
        _currentLevel.ExitLevel();
        //SceneManager.LoadScene(obj.nextLevel);
    }
}
