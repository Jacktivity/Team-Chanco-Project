using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject[] Units => transform.GetComponentsInChildren<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var p = new Pathfinder();
        p.GetPath(new BlockScript(), (b) => b.name == "Jeff");
    }
}

public enum AIStates
{
    Regroup,Retreat,Attack
}
