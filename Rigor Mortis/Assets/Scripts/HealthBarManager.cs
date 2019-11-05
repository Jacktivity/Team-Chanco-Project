using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public Canvas canvas;
    Slider healthBar;
    List<Slider> healthBars;
    List<GameObject> units;

    void Awake()
    {
        healthBar = (Slider) AssetDatabase.LoadAssetAtPath( "Assets/Prefabs/UI/HealthBarSlider.prefab", typeof(Slider));
        units = new List<GameObject>();
        healthBars = new List<Slider>();

        BuildUnits();
    }

    void BuildUnits() {
        foreach(GameObject unit in GameObject.FindGameObjectsWithTag("Player")) {
            units.Add(unit);
            InstantiateHealthBar(unit);
        }
    }

    void InstantiateHealthBar(GameObject unit)
    {
        Slider newSlider = Instantiate(healthBar, unit.transform.position, unit.transform.rotation, canvas.transform);
        healthBars.Add(newSlider);
        newSlider.gameObject.AddComponent<HealthBar>().unit = unit;
    }

    public void AddUnit(GameObject newUnit) {
        units.Add( newUnit );
        InstantiateHealthBar(newUnit);
    }
}
