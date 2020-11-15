using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocks : MonoBehaviour
{
    public Dictionary<CollectablesManager.AtomAbb, bool> atomsUnlocked = new Dictionary<CollectablesManager.AtomAbb, bool>();
    public Dictionary<CollectablesManager.MoleculeAbb, bool> moleculesUnlocked = new Dictionary<CollectablesManager.MoleculeAbb, bool>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        atomsUnlocked.Add(CollectablesManager.AtomAbb.C, false);
        atomsUnlocked.Add(CollectablesManager.AtomAbb.CL, false);
        atomsUnlocked.Add(CollectablesManager.AtomAbb.H, false);
        atomsUnlocked.Add(CollectablesManager.AtomAbb.FE, false);
        atomsUnlocked.Add(CollectablesManager.AtomAbb.N, false);
        atomsUnlocked.Add(CollectablesManager.AtomAbb.O, false);
        atomsUnlocked.Add(CollectablesManager.AtomAbb.SI, false);
        atomsUnlocked.Add(CollectablesManager.AtomAbb.NA, false);
        atomsUnlocked.Add(CollectablesManager.AtomAbb.S, false);
        
        moleculesUnlocked.Add(CollectablesManager.MoleculeAbb.NH, false);
        moleculesUnlocked.Add(CollectablesManager.MoleculeAbb.CO, false);
        moleculesUnlocked.Add(CollectablesManager.MoleculeAbb.CLCL, false);
        moleculesUnlocked.Add(CollectablesManager.MoleculeAbb.HH, false);
        moleculesUnlocked.Add(CollectablesManager.MoleculeAbb.CH, false);
        moleculesUnlocked.Add(CollectablesManager.MoleculeAbb.NO, false);
        moleculesUnlocked.Add(CollectablesManager.MoleculeAbb.FES, false);
        moleculesUnlocked.Add(CollectablesManager.MoleculeAbb.NACL, false);
        moleculesUnlocked.Add(CollectablesManager.MoleculeAbb.SIO, false);
        moleculesUnlocked.Add(CollectablesManager.MoleculeAbb.CCL, false);
        moleculesUnlocked.Add(CollectablesManager.MoleculeAbb.HO, false);
    }
}
