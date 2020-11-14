using UnityEngine;

public class CollectablesManager : MonoBehaviour
{

    public enum AtomAbb{
        H,
        C,
        N,
        CL,
        NA,
        O,
        S,
        SI,
        FE
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
        CLCL
    };
    
    public Atom[] atomsArray;
    public Molecule[] moleculesArray;

    public void SpawnAtom(AtomAbb atomAbb, Vector3 position){ 
        GameObject goAtomToSpawn = new GameObject();
        Atom atom = new Atom();
        foreach(Atom currentAtom in atomsArray){
            if(currentAtom.abbreviation == atomAbb){
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
        goAtomToSpawn.name = atom.abbreviation.ToString();
        //Apply position
        goAtomToSpawn.transform.position = position;
    }
}
