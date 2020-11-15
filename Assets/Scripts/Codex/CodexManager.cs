using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CodexManager : MonoBehaviour
{
    // Start is called before the first frame update
    public enum codexStates
    {
        OPEN,
        CLOSED
    }

    public codexStates codexState;
    public GameObject codexUI;
    public GameObject unlocks;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        
        codexState = codexStates.CLOSED;
    }

    // Update is called once per frame
    void Update()
    {
        if (!codexUI)
        {
            codexUI = FindObjectOfType<codexHelper>().gameObject; 
        }
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (codexState == codexStates.CLOSED)
            {
                UpdateUICodex();
                codexUI.transform.DOScale(1, .5f).SetEase(Ease.InOutSine);
                codexState = codexStates.OPEN;
            }
            else
            {
                codexUI.transform.DOScale(0, .5f).SetEase(Ease.InOutSine);
                codexState = codexStates.CLOSED;
            }
        }
    }

    public void UpdateUICodex()
    {
        print("Updating Codex");
        int atomIndex = 0;
        int molIndex = 0;
        foreach(KeyValuePair<CollectablesManager.AtomAbb, bool> atom in unlocks.GetComponent<Unlocks>().atomsUnlocked)
        {
            print(atom.Key + " " + atom.Value);
            if (atom.Value == true)
            {
                codexUI.transform.Find("Atoms").GetChild(atomIndex).GetChild(0).GetComponent<Image>().color = new Color(255,255,255);
            }

            atomIndex++;
        }
        
        foreach(KeyValuePair<CollectablesManager.MoleculeAbb, bool> mol in unlocks.GetComponent<Unlocks>().moleculesUnlocked)
        {
            if (mol.Value == true)
            {
                codexUI.transform.Find("Molecules").GetChild(molIndex).GetChild(0).GetComponent<Image>().color = new Color(255,255,255);
            }

            molIndex++;
        }
    }
}
