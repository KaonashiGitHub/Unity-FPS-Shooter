using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;
    public float moveDistance = 5f;
    public int HP = 100; // Thay đổi hoặc loại bỏ nếu không cần thiết
    private int maxHP; // Thay đổi hoặc loại bỏ nếu không cần thiết
    private bool movingForward = true;
    private Vector3 startPosition;

    private void Start()
    {
        maxHP = HP; // Thay đổi hoặc loại bỏ nếu không cần thiết
        startPosition = transform.position;
    }

    private void Update()
    {
        if (movingForward && transform.position.z > startPosition.z + moveDistance)
        {
            movingForward = false;
        }
        else if (!movingForward && transform.position.z < startPosition.z - moveDistance)
        {
            movingForward = true;
        }

        if (movingForward)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die(); // Gọi hàm để xử lý cái chết của kẻ địch
        }
    }

    private void Die()
    {
        // Thêm điểm khi kẻ địch bị tiêu diệt
        ScoreManager.Instance.AddScore(10);

        // Hủy đối tượng kẻ địch
        Destroy(gameObject);
    }
}