using UnityEngine;

public class CollectablesManager : MonoBehaviour
{
    public enum CollectableType {
        ATOM,
        MOLECULE
    }
    
    public enum AtomAbb{
        H,
        C,
        N,
        CL,
        NA,
        O,
        S,
        SI,
        FE,
        UNDEFINED
    };
    
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
    };
    
    public Collectable[] atomsArray;
    public Collectable[] moleculesArray;

    public void SpawnAtom(AtomAbb atomAbb, Vector3 position){ 
        GameObject goAtomToSpawn = new GameObject();
        Collectable atom = new Collectable();
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
}
