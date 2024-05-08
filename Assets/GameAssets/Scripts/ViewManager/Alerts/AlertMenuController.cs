using System;
using TMPro;
using UnityEngine;

public class AlertMenuController : MonoBehaviour
{
    public TMP_Text aHeader;
    public TMP_Text aTrigger;
    public TMP_Text aSecondaryTrigger;
    public TMP_Text aMessage;
    public Action mCallbackSuccess;
    public GameObject aAudioPaused;
    public GameObject aAudioPlay;


    public void InitController(string pMessage, Action pCallbackSuccess = null, string pHeader = "Success", string pTrigger = "Proceed", string pSecondaryTrigger = "Dismiss")
    {
        aHeader.text = pHeader;
        aTrigger.text = pTrigger;
        aMessage.text = pMessage;
        mCallbackSuccess = pCallbackSuccess;

        if (aSecondaryTrigger != null)
        {
            aSecondaryTrigger.text = pSecondaryTrigger;
        }
        if (userSessionManager.Instance.isAudioPlaying)
        {
            aAudioPlay.SetActive(true);
        }
        else
        {
            aAudioPaused.SetActive(true);
        }
    }

    public void OnTriggerPrimary()
    {
        if(mCallbackSuccess != null)
        {
            mCallbackSuccess.Invoke();
        }
        GlobalAnimator.Instance.AnimateAlpha(gameObject, false);
    }

    public void OnTriggerSecondary()
    {
        GlobalAnimator.Instance.AnimateAlpha(gameObject, false);
    }

    public void OnClose()
    {
        GlobalAnimator.Instance.AnimateAlpha(gameObject, false);
    }
}
