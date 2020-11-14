using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomsManager : MonoBehaviour
{

    public enum AtomAbb{
        H,
        C,
        N
    };
    
    public Atom[] atomsArray;
    public GameObject atomPrefab;

    public GameObject SpawnAtom(AtomAbb atom, Vector3 position){ 
        GameObject atomToSpawn;
        foreach(Atom a in atomsArray){
            if(a.abbreviation == atom){
                atomToSpawn = a.overworldGO;
                atomToSpawn.GetComponent<Renderer>().material = a.material;
            }
        }
        return Instantiate(atomPrefab, position, Quaternion.identity);
    }
}
