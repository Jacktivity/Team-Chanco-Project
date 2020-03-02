using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupierScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject contact = collision.gameObject;

        if (contact.tag == "Floor")
        {
           contact.GetComponent<BlockScript>().occupier = gameObject;
        }
    }
}
