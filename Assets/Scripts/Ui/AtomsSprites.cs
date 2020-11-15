using System.Collections.Generic;
using UnityEngine;

public class AtomsSprites : MonoBehaviour
{
    public Dictionary<CollectablesManager.AtomAbb, Sprite > atomsArray = new Dictionary<CollectablesManager.AtomAbb, Sprite>();
    public Sprite[] atomsSprites;

    private void Start()
    {
        atomsArray. Add(CollectablesManager.AtomAbb.C, atomsSprites[0]);
        atomsArray. Add(CollectablesManager.AtomAbb.CL, atomsSprites[1]);
        atomsArray. Add(CollectablesManager.AtomAbb.H, atomsSprites[2]);
        atomsArray. Add(CollectablesManager.AtomAbb.FE, atomsSprites[3]);
        atomsArray. Add(CollectablesManager.AtomAbb.N, atomsSprites[4]);
        atomsArray. Add(CollectablesManager.AtomAbb.O, atomsSprites[5]);
        atomsArray. Add(CollectablesManager.AtomAbb.SI, atomsSprites[6]);
        atomsArray. Add(CollectablesManager.AtomAbb.NA, atomsSprites[7]);
        atomsArray. Add(CollectablesManager.AtomAbb.S, atomsSprites[8]);
    }
}
