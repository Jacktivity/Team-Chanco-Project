using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject unit;
    [SerializeField]Character character;
    public Slider slider;
    public float offset = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        character = unit.GetComponent<Character>();
        slider = GetComponent<Slider>();

        slider.maxValue = character.GetHealth();
        slider.value = character.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(unit.transform.position.x, unit.transform.position.y + offset,  unit.transform.position.z);
    }
}
