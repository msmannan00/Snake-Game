using Unity.VisualScripting;
using UnityEngine;

public class StaticAudioManager : GenericSingletonClass<StaticAudioManager>
{
    public void playEatingSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/eating_sound");
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogError("Audio clip not found!");
        }
    }
    public void playWaterSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/water-splash");
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogError("Audio clip not found!");
        }
    }
    public void playPaperSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/paper");
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogError("Audio clip not found!");
        }
    }
    public void playSpraySound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/spray");
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogError("Audio clip not found!");
        }
    }
    public void playHammerSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/hammer");
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogError("Audio clip not found!");
        }
    }
    public void playPackingSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/packing");
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogError("Audio clip not found!");
        }
    }
    public void playDropSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/drop");
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogError("Audio clip not found!");
        }
    }
}
