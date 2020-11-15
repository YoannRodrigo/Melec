using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectable", menuName = "ScriptableObjects/Collectable/Collectable", order = 1)]
[Serializable]
public class Collectable : ScriptableObject
{
    public string collectableName;
    public CollectablesManager.CollectableType type;
    public CollectablesManager.AtomAbb atomAbb;
    public CollectablesManager.MoleculeAbb molAbb;
    public Sprite sprite;
}
