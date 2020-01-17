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

    void Offset() {
        switch (unit.name) {
            case "Necromancer":
                offset.y = 1.95f;
                break;
            case "Skeleton":
                offset.y = 2.15f;
                break;
            case "Skeleton_Axe":
                offset.y = 2.15f;
                break;
            case "Skeleton_Rifle":
                offset.y = 2.15f;
                break;
            case "Skeleton_Spear":
                offset.y = 2.15f;
                break;
            case "FlamingSkull":
                offset.y = 2.2f;
                break;
            default:
                offset.y = 1;
                break;
        }
        slider.transform.position = unit.transform.position + offset; 
    }
}
