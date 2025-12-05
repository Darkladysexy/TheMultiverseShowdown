using UnityEngine;

public class DownSpecialSasukeSkill : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    private float force = 1f;
    private int damage = 40;
    public GameObject parent;
    private string enemyTag;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        enemyTag = (parent.CompareTag("P1")) ? "P2" : "P1";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(enemyTag))
        {
            PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();

            Vector3 vt3 = (this.gameObject.transform.position + collision.gameObject.transform.position).normalized;
            // enemyAnimator.SetTrigger("TakeDamageFall"); // Không cần nữa, PlayerHealth tự xử lý
            
            // SỬA DÒNG NÀY: Thêm 'true' vì đây là đòn 'Fall'
            enemyHealth.TakeDamage(damage, force, vt3, true);
            parent.GetComponent<PlayerStamina>().IncreaseStamina(damage);

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
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
