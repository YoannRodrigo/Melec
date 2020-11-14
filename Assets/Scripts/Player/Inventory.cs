using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public const int MAX_CAPACITY = 7;
    public CollectablesManager collectablesManager;
    public GameObject uiInventory;
    public Sprite blankFiller;
    public List<Collectable> inventory = new List<Collectable>();

    // UI Functions

    public void UpdateUIInventory()
    {
        GameObject uiInventorySlots = uiInventory.transform.Find("Slots").gameObject;
        for(int i=0;i<MAX_CAPACITY;i++) {
            if (i <inventory.Count) {
                uiInventorySlots.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = inventory[i].sprite;
            }else {
                uiInventorySlots.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = blankFiller;
            }
        }
    }


    // Logical functions
    public bool Add(Collectable atomToAdd){
        if (inventory.Count < MAX_CAPACITY) {
            print("Added " + atomToAdd.collectableName + " to Inventory");
            inventory.Add(atomToAdd);
            UpdateUIInventory();
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
        inventory.RemoveAt(indexB);
        
        //Return result
        if(result != null){
            print("Successfully created " + result.molAbb.ToString() + " by fusing " + nameA + " & " + nameB);
            inventory[indexA] = result;
        }
        else {
            print("EXU-PURO-SIOOOOOOOON ! Fusing " + nameA + " & " + nameB + " didn't worked...");
            //Clear A
            inventory.RemoveAt(indexA);
        }
        UpdateUIInventory();
        return result;
    }
}
