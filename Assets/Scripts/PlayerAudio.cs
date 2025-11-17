using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    [Header("Movement & Damage")]
    public AudioClip runAudio;
    public AudioClip jumpAudio;
    public AudioClip fallAudio;
    public AudioClip takeDamageAudio;
    public AudioClip takeDamageFallAudio;
    public AudioClip dashAudio;

    [Header("Normal Attacks (J)")]
    public AudioClip normalAttack1Audio;
    public AudioClip normalAttack2Audio;
    public AudioClip normalAttack3Audio;

    // === PHẦN MỚI THÊM BẮT ĐẦU TỪ ĐÂY ===

    [Header("Skill Sounds (U, I, O)")]
    public AudioClip lightAttackAudio;      // U (Ground - Kunai)
    public AudioClip heavyAttackAudio;      // I (Ground - Chidori)
    public AudioClip aerialAttackAudio;     // Air U (Kunai)
    public AudioClip airNormalAttackAudio;  // Air J
    public AudioClip substitutionAudio;     // O (Thế thân)

    [Header("Directional Skill Sounds (W+S)")]
    public AudioClip downNormalAttackAudio; // S + J
    public AudioClip downLightAttackAudio;  // S + U (Triệu hồi)
    public AudioClip downHeavyAttackAudio;  // S + I (Special)
    public AudioClip upNormalAttackAudio;   // W + J
    public AudioClip upLightAttackAudio;    // W + U
    public AudioClip upHeavyAttackAudio;    // W + I (Dragon)

    // === KẾT THÚC PHẦN MỚI THÊM ===

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.volume = StaticData.vol; // Giữ nguyên dòng của bạn
    }

    void Update()
    {
        // Không cần gì ở đây
    }

    // (Các hàm Play... cũ của bạn)
    public void PlayRunAudio()
    {
        if (runAudio != null)
            audioSource.PlayOneShot(runAudio);
    }
    public void PlayJumpAudio()
    {
        if (jumpAudio != null)
            audioSource.PlayOneShot(jumpAudio);
    }
    public void PlayFallAudio()
    {
        if (fallAudio != null)
            audioSource.PlayOneShot(fallAudio);
    }
    public void PlayNormalAttackk1() // (Tên hàm "Attackk1" của bạn)
    {
        if (normalAttack1Audio != null)
            audioSource.PlayOneShot(normalAttack1Audio);
    }
    public void PlayNormalAttackk2()
    {
        if (normalAttack2Audio != null)
            audioSource.PlayOneShot(normalAttack2Audio);
    }
    public void PlayNormalAttackk3()
    {
        if (normalAttack3Audio != null)
            audioSource.PlayOneShot(normalAttack3Audio);
    }
    public void PlayTakeDamageAudio()
    {
        if (takeDamageAudio != null)
            audioSource.PlayOneShot(takeDamageAudio);
    }
    public void PlayTakeDamageFallAudio()
    {
        if (takeDamageFallAudio != null)
            audioSource.PlayOneShot(takeDamageFallAudio);
    }
    public void PlayDashAudio()
    {
        if (dashAudio != null)
            audioSource.PlayOneShot(dashAudio);
    }

    // === CÁC HÀM PLAY... MỚI ===

    // Hàm private trợ giúp để gọn gàng hơn
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Các hàm public mới để gọi từ Animation Event
    public void PlayLightAttackAudio() => PlaySound(lightAttackAudio);
    public void PlayHeavyAttackAudio() => PlaySound(heavyAttackAudio);
    public void PlayAerialAttackAudio() => PlaySound(aerialAttackAudio);
    public void PlayAirNormalAttackAudio() => PlaySound(airNormalAttackAudio);
    public void PlaySubstitutionAudio() => PlaySound(substitutionAudio);
    public void PlayDownNormalAttackAudio() => PlaySound(downNormalAttackAudio);
    public void PlayDownLightAttackAudio() => PlaySound(downLightAttackAudio);
    public void PlayDownHeavyAttackAudio() => PlaySound(downHeavyAttackAudio);
    public void PlayUpNormalAttackAudio() => PlaySound(upNormalAttackAudio);
    public void PlayUpLightAttackAudio() => PlaySound(upLightAttackAudio);
    public void PlayUpHeavyAttackAudio() => PlaySound(upHeavyAttackAudio);
}