using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public Vector3 coordinates;
    public GameObject N, NE, E, SE, S, SW, W, NW, occupier;
    
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
            Vector3 newCoord = coordinates - contact.GetComponent<BlockScript>().coordinates;

            switch(newCoord.z)
            {
                case 0:
                    switch (newCoord.x)
                    {
                        case -1:
                            E = contact;
                            break;
                        case 1:
                            W = contact;
                            break;
                        default:
                            break;
                    }
                    break;
                case -1:
                    switch (newCoord.x)
                    {
                        case -1:
                            NE = contact;
                            break;
                        case 1:
                            NW = contact;
                            break;
                        case 0:
                            N = contact;
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    switch (newCoord.x)
                    {
                        case -1:
                            SE = contact;
                            break;
                        case 1:
                            SW = contact;
                            break;
                        case 0:
                            S = contact;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        else {
            occupier = contact;
        }
    }

}
