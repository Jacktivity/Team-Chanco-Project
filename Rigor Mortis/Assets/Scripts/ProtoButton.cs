using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoButton : MonoBehaviour
{
    public Character unit;
    public GridManager manager;
    public GameObject mybutton;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void setUnit()
    {
        manager.SetSelectedUnit(unit);
        if (unit.name == "Necromancer")
        {
            mybutton.SetActive(false);
        }
    }
}
