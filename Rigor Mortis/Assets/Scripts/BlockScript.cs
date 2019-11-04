using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockScript : MonoBehaviour
{
    public Vector3 coordinates;
    public GameObject N, NE, E, SE, S, SW, W, NW, occupier;

    public float MoveModifier = 1;
    public bool Traversable { get; private set; }

    public bool Occupied => occupier != null;

    public BlockScript[] AdjacentTiles() => new GameObject[] { N, NE, E, SE, S, SW, W, W, NE }.Where(s => s != null).Select(go => go.GetComponent<BlockScript>()).Where(t => t.Occupied == false).ToArray();

    private void Start()
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
