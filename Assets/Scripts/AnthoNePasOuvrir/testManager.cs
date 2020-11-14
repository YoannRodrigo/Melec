using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testManager : MonoBehaviour
{

    public AtomsManager atomsManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = atomsManager.SpawnAtom(AtomsManager.AtomAbb.H, new Vector3(Random.Range(-7f,7f),1f,Random.Range(-7f,7f)));
        GameObject h = atomsManager.SpawnAtom(AtomsManager.AtomAbb.C, new Vector3(Random.Range(-7f,7f),1f,Random.Range(-7f,7f)));
        GameObject i = atomsManager.SpawnAtom(AtomsManager.AtomAbb.N, new Vector3(Random.Range(-7f,7f),1f,Random.Range(-7f,7f)));
    }
}
