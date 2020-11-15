﻿using DG.Tweening;
using UnityEngine;
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
    private const float TIME_BEFORE_CLICK = 1;
    private float timeSinceLastClick = 1;

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
    private void Update() {
        timeSinceLastClick += Time.deltaTime;
        
            ToggleMergerUI();

    }

    private void ToggleMergerUI()
    {
        if (Input.GetButton("InventoryMode"))
        {
            if (timeSinceLastClick > TIME_BEFORE_CLICK)
            {
                switch (gameState)
                {
                    case GameStates.GAME:
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
                        break;
                    case GameStates.INVENTORY:
                        //Darkener
                        inventory.darkener.GetComponent<Image>().DOFade(0f, .5f);
                        //hide Glass
                        inventory.mergeElements.SetActive(false);
                        //hide cursor
                        inventory.cursor.SetActive(false);
                        //Swap game state
                        gameState = GameStates.GAME;
                        break;
                }

                timeSinceLastClick = 0;
            }
        }
    }
}
