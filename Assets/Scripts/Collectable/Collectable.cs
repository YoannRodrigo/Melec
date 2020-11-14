using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectable", menuName = "ScriptableObjects/Collectable/Collectable", order = 1)]
[System.Serializable]
public class Collectable : ScriptableObject
{
    public string collectableName;
    public CollectablesManager.CollectableType type;
    public CollectablesManager.AtomAbb atomAbb;
    public CollectablesManager.MoleculeAbb molAbb;
    public Sprite sprite;
}
