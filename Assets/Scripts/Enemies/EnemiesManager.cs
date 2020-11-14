using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    private List<EnemyController> enemies = new List<EnemyController>();

    private void Start()
    {
        enemies = GetComponentsInChildren<EnemyController>(true).ToList();
    }

    private void Update()
    {
        enemies.RemoveAll(enemyController => enemyController == null);
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
