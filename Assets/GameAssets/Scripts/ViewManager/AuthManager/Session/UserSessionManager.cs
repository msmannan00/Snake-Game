
public class userSessionManager : GenericSingletonClass<userSessionManager>
{
    public string mProfileUsername;
    public string mProfileID;
    public int totalLevel = 30;
    public int currentLevel = 30;
    public bool isAudioPlaying = true;
    public bool mIsCounterRunning = false;
    public bool mIsLevelRestart = false;
    public bool mIsMenuOpened = false;

    public void OnInitialize(string pProfileUsername, string pProfileID)
    {
        this.mProfileUsername = pProfileUsername;
        this.mProfileID = pProfileID;
    }

    public void OnResetSession()
    {
        this.mProfileUsername = null;
        this.mProfileID = null;
    }

}

