using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Character unit;
    public Slider slider;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = unit.GetHealth;
        slider.value = unit.GetHealth;
        Offset();
    }

    // Update is called once per frame
    // Temporary until attack methods are created, then will be tied to said methods
    void Update()
    {
        //transform.position = new Vector3(unit.transform.position.x, unit.transform.position.y,  unit.transform.position.z);
    }

    void Offset() {
        switch (unit.name) {

            case "Necromancer":
                offset.y = 1.75f;
                break;

            case "Skeleton":
                offset.y = 1.25f;
                break;

            default:
                offset.y = 1;
                break;
        }
        slider.transform.position = unit.transform.position + offset; 
        Debug.Log( unit.tag + " " + unit.name + " Offset: " + offset.y);
    }
}
