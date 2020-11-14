using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
   private const float SPEED = 10;
   private Rigidbody rb;
   private bool isShooting = false;
   
   private void Start()
   {
      rb = GetComponent<Rigidbody>();
   }

   private void Update()
   {
      MovePlayer();
   }

   private void RotatePlayer(Vector3 movement)
   {
      if(movement != Vector3.zero)
      {
         transform.rotation = Quaternion.LookRotation(movement);
      }
   }

   private void MovePlayer()
   {
      Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
      rb.transform.position += movement * (SPEED * Time.deltaTime);
      if (!isShooting)
      {
         RotatePlayer(movement);
      }
   }   
}
