using UnityEngine;

public class StaticAudioManager : GenericSingletonClass<StaticAudioManager>
{

    public void PlayHammerHitSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/HammerHit");
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
       public void PlayCandyBreakSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/CandyBreak");
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    public void playEatingSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/eating_sound");
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
    public void playWaterSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/water-splash");
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
    public void playButtonSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/button");
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
    public void playGoSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/go");
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
    public void playbeepSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/beep");
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
    public void playPaperSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/paper");
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
    public void playSpraySound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/spray");
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
    public void playBonusSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/bonus");
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
    public void playHammerSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/hammer");
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
    public void playPackingSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/packing");
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
    public void playDropSound()
    {
        AudioClip clip = Resources.Load<AudioClip>("SoundAssets/drop");
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
}
