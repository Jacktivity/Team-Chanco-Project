using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] BoxCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(Input.GetAxis("Horizontal"), 0, 0);
    }
}
