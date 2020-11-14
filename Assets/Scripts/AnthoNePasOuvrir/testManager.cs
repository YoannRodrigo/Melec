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
    void Awake()
    {
        collectablesManager.SpawnAtom(CollectablesManager.AtomAbb.H, new Vector3(Random.Range(-7f,7f),1f,Random.Range(-5f,5f)));
        collectablesManager.SpawnAtom(CollectablesManager.AtomAbb.C, new Vector3(Random.Range(-7f,7f),1f,Random.Range(-5f,5f)));
        collectablesManager.SpawnAtom(CollectablesManager.AtomAbb.N, new Vector3(Random.Range(-7f,7f),1f,Random.Range(-5f,5f)));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.MergeAtoms(inventory.inventory[0], inventory.inventory[1]);
        }
    }
}
