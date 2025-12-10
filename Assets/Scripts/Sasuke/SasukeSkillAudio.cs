using UnityEngine;

public class SasukeSkillAudio : MonoBehaviour
{
    private AudioSource audioSource;
    [Header("Down Skill")]
    public AudioClip downNormalAudio;
    public AudioClip downHeavyAudio;
    public AudioClip downSpecialAudio;
    [Header("Up Skill")]
    public AudioClip upNormalAudio1;
    public AudioClip upNormalAudio2;
    public AudioClip upNormalAudio3;
    public AudioClip upSpecialAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    public void PlayDownNormalSkillAudio() => PlaySound(downNormalAudio);
    public void PlayDownHeavySkillAudio() => PlaySound(downHeavyAudio);
    public void PlayDownSpecialSkillAudio() => PlaySound(downSpecialAudio);
    public void PlayUpNormalSkill1Audio() => PlaySound(upNormalAudio1);
    public void PlayUpNormalSkill2Audio() => PlaySound(upNormalAudio2);
    public void PlayUpNormalSkill3Audio() => PlaySound(upNormalAudio3);
    public void PlayUpSpecialSkillAudio() => PlaySound(upSpecialAudio);
    

}
