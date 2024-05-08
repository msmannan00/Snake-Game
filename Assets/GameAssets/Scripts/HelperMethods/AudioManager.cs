using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void onButtonClick(AudioSource pAudioSource)
    {
        string soundName = "button";
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/" + soundName);
        pAudioSource.clip = clip;
        pAudioSource.Play();
    }
}
