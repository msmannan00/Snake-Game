using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveSpiralRoot : MonoBehaviour
{
    public float moveSpeed = 1;
    public Transform BaseTransform;
    public Candy baseCandy;
    [SerializeField]
    public List<Candy> StackedCandies = new List<Candy>();
    public int QueueLenght = 5;
    public static MoveSpiralRoot Instance;

    public int currentQueueLength = 0;
    bool isGameEnd = false;
    int maxSimilarityCount = 0;
    bool mSimilarityCounted = false;


    private void Awake()
    {
        if(Instance == null)
              Instance = this;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        baseCandy.HasAlreadyStacked = true;
        StackedCandies.Add(baseCandy);
    }


    public void onNextLevel()
    {
        Action callbackSuccess = () =>
        {
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach (var gameObject in allObjects)
            {
                if (gameObject.name.Contains("Candy"))
                {
                    Destroy(gameObject);
                }
            }

            int mCurrentLevel = PlayerPrefs.GetInt("current_level", 1);
            if((userSessionManager.Instance.currentLevel+1) >= mCurrentLevel && maxSimilarityCount>2)
            {
                PlayerPrefs.SetInt("current_level", mCurrentLevel + 1);
            }
            GameObject mGameplayScreen = gameObject;
            allObjects = FindObjectsOfType<GameObject>();
            foreach (var gameObject in allObjects)
            {
                if (gameObject.name == "gameplayScreen(Clone)")
                {
                    GameplayManager manager = gameObject.GetComponent<GameplayManager>();
                    if (manager != null)
                    {
                        manager.onRestartLevel();
                    }
                }
            }
        };
        Action callbackSuccessSecondary = () =>
        {
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach (var gameObject in allObjects)
            {
                if (gameObject.name.Contains("Candy"))
                {
                    Destroy(gameObject);
                }
            }

            int mCurrentLevel = PlayerPrefs.GetInt("current_level", 1);
            if ((userSessionManager.Instance.currentLevel + 1) >= mCurrentLevel && maxSimilarityCount > 2)
            {
                PlayerPrefs.SetInt("current_level", mCurrentLevel + 1);
            }
            GameObject mGameplayScreen = gameObject;
            allObjects = FindObjectsOfType<GameObject>();
            foreach (var gameObject in allObjects)
            {
                if (gameObject.name == "gameplayScreen(Clone)")
                {
                    GameplayManager manager = gameObject.GetComponent<GameplayManager>();
                    if (manager != null)
                    {
                        manager.onRestartLevel();
                    }
                }
            }
        };
        if (maxSimilarityCount > 2)
        {
            GameObject alertPrefab = Resources.Load<GameObject>("Prefabs/alerts/alertFinishedSuccess");
            GameObject alertsContainer = GameObject.FindGameObjectWithTag("alerts");
            GameObject instantiatedAlert = Instantiate(alertPrefab, alertsContainer.transform);
            AlertController alertController = instantiatedAlert.GetComponent<AlertController>();
            alertController.InitController("Awesome, You have finished this level. lets see you can keep your streak", callbackSuccessSecondary, callbackSuccess, pTrigger: "Next Level", pHeader: "Level Complete");
            GlobalAnimator.Instance.AnimateAlpha(instantiatedAlert, true);
        }
        else
        {
            GameObject alertPrefab = Resources.Load<GameObject>("Prefabs/alerts/alertFinishedFailed");
            GameObject alertsContainer = GameObject.FindGameObjectWithTag("alerts");
            GameObject instantiatedAlert = Instantiate(alertPrefab, alertsContainer.transform);
            AlertController alertController = instantiatedAlert.GetComponent<AlertController>();
            alertController.InitController("Oh no, You have failed this level. lets see you can do it again", callbackSuccessSecondary, callbackSuccess, pTrigger: "Restart", pHeader: "Level Failed");
            GlobalAnimator.Instance.AnimateAlpha(instantiatedAlert, true);
        }
    }

    void Update()
    {
        if (!userSessionManager.Instance.mIsCounterRunning && !userSessionManager.Instance.mIsMenuOpened)
        {
            if (StackedCandies.Count == 0)
            {
                if (!isGameEnd)
                {
                    onNextLevel();
                }
                isGameEnd = true;
                return;
            }

            currentQueueLength = StackedCandies.Count;
            transform.localPosition -= Vector3.forward * Time.deltaTime * moveSpeed;
            UpdateHead();
        }
    }


    void UpdateHead()
    {
        for (int i = 0; i < StackedCandies.Count; i++)
        {
            if (i == StackedCandies.Count - 1)
                continue;

            StackedCandies[i].FollowLead = StackedCandies[i + 1].transform;
        }

        for (int i = 0; i < StackedCandies.Count; i++)
        {
            if (i == 0)
                continue;

            StackedCandies[i].TailCandy = StackedCandies[i - 1];
        }
    }

    void UpdateZLength()
    {
      
    }

    public void RegisterNewCandy(Candy newCandy)
    {
        if (StackedCandies == null && StackedCandies.Count >= QueueLenght) {

            Debug.LogError("Limit Reached");
            return;
        }
        //  newCandy.UpZAxis = BaseTransform.localPosition.z +  newCandy.width * StackedCandies.Count;
        baseCandy.IsBaseCandy = false;
        newCandy.HasAlreadyStacked = true;
        newCandy.TailCandy = baseCandy;
        baseCandy = newCandy;
        baseCandy.IsBaseCandy = true;
        newCandy.transform.SetParent (this.transform);
        StackedCandies.Add(newCandy);
    }

    public void CandyTrashed(Candy _trashedCandy)
    {
        _trashedCandy.IsTrashed = true;
        if (_trashedCandy.IsBaseCandy)
        {
            if (_trashedCandy.TailCandy)
            {
                _trashedCandy.TailCandy.IsBaseCandy = true;
                baseCandy = _trashedCandy.TailCandy;
            }
                _trashedCandy.FollowLead = null;
        }
        else
        {
            if (_trashedCandy.TailCandy)
            {
                _trashedCandy.TailCandy.FollowLead = _trashedCandy.FollowLead;
            }
        }
        _trashedCandy.IsBaseCandy = false;
        _trashedCandy.transform.parent = null;
        StackedCandies.Remove(_trashedCandy);
    }

    public void CandyBaked(Candy _bakedCandy)
    {
        _bakedCandy.IsBaked = true;
        _bakedCandy.transform.parent = null;

        if (_bakedCandy.IsBaseCandy)
        {
            if (_bakedCandy.TailCandy)
            {
                _bakedCandy.TailCandy.IsBaseCandy = true;
                baseCandy = _bakedCandy.TailCandy;
            }
            _bakedCandy.IsBaseCandy = false;
            _bakedCandy.FollowLead = null;
        }
        else
        {
            if (_bakedCandy.TailCandy)
            {
                _bakedCandy.TailCandy.FollowLead = _bakedCandy.FollowLead;
            }
        }

        StackedCandies.Remove(_bakedCandy);
        CheckLargestCandyGroup();
    }

    private void CheckLargestCandyGroup()
    {
        Dictionary<ColorCode, int> candyCount = new Dictionary<ColorCode, int>();
        foreach (var candy in StackedCandies)
        {
            if (candyCount.ContainsKey(candy.MColorCode))
                candyCount[candy.MColorCode]++;
            else
                candyCount[candy.MColorCode] = 1;
        }

        int maxCount = 0;
        ColorCode mostCommonColorCode = default;
        foreach (var pair in candyCount)
        {
            if (pair.Value > maxCount)
            {
                maxCount = pair.Value;
                mostCommonColorCode = pair.Key;
            }
        }
        if (!mSimilarityCounted)
        {
            maxSimilarityCount = maxCount;
            mSimilarityCounted = true;
        }
        Debug.Log($"Most common color is {mostCommonColorCode} with a count of {maxCount}");
    }

    public int GetCandyIndex(Candy candy)
    {
        for (int i = 0; i < StackedCandies.Count; i++)
        {
            if (StackedCandies[i] == candy)
                return (StackedCandies.Count-1) - i;
        }

        return 1;
    }
    //public Stack<Candy> GetStack()
    //{
    //    return StackedCandies;
    //}

    //[CustomEditor(typeof(MoveSpiralRoot))]
    //public class GameObjectQueueEditor : Editor
    //{
    //    public override void OnInspectorGUI()
    //    {
    //        DrawDefaultInspector();

    //        MoveSpiralRoot queueScript = (MoveSpiralRoot)target;
    //        List<Candy> gameObjectList = new List<Candy>(queueScript.GetStack());

    //        GUILayout.Space(10);
    //        GUILayout.Label("Queue Contents:");

    //        foreach (Candy obj in gameObjectList)
    //        {
    //            EditorGUILayout.ObjectField(obj, typeof(Candy), false);
    //        }
    //    }
    //}

}


