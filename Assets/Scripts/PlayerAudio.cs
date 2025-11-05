using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;
    public AudioClip runAudio;
    public AudioClip jumpAudio;
    public AudioClip fallAudio;
    public AudioClip normalAttack1Audio;
    public AudioClip normalAttack3Audio;
    public AudioClip normalAttack2Audio;
    public AudioClip takeDamageAudio;
    public AudioClip takeDamageFallAudio;
    public AudioClip dashAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void PlayRunAudio()
    {
        if (runAudio != null)
        {
            audioSource.PlayOneShot(runAudio);
        }
    }
    public void PlayJumpAudio()
    {
        if (jumpAudio != null)
        {
            audioSource.PlayOneShot(jumpAudio);
        }
    }
    public void PlayFallAudio()
    {
        if (fallAudio != null)
        {
            audioSource.PlayOneShot(fallAudio);
        }
    }
    public void PlayNormalAttackk1()
    {
        if (normalAttack1Audio != null)
        {
            audioSource.PlayOneShot(normalAttack1Audio);
        }
    }
    public void PlayNormalAttackk2()
    {
        if (normalAttack2Audio != null)
        {
            audioSource.PlayOneShot(normalAttack2Audio);
        }
    }
    public void PlayNormalAttackk3()
    {
        if (normalAttack3Audio != null)
        {
            audioSource.PlayOneShot(normalAttack3Audio);
        }
    }
    public void PlayTakeDamageAudio()
    {
        if (takeDamageAudio != null)
        {
            audioSource.PlayOneShot(takeDamageAudio);
        }
    }
    public void PlayTakeDamageFallAudio()
    {
        if (takeDamageFallAudio != null)
        {
            audioSource.PlayOneShot(takeDamageFallAudio);
        }
    }
    public void PlayDashAudio()
    {
        if(dashAudio != null)
        {
            audioSource.PlayOneShot(dashAudio);
        }
    }
}
