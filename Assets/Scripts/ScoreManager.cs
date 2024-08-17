using UnityEngine;
using TMPro; // Thêm thư viện TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int playerScore = 0; // Điểm hiện tại của người chơi
    public TMP_Text scoreText; // Tham chiếu đến TMP Text để hiển thị điểm

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
        UpdateScoreUI(); // Cập nhật hiển thị điểm trên UI
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = playerScore.ToString();
        }
    }
}
