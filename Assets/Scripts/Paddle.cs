using UnityEngine;

public class Paddle : MonoBehaviour
{
   [SerializeField]
   private float _speed;

   public float horizontalLimitMax, horizontalLimitMin;

   private void Update() {
      Move();
   }

   private void Move() {
      float _move = Input.GetAxis("Horizontal") * _speed;

      float nextPlayerPosition = transform.position.x + _move * Time.deltaTime;
      float clampedX = Mathf.Clamp(nextPlayerPosition, horizontalLimitMin, horizontalLimitMax);

      transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
   }
}