using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour, PageController
{
    public GameObject aCounter;
    public TMP_Text aCounterValue;

    public GameObject aTutorial;
    public TMP_Text aMessage;

    private bool isRunning;
    private Camera mCamera;
    private int mCount = 1;

    public void onInit(Dictionary<string, object> data)
    {
        userSessionManager.Instance.mIsCounterRunning = true;
        StartTimer();
    }

    public void StartTimer()
    {
        if (!isRunning)
        {
            isRunning = true;
            StaticAudioManager.Instance.playButtonSound();
            StartCoroutine(RunEverySecond());
        }
    }

    public void ShowAndHideTutorial()
    {
        CanvasGroup canvasGroup = aTutorial.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = aTutorial.AddComponent<CanvasGroup>();
        }

        aTutorial.SetActive(true);
        canvasGroup.alpha = 0;

        canvasGroup.DOFade(1, 1.0f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(1.0f, () =>
            {
                canvasGroup.DOFade(0, 1.0f).OnComplete(() =>
                {
                    aTutorial.SetActive(false);
                });
            });
        });
    }

    public void onDisableCounter()
    {
        CanvasGroup canvasGroup = aCounter.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = aCounter.AddComponent<CanvasGroup>();
        }
        canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            aCounter.SetActive(false);
            userSessionManager.Instance.mIsCounterRunning = false;
        });
    }

    private IEnumerator RunEverySecond()
    {
        while (isRunning)
        {
            if (mCount < 4 && aCounterValue != null)
            {
                StaticAudioManager.Instance.playbeepSound();
                if (mCount == 0)
                {
                    aCounterValue.text = (1).ToString();
                }
                else
                {
                    aCounterValue.text = (mCount).ToString();
                }
            }
            if (mCount == 4)
            {
                StaticAudioManager.Instance.playGoSound();
                aCounterValue.text = "GO";
            }
            if (mCount == 5)
            {
                onDisableCounter();
            }
            if (userSessionManager.Instance.currentLevel < 1)
            {
                if (mCount == 5)
                {
                    aMessage.SetText("Drag your finger on first\r\ncandy and move in right\r\ndirection!");
                    ShowAndHideTutorial();
                }
                if (mCount == 9)
                {
                    aMessage.SetText("Be aware of sugar free \r\npoison! It would destroy\r\ncandy :(");
                    ShowAndHideTutorial();
                }
                if (mCount == 16)
                {
                    aMessage.SetText("Be aware of pepper! It \r\nmakes your candy not \r\neatable :(");
                    ShowAndHideTutorial();
                }
                if (mCount == 23)
                {
                    aMessage.SetText("Be aware of cooking hum-\r\nmer!  It will destroy your\r\ncandies :( ");
                    ShowAndHideTutorial();
                }
                if (mCount == 27)
                {
                    aMessage.SetText("Magic portal will transfer\r\nyou to additional level! ");
                    ShowAndHideTutorial();
                }
                if (mCount == 30)
                {
                    aMessage.SetText("Your main purpose make\r\ncombinations of 3x simi-\r\nlar candies for 30 sec. ");
                    ShowAndHideTutorial();
                }
                if (mCount == 38)
                {
                    aMessage.SetText("Not eatable candy will\r\ngo in trash bin :(");
                    ShowAndHideTutorial();
                }
            }
            mCount += 1;
            yield return new WaitForSeconds(1);
        }
    }

    public void onOpenMenu()
    {
        Action callbackSuccess = () =>
        {
            userSessionManager.Instance.mIsLevelRestart = true;
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        };

        Action callbackLevel = () =>
        {
            gameObject.transform.parent.SetSiblingIndex(1);
            Dictionary<string, object> mData = new Dictionary<string, object> { };
            StateManager.Instance.OpenStaticScreen("level", gameObject, "levelScreen", null);
        };

        GameObject alertPrefab = Resources.Load<GameObject>("Prefabs/alerts/alertMenu");
        GameObject alertsContainer = GameObject.FindGameObjectWithTag("alerts");
        GameObject instantiatedAlert = Instantiate(alertPrefab, alertsContainer.transform);
        AlertMenuController alertController = instantiatedAlert.GetComponent<AlertMenuController>();
        alertController.InitController("Your Game Session is paused", callbackLevel, callbackSuccess, pTrigger: "Restart", pHeader:"Game Paused", pSecondaryTrigger : "Level Menu");
        GlobalAnimator.Instance.AnimateAlpha(instantiatedAlert, true);
    }

   
    public void onRestartLevel()
    {
        userSessionManager.Instance.mIsLevelRestart = true;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }


    void Start()
    {
        mCamera = GameObject.Find("gameMainCamera").GetComponent<Camera>();
    }

    void OnDestroy()
    {
        if (GameObject.Find("gameMainCamera") != null)
        {
            mCamera = GameObject.Find("gameMainCamera").GetComponent<Camera>();
            if (mCamera != null)
            {
                mCamera.enabled = true;
            }
        }
    }

    void Update()
    {
        if (mCamera != null)
        {
            mCamera.enabled = false;
        }
    }
}
