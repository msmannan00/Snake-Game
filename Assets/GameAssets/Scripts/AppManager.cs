using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        if (userSessionManager.Instance.mIsLevelRestart == true)
        {
            userSessionManager.Instance.mIsLevelRestart = false;
            Dictionary<string, object> mData = new Dictionary<string, object>
            {
                { "currentLevel", 1}
            };
            StateManager.Instance.OpenStaticScreen("gameplay", null, "gameplayScreen", mData);
        }
        else {
            StateManager.Instance.OpenStaticScreen("welcome", null, "welcomeScreen", null);
        }
    }
   
}
