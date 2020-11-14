using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Molecule", menuName = "ScriptableObjects/Collectable/Molecule", order = 1)]
public class Molecule : ScriptableObject
{
    public string moleculeName;
    public CollectablesManager.MoleculeAbb abbreviation;
    public Sprite sprite;
}
