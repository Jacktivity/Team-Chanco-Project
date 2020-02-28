using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockDetect : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject contact = collision.gameObject;
        var block = gameObject.GetComponent<BlockScript>();
        var blockMeshes = block.blockPrefabs.Select(b => b.GetComponent<MeshRenderer>()).ToArray(); ;
        if (gameObject.tag == "Floor" && contact.tag == "Floor" && contact.transform.position.y == gameObject.transform.position.y)
        {
            Vector3 newCoord = block.coordinates - contact.GetComponent<BlockScript>().coordinates;
            if (collision.transform.position.y > transform.position.y)
            {
                block.occupier = contact;
            }

            switch ((int)newCoord.z)
            {
                case 0:
                    switch ((int)newCoord.x)
                    {
                        case -1:
                            if (block.E == null)
                                block.E = contact;
                            break;
                        case 1:
                            if (block.W == null)
                                block.W = contact;
                            break;
                        default:
                            break;
                    }
                    break;
                case -1:
                    switch ((int)newCoord.x)
                    {
                        case -1:
                            if (block.NE == null)
                                block.NE = contact;
                            break;
                        case 1:
                            if (block.NW == null)
                                block.NW = contact;
                            break;
                        case 0:
                            if (block.N == null)
                                block.N = contact;
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    switch ((int)newCoord.x)
                    {
                        case -1:
                            if (block.SE == null)
                                block.SE = contact;
                            break;
                        case 1:
                            if (block.SW == null)
                                block.SW = contact;
                            break;
                        case 0:
                            if (block.S == null)
                                block.S = contact;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            foreach (var item in blockMeshes) { item.enabled = false; };

            if (block.N && block.E && block.W && block.S)
            {
                blockMeshes[0].enabled = true;
            }
            else if (block.S && block.W && block.N)
            {
                blockMeshes[3].enabled = true;
                blockMeshes[3].transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else if (block.S && block.E && block.N)
            {
                blockMeshes[3].enabled = true;
                blockMeshes[3].transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else if (block.S && block.E && block.W)
            {
                blockMeshes[3].enabled = true;
            }
            else if (block.N && block.E && block.W)
            {
                blockMeshes[3].enabled = true;
                blockMeshes[3].transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (block.S && block.E)
            {
                blockMeshes[1].enabled = true;
                blockMeshes[1].transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else if (block.S && block.W)
            {
                blockMeshes[1].enabled = true;
            }
            else if (block.N && block.E)
            {
                blockMeshes[2].enabled = true;
                blockMeshes[2].transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else if (block.N && block.W)
            {
                blockMeshes[2].enabled = true;
                blockMeshes[2].transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                blockMeshes[4].enabled = true;
            }
        }
    }
}

