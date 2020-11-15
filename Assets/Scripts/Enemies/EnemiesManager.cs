using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    private List<EnemyController> enemies = new List<EnemyController>();
    private List<DoorManager> doors = new List<DoorManager>();
    private PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        doors = GetComponentsInChildren<DoorManager>().ToList();
        enemies = GetComponentsInChildren<EnemyController>(true).ToList();
    }

    private void Update()
    {
        enemies.RemoveAll(enemyController => enemyController == null);
        if (enemies.Count == 0)
        {
            if(playerMovement.GetCollectibleAttack())
            {
                foreach (DoorManager doorManager in doors)
                {
                    doorManager.doorController.gameObject.SetActive(false);
                    doorManager.OpenDoor();
                }
            }
            else
            {
                foreach (DoorManager doorManager in doors)
                {
                    doorManager.CloseDoor();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach (EnemyController enemy in enemies)
            {
                enemy.gameObject.SetActive(true);
            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach (EnemyController enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }
    }
}
