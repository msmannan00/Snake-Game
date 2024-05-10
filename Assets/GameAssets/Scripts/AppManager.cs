using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        PlayerPrefs.DeleteAll();
        if (!PreferenceManager.Instance.GetBool("WelcomeScreensShown_v3"))
        {
            StateManager.Instance.OpenStaticScreen("welcome", null, "welcomeScreen", null);
        }
        else
        {
            Dictionary<string, object> mData = new Dictionary<string, object>
            {
                { AuthKey.sAuthType, AuthConstant.sAuthTypeLogin}
            };
            StateManager.Instance.OpenStaticScreen("auth", null, "authScreen", mData);
        }
    }
}
