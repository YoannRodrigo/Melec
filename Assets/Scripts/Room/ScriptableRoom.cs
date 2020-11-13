using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/ScriptableRoom", order = 1)]
public class ScriptableRoom : ScriptableObject
{
    public int nbEnemies;
    public GameObject roomPrefab;

    private List<GameObject> walls = new List<GameObject>();
    private GameObject floor;
    public Transform SpawnRoom(Vector3 position)
    {
        Vector3 newPosition = new Vector3(position.x,position.y,position.z + 9f);
        floor = Instantiate(roomPrefab, newPosition, Quaternion.identity);
        SpawnWalls();
        return floor.transform;
    }

    private void SpawnWalls()
    {
        walls = Resources.LoadAll<GameObject>("Walls/").ToList();
        for (float i = 1.5f; i < 8; i++)
        {
            int randomId = Random.Range(0,walls.Count);
            Instantiate(walls[randomId], new Vector3(floor.transform.position.x + i, 0, floor.transform.position.z - 4f),
                Quaternion.identity, floor.transform);
            Instantiate(walls[randomId], new Vector3(floor.transform.position.x + i, 0, floor.transform.position.z + 4f),
                Quaternion.identity, floor.transform);
            Instantiate(walls[randomId], new Vector3(floor.transform.position.x - i, 0, floor.transform.position.z - 4f),
                Quaternion.identity, floor.transform);
            Instantiate(walls[randomId], new Vector3(floor.transform.position.x - i, 0, floor.transform.position.z + 4f),
                Quaternion.identity, floor.transform);
            
        }
        for (float j = -3; j < 4; j++)
        {
            int randomId = Random.Range(0,walls.Count);
            Instantiate(walls[randomId], new Vector3(floor.transform.position.x + 7.5f, 0, floor.transform.position.z + j),
                Quaternion.identity,
                floor.transform);
            Instantiate(walls[randomId], new Vector3(floor.transform.position.x - 7.5f, 0, floor.transform.position.z + j),
                Quaternion.identity,
                floor.transform);
        }
    }
    
    
}
