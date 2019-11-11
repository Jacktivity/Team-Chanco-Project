using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoButton : MonoBehaviour
{
    public Character unit;
    public GridManager manager;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void setUnit()
    {
        manager.SetSelectedUnit(unit);
    }
}
