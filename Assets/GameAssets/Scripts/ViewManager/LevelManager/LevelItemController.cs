using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelItemController : MonoBehaviour
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

    public void onClick()
    {
        if (mItemLevel >= mCurrentLevel)
        {
            GlobalAnimator.Instance.ShakeObject(aLockIcon);
        }
        else
        {
            Dictionary<string, object> mData = new Dictionary<string, object>
            {
                { "currentLevel", mItemLevel}
            };
            StateManager.Instance.OpenStaticScreen("gameplay", mParent, "gameplayScreen", mData);
        }
    }

}