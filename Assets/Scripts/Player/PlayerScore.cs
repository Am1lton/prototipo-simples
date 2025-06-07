using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        
        private static int score = 0;
        
        public void AddScore(int value)
        {
            score += value;
            scoreText.text = score.ToString();
        }
    }
}