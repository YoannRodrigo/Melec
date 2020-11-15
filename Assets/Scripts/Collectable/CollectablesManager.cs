using System;
using UnityEngine;

public class CollectablesManager : MonoBehaviour
{
    public static CollectablesManager instance;
    public enum CollectableType {
        ATOM,
        MOLECULE
    }
    
    public enum AtomAbb{
        C,
        CL,
        H,
        SI,
        N,
        O,
        FE,
        NA,
        S,
        UNDEFINED
    }
    
    public enum MoleculeAbb{
        NACL,
        CO,
        HO,
        NO,
        NH,
        CH,
        CCL,
        SIO,
        HH,
        FES,
        CLCL,
        UNDEFINED
    }
    
    public Collectable[] atomsArray;
    public Collectable[] bossAtomsArray;
    public Collectable[] moleculesArray;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
    }

    public void SpawnAtom(AtomAbb atomAbb, Vector3 position){ 
        GameObject goAtomToSpawn = new GameObject();
        Collectable atom = ScriptableObject.CreateInstance<Collectable>();
        foreach(Collectable currentAtom in atomsArray){
            if(currentAtom.atomAbb == atomAbb){
                atom = currentAtom;
            }
        }
        //Apply atom properties
        SpriteRenderer spriteR = goAtomToSpawn.AddComponent<SpriteRenderer>();
        spriteR.sprite = atom.sprite;
        BoxCollider bc = goAtomToSpawn.AddComponent<BoxCollider>();
        bc.isTrigger = true;
        //Add a pickable behavior to spawned atom
        Pickable p = goAtomToSpawn.AddComponent<Pickable>();
        p.atomType = atom;
        //Apply name
        goAtomToSpawn.name = atom.atomAbb.ToString();
        //Apply position
        goAtomToSpawn.transform.position = position;
    }
    
    public Pickable SpawnRareAtom(AtomAbb atomAbb, Vector3 position){ 
        GameObject goAtomToSpawn = new GameObject();
        Collectable atom = ScriptableObject.CreateInstance<Collectable>();
        foreach(Collectable currentAtom in bossAtomsArray){
            if(currentAtom.atomAbb == atomAbb){
                atom = currentAtom;
            }
        }
        //Apply atom properties
        SpriteRenderer spriteR = goAtomToSpawn.AddComponent<SpriteRenderer>();
        spriteR.sprite = atom.sprite;
        BoxCollider bc = goAtomToSpawn.AddComponent<BoxCollider>();
        bc.isTrigger = true;
        //Add a pickable behavior to spawned atom
        Pickable p = goAtomToSpawn.AddComponent<Pickable>();
        p.SetIsRare();
        p.atomType = atom;
        //Apply name
        goAtomToSpawn.name = atom.atomAbb.ToString();
        //Apply position
        goAtomToSpawn.transform.position = position;
        return p;
    }
}
