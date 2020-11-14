using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public CollectablesManager collectablesManager;
    public List<Atom> inventory = new List<Atom>();

    public void Add(Atom atomToAdd){
        print("Added " + atomToAdd + " to Inventory");
        inventory.Add(atomToAdd);
    }

    public Molecule MergeAtoms(Atom a, Atom b)
    {
        Molecule result = null;
        string nameA = a.abbreviation.ToString();
        string nameB = b.abbreviation.ToString();
        int resultLenght = (nameA + nameB).Length;
        foreach (Molecule m in collectablesManager.moleculesArray){
            if (m.abbreviation.ToString().Length == resultLenght) {
                if (nameA + nameB == m.abbreviation.ToString() || nameB + nameA == m.abbreviation.ToString()) {
                    result = m;
                }
            }
        }
        //Return result
        if(result != null){
            print("Successfully created " + result.abbreviation + " by fusing " + nameA + " & " + nameB);
        }
        else {
            print("EXU-PURO-SIOOOOOOOON ! Fusing " + nameA + " & " + nameB + " didn't worked...");
        }

        return result;
    }
}
