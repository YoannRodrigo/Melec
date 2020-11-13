using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/ScriptableRoom", order = 1)]
public class ScriptableRoom : ScriptableObject
{
    public int nbEnemies;
    public GameObject roomPrefab;

    public Transform SpawnRoom(Vector3 position)
    {
        Vector3 newPosition = new Vector3(position.x,position.y,position.z + 15f);
        return Instantiate(roomPrefab, newPosition, Quaternion.identity).transform;
    }
}
