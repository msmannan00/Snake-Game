using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WelcomeController : MonoBehaviour, PageController
{
    public TMP_Text aProgress;
    public TMP_Text aProgressShadow;
    public GameObject aProgressPanel;

    public void onInit(Dictionary<string, object> pData)
    {
        userSessionManager.Instance.currentLevel = PlayerPrefs.GetInt("current_level", 0);
        if (!userSessionManager.Instance.mIsAppStarted)
        {
            userSessionManager.Instance.mIsAppStarted = true;
            aProgress.SetText("0%");
            aProgressShadow.SetText("0%");
            aProgressPanel.SetActive(true);
            AnimateProgress();
        }
    }

    public void onPlay()
    {
        gameObject.transform.parent.SetSiblingIndex(1);
        Dictionary<string, object> mData = new Dictionary<string, object>();
        StateManager.Instance.OpenStaticScreen("gameplay", gameObject, "gameplayScreen", null);
    }

    public void onLevel()
    {
        gameObject.transform.parent.SetSiblingIndex(1);
        Dictionary<string, object> mData = new Dictionary<string, object>();
        StateManager.Instance.OpenStaticScreen("level", gameObject, "levelScreen", null);
    }

    private void AnimateProgress()
    {
        DOTween.To(() => 0, x => UpdateProgress(x), 100, 1).OnComplete(() => FadeOutPanel());
    }

    private void UpdateProgress(int progress)
    {
        aProgress.SetText(progress + "%");
        aProgressShadow.SetText(progress + "%");
    }

    private void FadeOutPanel()
    {
        CanvasGroup canvasGroup = aProgressPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = aProgressPanel.AddComponent<CanvasGroup>();
        }
        canvasGroup.DOFade(0, 1).OnComplete(() => aProgressPanel.SetActive(false));
    }
}
