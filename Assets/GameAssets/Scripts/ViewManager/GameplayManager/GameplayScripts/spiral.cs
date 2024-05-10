using UnityEngine;

public class spiral : MonoBehaviour
{
    public bool CanMakeCandyRot;
    bool isBonusLevelStarted = false;


    private void OnTriggerEnter(Collider other)
    {
        Candy _candy = other.GetComponent<Candy>();

        if (_candy)
        {
            _candy.IsCandyRoten = CanMakeCandyRot;
            _candy.MColorCode = ColorCode.Roten;
        }
        if (!isBonusLevelStarted)
        {
            isBonusLevelStarted = true; 
            StaticAudioManager.Instance.playBonusSound();
            userSessionManager.Instance.mIsBonusLevelStarted = true;
        }
    }
}
