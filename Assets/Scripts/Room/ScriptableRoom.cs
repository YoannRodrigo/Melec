using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/ScriptableRoom", order = 1)]
public class ScriptableRoom : ScriptableObject
{
    public bool isEntrance;
    public bool isEnd;
    public int nbEnemies;
    public GameObject roomPrefab;

    private List<GameObject> walls = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> bosses = new List<GameObject>();
    private GameObject floor;
    private CollectablesManager collectablesManager;
    
    public Transform SpawnRoom(Vector3 position)
    {
        Vector3 newPosition = new Vector3(position.x + 16,position.y,position.z);
        floor = Instantiate(roomPrefab, newPosition, Quaternion.identity);
        SpawnWalls();
        SpawnEnemies();
        return floor.transform;
    }

    private void SpawnWalls()
    {
        walls = Resources.LoadAll<GameObject>("Walls/").ToList();
        for (float i = -7.5f; i < 8; i++)
        {
            int randomId = Random.Range(0,walls.Count);
            Instantiate(walls[randomId], new Vector3(floor.transform.position.x + i, 0, floor.transform.position.z - 4f),
                Quaternion.identity, floor.transform);
            randomId = Random.Range(0,walls.Count);
            Instantiate(walls[randomId], new Vector3(floor.transform.position.x + i, 0, floor.transform.position.z + 4f),
                Quaternion.identity, floor.transform);
        }
        for (float j = -3; j < 4; j++)
        {
            if(j!=0)
            {
                int randomId = Random.Range(0, walls.Count);
                Instantiate(walls[randomId], new Vector3(floor.transform.position.x + 7.5f, 0, floor.transform.position.z + j),
                    Quaternion.identity,
                    floor.transform);
                randomId = Random.Range(0, walls.Count);
                Instantiate(walls[randomId], new Vector3(floor.transform.position.x - 7.5f, 0, floor.transform.position.z + j),
                    Quaternion.identity,
                    floor.transform);
            }
        }

        if (isEntrance)
        {
            int randomId = Random.Range(0,walls.Count);
            Instantiate(walls[randomId], new Vector3(floor.transform.position.x - 7.5f, 0, floor.transform.position.z),
                Quaternion.identity,
                floor.transform);
            SpawnAtoms();
        }
        else if (isEnd)
        {
            int randomId = Random.Range(0,walls.Count);
            Instantiate(walls[randomId], new Vector3(floor.transform.position.x + 7.5f, 0, floor.transform.position.z),
                Quaternion.identity,
                floor.transform);
            SpawnBoss();
        }
    }

    private void SpawnEnemies()
    {
        enemies = Resources.LoadAll<GameObject>("Enemies/").ToList();
        for (int i = 0; i < nbEnemies; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(floor.transform.position.x - 6,floor.transform.position.x + 6),
                0, 
                Random.Range(floor.transform.position.z - 3f, floor.transform.position.z + 3f));
            Instantiate(enemies[Random.Range(0, enemies.Count)], randomPos, Quaternion.identity,floor.transform).SetActive(false);
        }
    }

    private void SpawnBoss()
    {
        bosses = Resources.LoadAll<GameObject>("Bosses/").ToList();
        int randomId = Random.Range(0,bosses.Count);
        Instantiate(bosses[randomId], floor.transform.position, Quaternion.identity,floor.transform).SetActive(false);
    }

    private void SpawnAtoms()
    {
        collectablesManager = FindObjectOfType<CollectablesManager>();
        int randomDropId = Random.Range(0, collectablesManager.atomsArray.Length);
        Debug.Log(randomDropId);
        collectablesManager.SpawnAtom((CollectablesManager.AtomAbb) randomDropId, floor.transform.position + 2*Vector3.right + Vector3.up);
    }
}
