using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject unit;
    public float offset = 0.75f;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(unit.transform.position.x, unit.transform.position.y + offset,  unit.transform.position.z);
    }
}
