using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPointBar : MonoBehaviour
{
    public Character unit;
    public Slider slider;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = unit.ActionPoints;
        slider.value = unit.ActionPoints;
        Offset();
    }

    void Offset() {
        switch (unit.name) {

            case "Necromancer":
                offset.y = 1.75f;
                break;

            case "Skeleton":
                offset.y = 1.95f;
                break;
            case "FlamingSkull":
                offset.y = 2.0f;
                break;
            default:
                offset.y = 1;
                break;
        }
        slider.transform.position = unit.transform.position + offset; 
    }
}
