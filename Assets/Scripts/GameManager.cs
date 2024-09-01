using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   [SerializeField]
   private Rigidbody2D _ball;

   [SerializeField]
   private Transform _paddle;

   [SerializeField]
   private TextMeshProUGUI _scoreText, _lifeText;

   private int _score, _life;

   private void Start() {
      _ball.gameObject.SetActive(false);
      ShowScore();
      ShowLife();
      Invoke(nameof(ResetGame), 3);
   }

   private void ResetGame() {
      _ball.gameObject.SetActive(true);
      _score = 0;
      _life = 3;

      ResetBall();
      ResetPaddle();
      ShowScore();
      ShowLife();
   }

   public void ResetPaddle() {
      _paddle.transform.position = new Vector2(0, _paddle.transform.position.y);
   }

   public void ResetBall() {
      _ball.transform.position = Vector2.zero;
   }

   private void ShowLife() {
      _lifeText.text = "Life: " + _life;
   }

   private void ShowScore() {
      _scoreText.text = "Score: " + _score;
   }

   public void LoseLifeAndVerifyGameOver() {
      _life -= 1;

      if (_life < 1) {
         _ball.gameObject.SetActive(false);
         Invoke(nameof(ResetGame), 3);
      }

      ShowLife();
   }
}