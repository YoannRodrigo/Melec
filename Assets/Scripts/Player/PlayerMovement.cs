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
   
   public Collectable collectableAttack;

   private void Start()
   {
      rb = GetComponent<Rigidbody>();
   }

   private void Update()
   {
      if (GetComponent<Inventory>().inventory.Count > 0)
      {
         collectableAttack = GetComponent<Inventory>().inventory[0];
      }
      timeSinceLastShot += Time.deltaTime;
      MovePlayer();
      PlayerShoot();
      UpdateState();
   }

   private void UpdateState()
   {
      if(collectableAttack)
      {
         speedRate = collectableAttack.molAbb == CollectablesManager.MoleculeAbb.HH 
            ? .1f :
                     collectableAttack.atomAbb == CollectablesManager.AtomAbb.H || 
                     collectableAttack.molAbb == CollectablesManager.MoleculeAbb.HO || 
                     collectableAttack.molAbb == CollectablesManager.MoleculeAbb.NH || 
                     collectableAttack.molAbb == CollectablesManager.MoleculeAbb.CH 
            ? 0.2f : TIME_BEFORE_SHOT;
         
         projectileSpeed = collectableAttack.atomAbb == CollectablesManager.AtomAbb.S ? 7 : DEFAULT_SPEED_BALL;
         
         
         projectileSize = collectableAttack.molAbb == CollectablesManager.MoleculeAbb.CLCL 
            ? 3 : 
                     collectableAttack.atomAbb == CollectablesManager.AtomAbb.CL ||
                     collectableAttack.molAbb == CollectablesManager.MoleculeAbb.CCL
            ? 2 : 1;
         
         
         damage = collectableAttack.atomAbb == CollectablesManager.AtomAbb.C ||
                  collectableAttack.molAbb == CollectablesManager.MoleculeAbb.CO ||
                  collectableAttack.molAbb == CollectablesManager.MoleculeAbb.CH ||
                  collectableAttack.molAbb == CollectablesManager.MoleculeAbb.CCL
            ? 3 : 1;
         
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
               if (collectableAttack.atomAbb == CollectablesManager.AtomAbb.N || collectableAttack.molAbb == CollectablesManager.MoleculeAbb.NH)
               {
                  foreach (Transform firePoint in alternativeFireN)
                  {
                     SpawnProjectile(firePoint);
                  }
               }
               else if (collectableAttack.atomAbb == CollectablesManager.AtomAbb.O || 
                        collectableAttack.molAbb == CollectablesManager.MoleculeAbb.HO || 
                        collectableAttack.molAbb == CollectablesManager.MoleculeAbb.CO || 
                        collectableAttack.molAbb == CollectablesManager.MoleculeAbb.NO)
               {
                  SpawnProjectile(alternativeFireO);
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
