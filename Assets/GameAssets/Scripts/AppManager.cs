using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        StateManager.Instance.OpenStaticScreen("welcome", null, "welcomeScreen", null);
    }
   
}
