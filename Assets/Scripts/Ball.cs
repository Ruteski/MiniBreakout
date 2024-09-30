using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
   [SerializeField]
   private GameManager _gameManager;

   [SerializeField]
   private float _iniBallSpeed;

   private float _ballSpeed;

   private Vector2 _direction;

   private void Start() {
      BallDirection();
      _ballSpeed = _iniBallSpeed;
   }

   private void Update() {
      transform.Translate(_direction * (_ballSpeed * Time.deltaTime));
   }

   private void OnCollisionEnter2D(Collision2D other) {
      if (other.gameObject.CompareTag("DownWall")) {
         _gameManager.LoseLifeAndVerifyGameOver();
         _gameManager.ResetPaddle();

         if (isActiveAndEnabled) {
            StartCoroutine(ResetBall());
         }
      }

      if (other.gameObject.CompareTag("HorizontalWall")) {
         _direction = new Vector2(-_direction.x, _direction.y);
      }

      if (other.gameObject.CompareTag("UpWall")) {
         _direction = new Vector2(_direction.x, -_direction.y);
      }

      if (other.gameObject.CompareTag("Paddle")) {
         _direction = new Vector2(_direction.x, -_direction.y);
      }

      if (other.gameObject.CompareTag("Block")) {
         _direction = new Vector2(_direction.x, -_direction.y);
         _gameManager.AddPoint();
         Destroy(other.gameObject);
      }
   }

   private IEnumerator ResetBall() {
      yield return new WaitForSeconds(3f);
      _gameManager.ResetBall();
   }

   private void BallDirection() {
      float _x = Random.Range(-1, 1);
      float _y = Random.Range(-1, 1);

      while (_x == 0) _x = Random.Range(-1f, 1f);

      while (_y == 0) _y = Random.Range(-1f, 1f);

      _direction = new Vector2(_x, _y);
   }

   public void IncrementBallSpeed() {
      _ballSpeed += 1;
   }
}