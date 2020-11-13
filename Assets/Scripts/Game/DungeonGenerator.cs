using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    private List<ScriptableRoom> rooms = new List<ScriptableRoom>();
    private Vector3 lastPos = new Vector3(0,0,-9f);

    private void Awake()
    {
        rooms = Resources.LoadAll<ScriptableRoom>("Rooms/").ToList();
    }

    private void Start()
    {
        GenerateRooms();
    }

    private void GenerateRooms()
    {
        foreach (ScriptableRoom room in rooms)
        {
            lastPos = room.SpawnRoom(lastPos).position;
        }
    }
}
