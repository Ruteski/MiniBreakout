using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public enum Options
   {
      aspect_16_09,
      aspect_16_10
   }

   [SerializeField]
   private Options limitAspectWindow;

   [SerializeField]
   private Ball ball;

   [SerializeField]
   private GameObject block;

   [SerializeField]
   private Paddle paddle;

   [SerializeField]
   private Transform leftWall;

   [SerializeField]
   private Transform rightWall;

   [SerializeField]
   private TextMeshProUGUI scoreText, lifeText;

   [SerializeField]
   private int scoreToAddBallSpeed;

   private float _horizontalLimitMax, _horizontalLimitMin;

   private bool _isExecCreateBlocks;
   private int _score, _life;
   private float _verticalLimitMax, _verticalLimitMin;

   private void Start() {
      ball.gameObject.SetActive(false);
      ShowScore();
      ShowLife();
      CreateBlocks();
      ApplyLimitAspectWindow();
      Invoke(nameof(ResetGame), 3);
   }

   private void ApplyLimitAspectWindow() {
      switch (limitAspectWindow) {
         case Options.aspect_16_09:
            _horizontalLimitMax = 8.1f;
            _horizontalLimitMin = -8.1f;
            _verticalLimitMax = 9f;
            _verticalLimitMin = -9f;

            break;
         case Options.aspect_16_10:
            _horizontalLimitMax = 7.23f;
            _horizontalLimitMin = -7.23f;
            _verticalLimitMax = 8.11f;
            _verticalLimitMin = -8.11f;

            break;
      }

      paddle.horizontalLimitMin = _horizontalLimitMin;
      paddle.horizontalLimitMax = _horizontalLimitMax;

      leftWall.position = new Vector2(_verticalLimitMin, leftWall.position.y);
      rightWall.position = new Vector2(_verticalLimitMax, rightWall.position.y);
   }

   public void CreateBlocks() {
      for (int y = 0; y < 6; y++) { // qtd de linhas
         for (int x = 0; x < 14; x++) { // qtd de colunas
            float posX = -7.48f + x * 1.15f;
            float posY = 1.9f + y * .4f;
            Instantiate(block, new Vector2(posX, posY), Quaternion.identity);
         }
      }
   }

   private void ResetGame() {
      ball.gameObject.SetActive(true);
      _score = 0;
      _life = 3;

      if (_isExecCreateBlocks) {
         CreateBlocks();
      }

      ResetBall();
      ResetPaddle();
      ShowScore();
      ShowLife();
   }

   public void ResetPaddle() {
      paddle.transform.position = new Vector2(0, paddle.transform.position.y);
   }

   public void ResetBall() {
      ball.transform.position = Vector2.zero;
   }

   private void ShowLife() {
      lifeText.text = "Life: " + _life;
   }

   private void ShowScore() {
      scoreText.text = "Score: " + _score;
   }

   private void Reload() {
      SceneManager.LoadScene(0);
   }

   public void LoseLifeAndVerifyGameOver() {
      _life -= 1;

      // game over
      if (_life < 1) {
         ball.gameObject.SetActive(false);
         _isExecCreateBlocks = true;
         Invoke(nameof(Reload), 3);
      }

      ShowLife();
   }

   public void AddPoint() {
      _score++;
      ShowScore();

      if (_score == scoreToAddBallSpeed) {
         ball.IncrementBallSpeed();
      }

      if (_score >= 84) {
         _isExecCreateBlocks = true;
         Reload();
      }
   }
}