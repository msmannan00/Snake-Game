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

    public void onPlayBackgroundMusic(GameObject pToggleAudio)
    {
        pToggleAudio.SetActive(true);
        gameObject.SetActive(false);
        userSessionManager.Instance.isAudioPlaying = true;

        GameObject audioSourceObject = GameObject.FindWithTag("globalAudioSource");
        audioSourceObject.GetComponent<AudioSource>().enabled = true;
    }
    public void onPauseBackgroundMusic(GameObject pToggleAudio)
    {
        pToggleAudio.SetActive(true);
        gameObject.SetActive(false);
        userSessionManager.Instance.isAudioPlaying = false;

        GameObject audioSourceObject = GameObject.FindWithTag("globalAudioSource");
        audioSourceObject.GetComponent<AudioSource>().enabled = false;
    }
}
