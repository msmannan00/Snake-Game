using UnityEngine;

public class spiral : MonoBehaviour
{
    public bool CanMakeCandyRot;
    bool isBonusLevelStarted = false;


    private void OnTriggerEnter(Collider other)
    {
        if (!isBonusLevelStarted)
        {
            isBonusLevelStarted = true;
            StaticAudioManager.Instance.playBonusSound();
            userSessionManager.Instance.mIsBonusLevelStarted = true;
        }
    }
}
