using System;
using TMPro;
using UnityEngine;

public class AlertController : MonoBehaviour
{
    public TMP_Text aTrigger;
    public TMP_Text aSecondaryTrigger;
    public Action mCallbackSuccess;
    public Action mCallbackSuccessSecondary;


    public void InitController(string pMessage, Action pCallbackSuccessSecondary = null, Action pCallbackSuccess = null, string pHeader = "Success", string pTrigger = "Proceed", string pSecondaryTrigger = "Dismiss")
    {
        mCallbackSuccessSecondary = pCallbackSuccessSecondary;
        mCallbackSuccess = pCallbackSuccess;

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
        mCallbackSuccessSecondary.Invoke();
    }

    public void OnClose()
    {
        GlobalAnimator.Instance.AnimateAlpha(gameObject, false);
    }
}
