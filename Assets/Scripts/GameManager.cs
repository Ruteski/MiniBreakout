using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   [SerializeField]
   private Rigidbody2D _ball;

   [SerializeField]
   private GameObject _block;

   [SerializeField]
   private Transform _paddle;

   [SerializeField]
   private TextMeshProUGUI _scoreText, _lifeText;

   private int _score, _life;

   private void Start() {
      _ball.gameObject.SetActive(false);
      ShowScore();
      ShowLife();
      CreateBlocks();
      Invoke(nameof(ResetGame), 3);
   }

   private void CreateBlocks() {
      Vector3 posIni = new(-8.15f, 4f, 0);
      Vector3 pos = Vector3.zero;
      GameObject obj;

      for (int i = 0; i < 6; i++) { // qtd de linhas
         pos = new Vector3(pos.x, pos.y + 5f * i, pos.z);
         print("antes: " + pos);

         for (int j = 0; j < 14; j++) { // qtd de blocos por linha
            if (j == 0) {
               obj = Instantiate(_block, posIni, transform.rotation);
               print("depois j=0: " + pos);
               pos = obj.transform.position;
            } else {
               pos = new Vector3(pos.x + 1.25f, pos.y, pos.z);
               print("depois j!=0: " + pos);
               obj = Instantiate(_block, pos, transform.rotation);
            }
         }
      }
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