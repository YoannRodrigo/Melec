using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
   private const float SPEED = 10;
   private Rigidbody rb;
   private bool isShooting;
   public Transform shootingPoint;
   public GameObject projectilePrefab;
   private float timeSinceLastShot = 2;
   private const float TIME_BEFORE_SHOT = 0.5f;

   private void Start()
   {
      rb = GetComponent<Rigidbody>();
   }

   private void Update()
   {
      timeSinceLastShot += Time.deltaTime;
      MovePlayer();
      PlayerShoot();
   }

   private void PlayerShoot()
   {
      if (GameManager.instance.gameState == GameManager.GameStates.GAME)
      {
         Vector3 shootDirection = new Vector3(Input.GetAxis("FireHorizontal"), 0, Input.GetAxis("FireVertical"));
         if (shootDirection != Vector3.zero)
         {
            isShooting = true;
            RotatePlayer(shootDirection);
            if (timeSinceLastShot > TIME_BEFORE_SHOT)
            {
               timeSinceLastShot = 0;
               GameObject bullet = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.identity);
               bullet.GetComponent<Rigidbody>().AddForce(4 * transform.forward, ForceMode.Impulse);
               bullet.GetComponent<ProjectileCollision>().SetLauncherInstanceId(gameObject.GetInstanceID());
               Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
            }
         }
         else
         {
            isShooting = false;
         }
      }
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
      if (GameManager.instance.gameState == GameManager.GameStates.GAME) {
         Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
         rb.velocity = new Vector3 (0, rb.velocity.y, 0) + SPEED * movement;
         if (!isShooting)
         {
            RotatePlayer(movement);
         }
      }
   }   
}
