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
    public int cursorIndex;
    public GameObject uiInventorySlots;
    public List<int> playerSelection = new List<int>();
    public GameObject atomToMergeA;
    public GameObject atomToMergeB;
    public GameObject mergeSuccess;
    public GameObject mergeFailure;

    private void Start()
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
            cursor.transform.SetParent(uiInventorySlots.transform.GetChild(cursorIndex));
            cursor.GetComponent<RectTransform>().localPosition = Vector3.back;
        }
    }

    public void AddCursorSelection()
    {
        if (Input.GetButton("Submit")) {
            if (playerSelection.Count == 2)
            {
                uiInventorySlots.transform.GetChild(playerSelection[0]).GetChild(0).GetComponent<Image>().color = new Color(255,255,255,1f);
                uiInventorySlots.transform.GetChild(playerSelection[1]).GetChild(0).GetComponent<Image>().color = new Color(255,255,255,1f);
                MergeAtoms(playerSelection[0], playerSelection[1]);
            }
            else {
                if (playerSelection.Count < 2) {
                    if (inventory[cursorIndex].type == CollectablesManager.CollectableType.ATOM)
                    {
                        playerSelection.Add(cursorIndex);
                        uiInventorySlots.transform.GetChild(cursorIndex).GetChild(0).GetComponent<Image>().color = new Color(255,255,255,.6f);
                        switch (playerSelection.Count)
                        {
                            case 1:
                                atomToMergeA.GetComponent<Image>().sprite = mergeElements.GetComponent<AtomsSprites>().atomsArray[inventory[playerSelection[0]].atomAbb];
                                break;
                            case 2:
                                atomToMergeB.GetComponent<Image>().sprite = mergeElements.GetComponent<AtomsSprites>().atomsArray[inventory[playerSelection[1]].atomAbb];
                                break;
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
            AddInInventory(atomToAdd);
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
                if (nameA + nameB == m.molAbb.ToString() || nameB + nameA == m.molAbb.ToString()) {
                    result = m;
                }
            }
        }
        //Clear B
        inventory.RemoveAt(indexB);
        inventory.RemoveAt(indexA);

        Sequence showResult = DOTween.Sequence();
        GameObject activeResultUI;
        
        //Return result
        if(result != null)
        {
            activeResultUI = mergeSuccess;
            mergeSuccess.transform.Find("Result").GetComponent<Image>().sprite = result.sprite;
            inventory[0] = result;
        }
        else {
            activeResultUI = mergeFailure;
        }
        playerSelection.Clear(); 
        activeResultUI.SetActive(true);
        
        //Show result
        showResult.Append(mergeElements.transform.DOScale(0,.5f).SetEase(Ease.InOutSine));
        showResult.Append(activeResultUI.transform.DOScale(1, .5f).SetEase(Ease.InBounce));
        showResult.AppendInterval(1.5f);
        showResult.Append(activeResultUI.transform.DOScale(0, .25f));
        showResult.Append(mergeElements.transform.DOScale(1,.5f).SetEase(Ease.InOutSine));

        showResult.Play();
        
        //reset merger UI
        atomToMergeA.GetComponent<Image>().sprite = mergeElements.GetComponent<AtomsSprites>().blank;
        atomToMergeB.GetComponent<Image>().sprite = mergeElements.GetComponent<AtomsSprites>().blank;

        UpdateUIInventory();

        return result;
    }

    private void AddInInventory(Collectable atomToAdd)
    {
        if(inventory.Count != 0)
        {
            inventory.Add(inventory[inventory.Count-1]);
            for (int i = inventory.Count - 2; i > 0; i--)
            {
                inventory[i] = inventory[i - 1];
            }
            inventory[0] = atomToAdd;
        }
        else
        {
            inventory.Add(atomToAdd);
        }
        
        
    }
}
