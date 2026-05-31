using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSoundPlayer : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] soundEffects;
    public AudioClip[] damageSoundEffects;
    public AudioClip[] eDamageSoundEffects;

    private AudioSource audioSource;
    private int lastIndex = -1;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    public void PlayRandomSound()
    {
        if (soundEffects.Length == 0) return;

        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, soundEffects.Length);
        }
        while (soundEffects.Length > 1 && randomIndex == lastIndex);

        lastIndex = randomIndex;
        audioSource.PlayOneShot(soundEffects[randomIndex]);
    }

    public void PlayRandomDamageSound()
    {
        if (damageSoundEffects.Length == 0) return;

        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, damageSoundEffects.Length);
        }
        while (damageSoundEffects.Length > 1 && randomIndex == lastIndex);

        lastIndex = randomIndex;
        audioSource.PlayOneShot(damageSoundEffects[randomIndex]);
    }

    public void PlayRandomEDamageSound()
    {
        if (eDamageSoundEffects.Length == 0) return;

        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, eDamageSoundEffects.Length);
        }
        while (eDamageSoundEffects.Length > 1 && randomIndex == lastIndex);

        lastIndex = randomIndex;
        audioSource.PlayOneShot(eDamageSoundEffects[randomIndex]);
    }
}