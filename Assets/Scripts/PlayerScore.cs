using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        
        [SerializeField]private static int score = 0;
        
        public int GetScore() => score;

        public void AddScore(int value)
        {
            PlayerScore.score += value;
            scoreText.text = PlayerScore.score.ToString();
        }
    }
}