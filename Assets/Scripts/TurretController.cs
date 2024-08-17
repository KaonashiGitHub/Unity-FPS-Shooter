using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurretController : MonoBehaviour
{
    public Transform neck;           // Đối tượng Neck để xoay
    public Transform player;         // Đối tượng Player để theo dõi
    public float rotationSpeed = 10f; // Tốc độ xoay của Neck

    public Transform rightGun;       // Điểm bắn đạn của RightGun
    public Transform leftGun;        // Điểm bắn đạn của LeftGun
    public GameObject bulletPrefab;  // Prefab của viên đạn
    public float fireRate = 3f;      // Tốc độ bắn (số lần bắn mỗi giây)
    public float bulletForce = 200f;  // Lực đẩy của viên đạn

    private float nextFireTime = 0f; // Thời điểm bắn kế tiếp
    private bool playerInRange = false; // Kiểm tra xem Player có trong vùng hay không

    public float maxHealth = 1750f;   // Sức khỏe tối đa của Turret
    private float currentHealth;     // Sức khỏe hiện tại của Turret
    public Slider healthBar;         // Thanh máu trên UI

    public string gameWinSceneName = "Win";

    public AudioClip fireSound;      // Âm thanh phát ra khi bắn
    private AudioSource audioSource; // Component AudioSource

    void Start()
    {
        currentHealth = maxHealth; // Đặt sức khỏe ban đầu
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth; // Đặt giá trị tối đa cho thanh máu
            healthBar.value = currentHealth; // Đặt giá trị ban đầu cho thanh máu
        }
    
        audioSource = GetComponent<AudioSource>(); // Lấy AudioSource component
    }

    void Update()
    {
        if (playerInRange && player != null)
        {
            Vector3 directionToPlayer = player.position - neck.position;
            directionToPlayer.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            neck.rotation = Quaternion.Slerp(neck.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        if (rightGun != null && bulletPrefab != null)
        {
            ShootFromGun(rightGun);
        }

        if (leftGun != null && bulletPrefab != null)
        {
            ShootFromGun(leftGun);
        }

        // Phát âm thanh khi bắn
        if (audioSource != null && fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }

    void ShootFromGun(Transform gunTransform)
{
    // Tạo viên đạn
    GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);

    
    Vector3 targetPosition = new Vector3(player.position.x, player.position.y + 1f, player.position.z);
    
    // Điều chỉnh hướng của viên đạn đến vị trí cao hơn của người chơi
    bullet.transform.LookAt(targetPosition);

    // Lấy thành phần Rigidbody và áp dụng lực đẩy
    Rigidbody rb = bullet.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.AddForce(bullet.transform.forward * bulletForce, ForceMode.Impulse);
    }
}


    // Phương thức xử lý sát thương
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(gameWinSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }
}