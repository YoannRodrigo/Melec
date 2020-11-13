using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
   private const float SPEED = 3;
   private Rigidbody rb;
   
   private void Start()
   {
      rb = GetComponent<Rigidbody>();
   }

   private void Update()
   {
      MovePlayer();
   }

   private void MovePlayer()
   {
      Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
      rb.transform.position += movement * (SPEED * Time.deltaTime);
   }   
}
