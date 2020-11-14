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
    private void Update() 
    {
        if (player && Input.GetButton("InventoryMode"))
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
