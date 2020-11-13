using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomsManager : MonoBehaviour
{

    public enum AtomAbb{
        H,
    };

    public static AtomsManager _instance = null;
    public GameObject atomPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnAtom(AtomAbb atom, Vector3 position){ 
        return Instantiate(atomPrefab, position, Quaternion.identity);
    }
}
