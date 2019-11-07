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
    List<Character> units;
    public Vector3 offset;

    void Awake()
    {
        healthBar = (Slider) AssetDatabase.LoadAssetAtPath( "Assets/Prefabs/UI/HealthBarSlider.prefab", typeof(Slider));
        units = new List<Character>();
        healthBars = new List<Slider>();

        BuildUnits();
    }

    void BuildUnits() {
        foreach(Character unit in FindObjectsOfType<Character>()) {
            units.Add(unit);
            InstantiateHealthBar(unit);
        }
    }

    void InstantiateHealthBar(Character unit)
    {
        Slider newSlider = Instantiate(healthBar, unit.transform.position + offset, canvas.transform.rotation, canvas.transform);
        healthBars.Add(newSlider);
        unit.gameObject.AddComponent<HealthBar>().unit = unit;
        unit.gameObject.GetComponent<HealthBar>().slider = newSlider;
        unit.gameObject.GetComponent<HealthBar>().offset = offset;
    }

    public void AddUnit(Character newUnit) {
        units.Add( newUnit );
        InstantiateHealthBar(newUnit);
    }
}
