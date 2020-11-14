using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Inventory : MonoBehaviour
{
    public const int MAX_CAPACITY = 7;
    public CollectablesManager collectablesManager;
    public GameObject uiInventory;
    public Sprite blankFiller;
    public GameObject mergeElements;
    public GameObject darkener;
    public GameObject cursor;
    public List<Collectable> inventory = new List<Collectable>();
    private const float TIME_BEFORE_MOVE = 0.1f;
    private float timeSinceLastMove = 1f;
    private const float TIME_BEFORE_SUBMIT = 0.1f;
    private float timeSinceLastSubmit = 1f;
    public int cursorIndex = 0;
    public GameObject uiInventorySlots;
    public List<int> playerSelection = new List<int>();
    public GameObject atomToMergeA;
    public GameObject atomToMergeB;

    void Start()
    {
        uiInventorySlots = uiInventory.transform.Find("Slots").gameObject;
    }

    private void Update()
    {
        timeSinceLastMove += Time.deltaTime;
        timeSinceLastSubmit += Time.deltaTime;
        if (GameManager.instance.gameState == GameManager.GameStates.INVENTORY)
        {
            if (timeSinceLastMove > TIME_BEFORE_MOVE)
            {
                MoveCursor();
                timeSinceLastMove = 0;
            }
            if (timeSinceLastSubmit > TIME_BEFORE_SUBMIT)
            {
                AddCursorSelection();
                timeSinceLastSubmit = 0; 
            }
        }
    }

    // UI Functions

    public void UpdateUIInventory()
    {
        for(int i=0;i<MAX_CAPACITY;i++) {
            if (i <inventory.Count) {
                uiInventorySlots.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = inventory[i].sprite;
            }else {
                uiInventorySlots.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = blankFiller;
            }
        }
    }

    public void MoveCursor()
    {
        if (Input.GetAxis("InventoryMove") != 0)
        {
            int direction = (int)Mathf.Sign(Input.GetAxis("InventoryMove"));
            cursorIndex -= direction;
            cursorIndex = Mathf.Clamp(cursorIndex, 0, MAX_CAPACITY-1);
            cursor.transform.parent = uiInventorySlots.transform.GetChild(cursorIndex);
            cursor.GetComponent<RectTransform>().localPosition = Vector3.back;
        }
    }

    public void AddCursorSelection()
    {
        if (Input.GetButton("Submit")) {
            print(playerSelection.Count);
            if (playerSelection.Count == 2)
            {
                MergeAtoms(playerSelection[0], playerSelection[1]);
            }
            else {
                if (playerSelection.Count < 2) {
                    if (inventory[cursorIndex].type == CollectablesManager.CollectableType.ATOM) {
                        playerSelection.Add(cursorIndex);
                        if (playerSelection.Count == 1) {
                            atomToMergeA.GetComponent<Image>().sprite = mergeElements.GetComponent<AtomsSprites>().atomsArray[inventory[cursorIndex].atomAbb];
                        }else if (playerSelection.Count == 2) {
                            atomToMergeB.GetComponent<Image>().sprite = mergeElements.GetComponent<AtomsSprites>().atomsArray[inventory[cursorIndex].atomAbb];
                        }
                    }
                    else {
                        print("Can't fuse an already fused Molecule");
                    }  
                }
            }
        }
    }


    // Logical functions
    public bool Add(Collectable atomToAdd){
        if (inventory.Count < MAX_CAPACITY) {
            //print("Added " + atomToAdd.collectableName + " to Inventory");
            inventory.Add(atomToAdd);
            UpdateUIInventory();
            return true;
        }
        //print("Inventory is full.");
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
        playerSelection.Clear();
        
        //SHOW UI RESULT
        
        UpdateUIInventory();
        return result;
    }
}
