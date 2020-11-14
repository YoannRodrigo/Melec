using UnityEngine;

public class testManager : MonoBehaviour
{

    public CollectablesManager collectablesManager;
    public Inventory inventory;

    // Start is called before the first frame update
    private void Start()
    {
        int numberOfSpawn = Random.Range(8,8);
        for (int i = 0; i < numberOfSpawn; i++) {
            collectablesManager.SpawnAtom((CollectablesManager.AtomAbb)Random.Range(0,8), new Vector3(Random.Range(-6f,6f),1f,Random.Range(-2.8f,2.8f)));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.MergeAtoms(0, 1);
        }
    }
}
