using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class WelcomeController : MonoBehaviour, PageController
{
    public TMP_Text aProgress;
    public TMP_Text aProgressShadow;
    public GameObject aProgressPanel;

    public void onInit(Dictionary<string, object> pData)
    {
        int mCurrentLevel = PlayerPrefs.GetInt("current_level", 1)-1;
        aProgress.SetText((Mathf.CeilToInt((mCurrentLevel / 30f) * 100)).ToString()+"%");
        aProgressShadow.SetText((Mathf.CeilToInt((mCurrentLevel / 30f) * 100)).ToString() + "%");
    }

    public void onPlay()
    {
        int mCurrentLevel = PlayerPrefs.GetInt("current_level", 1);
        userSessionManager.Instance.currentLevel = mCurrentLevel;
        Image panelImage = aProgressPanel.GetComponent<Image>();
        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 0);
        aProgressPanel.SetActive(true);

        panelImage.DOFade(1, 0.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(1.0f, () =>
            {
                gameObject.transform.parent.SetSiblingIndex(1);
                Dictionary<string, object> mData = new Dictionary<string, object>();
                StateManager.Instance.OpenStaticScreen("gameplay", gameObject, "gameplayScreen", null);
            });
        });
    }

    public void onLevel()
    {
        gameObject.transform.parent.SetSiblingIndex(1);
        Dictionary<string, object> mData = new Dictionary<string, object>();
        StateManager.Instance.OpenStaticScreen("level", gameObject, "levelScreen", null);
    }
}
