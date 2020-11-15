using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    public void Init()
    {
        gameState = GameStates.GAME;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        inventory = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    private void Update() {
        ToggleMergerUI();

    }

    private void ToggleMergerUI()
    {
        if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1)
        {
            switch (gameState)
            {
                case GameStates.GAME:
                    Sequence showMerger = DOTween.Sequence();
                    //Swap game state
                    gameState = GameStates.INVENTORY;

                    showMerger.Append(inventory.darkener.GetComponent<Image>().DOFade(.8f, .3f));
                    showMerger.Append(inventory.mergeElements.transform.DOScale(1.2f, .3f).SetEase(Ease.InOutSine));

                    showMerger.Play();
                    
                    //Pos and show cursor
                    inventory.cursor.transform.SetParent(inventory.uiInventory.transform.Find("Slots").GetChild(inventory.cursorIndex).transform);
                    inventory.cursor.GetComponent<RectTransform>().localPosition = Vector3.zero;
                    inventory.cursor.SetActive(true);
                    break;
                case GameStates.INVENTORY:
                    //Darkener
                    inventory.darkener.GetComponent<Image>().DOFade(0f, .2f);
                    //hide Glass
                    inventory.mergeElements.transform.DOScale(0,.2f).SetEase(Ease.InOutSine);
                    //hide cursor
                    inventory.cursor.SetActive(false);
                    //Swap game state
                    //Clear merger
                    inventory.playerSelection.Clear();
                    inventory.atomToMergeA.GetComponent<Image>().sprite = inventory.mergeElements.GetComponent<AtomsSprites>().blank;;
                    inventory.atomToMergeB.GetComponent<Image>().sprite = inventory.mergeElements.GetComponent<AtomsSprites>().blank;;
                    gameState = GameStates.GAME;
                    break;
            }
        }
    }
}
