using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    Slider[] healthBars;
    GameObject[] units;
    // Start is called before the first frame update
    void Start()
    {
        units = GameObject.FindGameObjectsWithTag("Player");
    }

    void InstantiateHealthBar()
    {
        foreach(GameObject unit in units)
        {

        }
    }
}
