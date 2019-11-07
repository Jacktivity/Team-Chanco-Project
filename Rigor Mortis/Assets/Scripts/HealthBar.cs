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
        slider.maxValue = unit.GetHealth();
        slider.value = unit.GetHealth();
    }

    // Update is called once per frame
    // Temporary until attack methods are created, then will be tied to said methods
    void Update()
    {
        //transform.position = new Vector3(unit.transform.position.x, unit.transform.position.y,  unit.transform.position.z);
    }
}
