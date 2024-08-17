using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingDoor : MonoBehaviour
{
    private Animator doorAnimator;
    [SerializeField] private int requiredScore = 30; // Điểm số yêu cầu để mở cửa

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager scoreManager = ScoreManager.Instance; // Lấy tham chiếu đến ScoreManager

            if (scoreManager != null)
            {
                if (scoreManager.playerScore >= requiredScore)
                {
                    doorAnimator.SetTrigger("Open");
                }
                else
                {
                    // Có thể thêm một thông báo không đủ điểm nếu cần
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("Closed");
        }
    }
}

