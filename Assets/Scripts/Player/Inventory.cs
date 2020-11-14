using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<Atom> inventory = new List<Atom>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(Atom atomToAdd){
        print("Added " + atomToAdd + " to Inventory");
        inventory.Add(atomToAdd);
    }

/*     public void Remove(Atom atomToRemove){
        
    } */
}
