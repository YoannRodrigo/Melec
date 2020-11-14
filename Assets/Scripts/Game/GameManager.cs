using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameStates
    {
        GAME,
        INVENTORY
    }

    public static GameManager instance;
    public GameStates gameState;
    public GameObject player;
    private Inventory inventory;

    void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameStates.GAME;
        inventory = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButton("InventoryMode"))
        {
            //Swap game state
            gameState = GameStates.INVENTORY;
            //Pos and show cursor
            inventory.cursor.transform.parent = inventory.uiInventory.transform.Find("Slots").GetChild(inventory.cursorIndex).transform;
            inventory.cursor.GetComponent<RectTransform>().localPosition = Vector3.zero;
            inventory.cursor.SetActive(true);
        }
    }
}
