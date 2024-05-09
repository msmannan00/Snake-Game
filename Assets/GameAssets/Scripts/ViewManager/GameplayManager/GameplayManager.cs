using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour, PageController
{
    public TMP_Text aLevel;
    private Camera mCamera;

    public void onInit(Dictionary<string, object> data)
    {
        int mCurrentLevel = PlayerPrefs.GetInt("current_level", 1);
        aLevel.SetText("Level " + mCurrentLevel.ToString());
    }

    public void onLogout()
    {
        Dictionary<string, object> mData = new Dictionary<string, object>
            {
                { AuthKey.sAuthType, AuthConstant.sAuthTypeLogin}
            };
        StateManager.Instance.OpenStaticScreen("auth", gameObject, "authScreen", mData);
    }

    public void onOpenMenu()
    {
        Action callbackSuccess = () =>
        {
            gameObject.transform.parent.SetSiblingIndex(1);
            Dictionary<string, object> mData = new Dictionary<string, object> { };
            StateManager.Instance.OpenStaticScreen("level", gameObject, "levelScreen", null);
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
        alertController.InitController("Your Game Session is paused", callbackSuccess, callbackLevel, pTrigger: "Restart", pHeader:"Game Paused", pSecondaryTrigger : "Level Menu");
        GlobalAnimator.Instance.AnimateAlpha(instantiatedAlert, true);
    }

    public void onLevelFailed()
    {
        Action callbackSuccess = () =>
        {
            gameObject.transform.parent.SetSiblingIndex(1);
            Dictionary<string, object> mData = new Dictionary<string, object> { };
            StateManager.Instance.OpenStaticScreen("gameplay", gameObject, "gameplayScreen", null);
        };
        GameObject alertPrefab = Resources.Load<GameObject>("Prefabs/alerts/alertFinishedFailed");
        GameObject alertsContainer = GameObject.FindGameObjectWithTag("alerts");
        GameObject instantiatedAlert = Instantiate(alertPrefab, alertsContainer.transform);
        AlertController alertController = instantiatedAlert.GetComponent<AlertController>();
        alertController.InitController("Oh no, You have failed this level. lets see you can do it again", callbackSuccess, pTrigger: "Restart", pHeader: "Level Failed");
        GlobalAnimator.Instance.AnimateAlpha(instantiatedAlert, true);
    }

    public void onRestartLevel()
    {
        gameObject.transform.parent.SetSiblingIndex(1);
        Dictionary<string, object> mData = new Dictionary<string, object> { };
        StateManager.Instance.OpenStaticScreen("level", gameObject, "levelScreen", null);
    }

    public void onNextLevel()
    {
        Action callbackSuccess = () =>
        {
            gameObject.transform.parent.SetSiblingIndex(1);
            Dictionary<string, object> mData = new Dictionary<string, object> { };
            StateManager.Instance.OpenStaticScreen("gameplay", gameObject, "gameplayScreen", null);
        };

        GameObject alertPrefab = Resources.Load<GameObject>("Prefabs/alerts/alertFinishedSuccess");
        GameObject alertsContainer = GameObject.FindGameObjectWithTag("alerts");
        GameObject instantiatedAlert = Instantiate(alertPrefab, alertsContainer.transform);
        AlertController alertController = instantiatedAlert.GetComponent<AlertController>();
        alertController.InitController("Awesome, You have finished this level. lets see you can keep your streak", callbackSuccess, pTrigger: "Next Level", pHeader: "Level Complete");
        GlobalAnimator.Instance.AnimateAlpha(instantiatedAlert, true);
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
