using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
   public List<Transform> alternativeFireN = new List<Transform>();
   public Transform alternativeFireO;
   private const float SPEED = 10;
   private Rigidbody rb;
   private bool isShooting;
   public Transform shootingPoint;
   public GameObject projectilePrefab;
   private float timeSinceLastShot = 2;
   private const float TIME_BEFORE_SHOT = 0.5f;
   private const float DEFAULT_SPEED_BALL = 4;

   private float speedRate;
   private float projectileSpeed;
   private float projectileSize;
   private int damage;
   
   private Collectable collectableAttack;

   public void SetCollectableAttack(Collectable collectableAttack)
   {
      this.collectableAttack = collectableAttack;
   }

   private void Start()
   {
      rb = GetComponent<Rigidbody>();
   }

   private void Update()
   {
      timeSinceLastShot += Time.deltaTime;
      MovePlayer();
      PlayerShoot();
      UpdateState();
   }

   private void UpdateState()
   {
      if(collectableAttack)
      {
         speedRate = collectableAttack.atomAbb == CollectablesManager.AtomAbb.H ? 0.2f : TIME_BEFORE_SHOT;
         projectileSpeed = collectableAttack.atomAbb == CollectablesManager.AtomAbb.SI ? 7 : DEFAULT_SPEED_BALL;
         projectileSize = collectableAttack.atomAbb == CollectablesManager.AtomAbb.C ? 2 : 1;
         damage = collectableAttack.atomAbb == CollectablesManager.AtomAbb.FE ? 3 : 1;
      }
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
            if (collectableAttack && timeSinceLastShot > speedRate)
            {
               timeSinceLastShot = 0;
               SpawnProjectile(shootingPoint);
               switch (collectableAttack.atomAbb)
               {
                  case CollectablesManager.AtomAbb.N:
                  {
                     foreach (Transform firePoint in alternativeFireN)
                     {
                        SpawnProjectile(firePoint);
                     }

                     break;
                  }
                  case CollectablesManager.AtomAbb.O:
                     SpawnProjectile(alternativeFireO);
                     break;
               }
            }
         }
         else
         {
            isShooting = false;
         }
      }
   }

   private void SpawnProjectile(Transform firePoint)
   {
      GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(firePoint.transform.position - (transform.position + new Vector3(0,firePoint.transform.position.y,0)), Vector3.up));
      bullet.transform.localScale *= projectileSize;
      bullet.GetComponent<Rigidbody>().AddForce(projectileSpeed * transform.forward, ForceMode.Impulse);
      bullet.GetComponent<ProjectileCollision>().SetLauncherInstanceId(gameObject.GetInstanceID());
      bullet.GetComponent<ProjectileCollision>().SetDamage(damage);
      Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
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
      switch (GameManager.instance.gameState)
      {
         case GameManager.GameStates.GAME:
         {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            rb.velocity = new Vector3 (0, rb.velocity.y, 0) + SPEED * movement;
            if (!isShooting)
            {
               RotatePlayer(movement);
            }

            break;
         }
         case GameManager.GameStates.INVENTORY:
            rb.velocity = Vector3.zero;
            break;
      }
   }

   public Collectable GetCollectibleAttack()
   {
      return collectableAttack;
   }
}
