using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

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
    private const float TIME_BEFORE_CLICK = .1f;
    private float timeSinceLastClick = .1f;

    private void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    public void Init()
    {
        gameState = GameStates.GAME;
        player = GameObject.FindWithTag("Player");
        inventory = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update() {
        timeSinceLastClick += Time.deltaTime;
        if (timeSinceLastClick > TIME_BEFORE_CLICK)
        {
            ToggleMergerUI();
            timeSinceLastClick = 0;
        }

    }

    public void ToggleMergerUI()
    {
        if (Input.GetButton("InventoryMode") && gameState == GameStates.GAME) {
            //Swap game state
            gameState = GameStates.INVENTORY;
            //Darkener
            inventory.darkener.GetComponent<Image>().DOFade(.8f, .5f);
            //show Glass
            inventory.mergeElements.SetActive(true);
            //Pos and show cursor
            inventory.cursor.transform.parent = inventory.uiInventory.transform.Find("Slots").GetChild(inventory.cursorIndex).transform;
            inventory.cursor.GetComponent<RectTransform>().localPosition = Vector3.zero;
            inventory.cursor.SetActive(true);
        }else if(Input.GetButton("InventoryMode") && gameState == GameStates.INVENTORY) {
            //Darkener
            inventory.darkener.GetComponent<Image>().DOFade(0f, .5f);
            //hide Glass
            inventory.mergeElements.SetActive(false);
            //hide cursor
            inventory.cursor.SetActive(false);
            //Swap game state
            gameState = GameStates.GAME;
        }
    }
}
