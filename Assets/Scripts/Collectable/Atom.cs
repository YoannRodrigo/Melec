using UnityEngine;

[CreateAssetMenu(fileName = "Atom", menuName = "ScriptableObjects/Collectable/Atom", order = 1)]
public class Atom : ScriptableObject
{
    public string atomName;
    public CollectablesManager.AtomAbb abbreviation;
    public Sprite sprite;

}
