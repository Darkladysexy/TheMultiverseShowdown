using UnityEngine;

public class DownSpecialSasukeSkill : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    private float force = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("P1") || collision.gameObject.CompareTag("P2"))
        {
            PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();

            Vector3 vt3 = (this.gameObject.transform.position + collision.gameObject.transform.position).normalized;
            // enemyAnimator.SetTrigger("TakeDamageFall"); // Không cần nữa, PlayerHealth tự xử lý
            
            // SỬA DÒNG NÀY: Thêm 'true' vì đây là đòn 'Fall'
            enemyHealth.TakeDamage(40, force, vt3, true);

            GameManager.instant.PauseGame(collision.gameObject.transform.position);
            CameraManager.instant.StartShake(0.1f, 0.1f,this.transform);
        }
        Debug.Log("DownSpecial");
    }
    public void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
    public void PlayAudioClip()
    {
        if(audioSource != null) audioSource.PlayOneShot(audioClip);
    }
}
