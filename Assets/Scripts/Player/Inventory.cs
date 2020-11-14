using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public CollectablesManager collectablesManager;
    public List<Collectable> inventory = new List<Collectable>();
    public const int MAX_CAPACITY = 7;

    public bool Add(Collectable atomToAdd){
        if (inventory.Count < MAX_CAPACITY) {
            print("Added " + atomToAdd.collectableName + " to Inventory");
            inventory.Add(atomToAdd);
            return true;
        }
        print("Inventory is full.");
        return false;
    }

    public Collectable MergeAtoms(int indexA, int indexB)
    {
        Collectable result = null;
        Collectable a = inventory[indexA];
        Collectable b = inventory[indexB];
        string nameA = a.atomAbb.ToString();
        string nameB = b.atomAbb.ToString();
        int resultLenght = (nameA + nameB).Length;
        foreach (Collectable m in collectablesManager.moleculesArray){
            if (m.molAbb.ToString().Length == resultLenght) {
                if ((nameA + nameB) == m.molAbb.ToString() || (nameB + nameA) == m.molAbb.ToString()) {
                    result = m;
                }
            }
        }
        //Clear B
        inventory[indexB] = null;
        
        //Return result
        if(result != null){
            print("Successfully created " + result.molAbb.ToString() + " by fusing " + nameA + " & " + nameB);
            inventory[indexA] = result;
        }
        else {
            print("EXU-PURO-SIOOOOOOOON ! Fusing " + nameA + " & " + nameB + " didn't worked...");
            //Clear A
            inventory[indexA] = null;
        }

        return result;
    }
}
