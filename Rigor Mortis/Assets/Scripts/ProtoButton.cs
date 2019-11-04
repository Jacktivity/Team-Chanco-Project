using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoButton : MonoBehaviour
{
    public GameObject unit;
    public GridManager manager;
    // Start is called before the first frame update
    void Start()
    {
    }
     public void SetUnit()
    {
        manager.setSelectedUnit(unit);
    }
}
