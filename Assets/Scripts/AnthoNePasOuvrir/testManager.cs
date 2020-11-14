using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class testManager : MonoBehaviour
{

    public CollectablesManager collectablesManager;
    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        int numberOfSpawn = Random.Range(2, 6);
        for (int i = 0; i < numberOfSpawn; i++) {
            collectablesManager.SpawnAtom((CollectablesManager.AtomAbb)Random.Range(0,8), new Vector3(Random.Range(-6f,6f),1f,Random.Range(-2.8f,2.8f)));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.MergeAtoms(inventory.inventory[0], inventory.inventory[1]);
        }
    }
}
