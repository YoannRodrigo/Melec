using UnityEngine;

public class AtomsManager : MonoBehaviour
{

    public enum AtomAbb{
        H,
        C,
        N
    };
    
    public Atom[] atomsArray;

    public void SpawnAtom(AtomAbb atomAbb, Vector3 position){ 
        GameObject go_atomToSpawn = new GameObject();
        Atom atom = new Atom();
        foreach(Atom currentAtom in atomsArray){
            if(currentAtom.abbreviation == atomAbb){
                atom = currentAtom;
            }
        }
        //Apply atom properties
        SpriteRenderer SpriteR = go_atomToSpawn.AddComponent<SpriteRenderer>();
        SpriteR.sprite = atom.sprite;
        BoxCollider bc = go_atomToSpawn.AddComponent<BoxCollider>();
        bc.isTrigger = true;
        //Add a pickable behavior to spawned atom
        Pickable p = go_atomToSpawn.AddComponent<Pickable>();
        p.atomType = atom;
        //Apply name
        go_atomToSpawn.name = atom.abbreviation.ToString();
        //Apply position
        go_atomToSpawn.transform.position = position;
    }
}
