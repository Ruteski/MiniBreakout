using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Paddle : MonoBehaviour
{
   [SerializeField]
   private float _speed;
   
   [SerializeField]
   private float _horizontalLimitMax, _horizontalLimitMin;

   void Update() {
      Move();
   }

   private void Move() {
      float _move = Input.GetAxis("Horizontal") * _speed;
      
      float nextPlayerPosition = transform.position.x + _move * Time.deltaTime;
      float clampedX = Mathf.Clamp(nextPlayerPosition, _horizontalLimitMin, _horizontalLimitMax);
      
      transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
   }
}
