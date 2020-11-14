using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class DungeonGenerator : MonoBehaviour
{
    public SoundManager soundManager;
    private List<ScriptableRoom> rooms = new List<ScriptableRoom>();
    private readonly Queue<ScriptableRoom> dungeonRooms = new Queue<ScriptableRoom>();
    private Vector3 lastPos = new Vector3(-16f,0,0);
    private readonly Random rng = new Random();

    private void Awake()
    {
        rooms = Resources.LoadAll<ScriptableRoom>("Rooms/").ToList();
        ShuffleList();
    }

    private void Start()
    {
        GenerateRooms();
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.InitAnalyzer();
        soundManager.StartRandomMusic();
        soundManager.AnalyzeCurrentClip();
        soundManager.CreateEventProviderOnCurrentSoundWithOffset();
    }

    private void GenerateRooms()
    {
        CreateRoomsSequence();
        int maxCount = dungeonRooms.Count;
        for (int i = 0; i < maxCount; i++)
        {
            lastPos = dungeonRooms.Dequeue().SpawnRoom(lastPos).position;
        }
    }

    private void CreateRoomsSequence()
    {
        foreach (ScriptableRoom room in rooms.Where(room => room.isEntrance))
        {
            dungeonRooms.Enqueue(room);
            rooms.Remove(room);
            break;
        }

        foreach (ScriptableRoom room in rooms.Where(room => !room.isEnd))
        {
            dungeonRooms.Enqueue(room);
        }
        
        foreach (ScriptableRoom room in rooms.Where(room => room.isEnd))
        {
            dungeonRooms.Enqueue(room);
            break;
        }
    }

    private void ShuffleList()
    {
        int n = rooms.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            ScriptableRoom value = rooms[k];
            rooms[k] = rooms[n];
            rooms[n] = value;
        }
    }
}
