using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelItemController : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text aLevel;
    public GameObject aLock;
    public GameObject aLockIcon;
    public GameObject mParent;
    int mCurrentLevel; 
    int mItemLevel;

    void Start()
    {

    }

    void Update()
    {

    }
    public void InitCategory(int pCurrentLevel, int pItemLevel, GameObject pParent)
    {
        mParent = pParent;
        mCurrentLevel = pCurrentLevel;
        mItemLevel = pItemLevel;
        if (mItemLevel >= mCurrentLevel)
        {
            aLock.SetActive(true);
            aLevel.SetText("");
        }
        else
        {
            aLevel.SetText((pItemLevel+1).ToString());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick();
    }

    public void onClick()
    {
        if (mItemLevel >= mCurrentLevel)
        {
            GlobalAnimator.Instance.ShakeObject(aLockIcon);
        }
        else
        {
            userSessionManager.Instance.currentLevel = mItemLevel;
            userSessionManager.Instance.mIsLevelRestart = true;
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }

}