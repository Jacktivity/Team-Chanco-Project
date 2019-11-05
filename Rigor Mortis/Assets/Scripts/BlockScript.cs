using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockScript : MonoBehaviour
{
    public Vector3 coordinates;
    public GridManager manager;
    public bool placeable;
    public Color origin;
    public float MoveModifier = 1;
    public bool Traversable { get; private set; }
    public bool Occupied => occupier != null;

    public GameObject N, NE, E, SE, S, SW, W, NW, occupier;
    public BlockScript[] UnoccupiedAdjacentTiles() => AdjacentTiles().Where(t => t.Occupied == false).ToArray();
    public BlockScript[] AdjacentTiles() => new GameObject[] { N, E, S, W }.Where(s => s != null).Select(go => go.GetComponent<BlockScript>()).ToArray();

    // Start is called before the first frame update
    void Start()
    {
        manager = gameObject.transform.parent.GetComponent<GridManager>();
        origin = gameObject.GetComponent<Renderer>().material.color;

        if (placeable)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            if (manager.GetPlacementPoints() <= 0)
            {
                placeable = false;
                gameObject.GetComponent<Renderer>().material.color = origin;
            }
        }
    }

    private void Update()
    {

        if (placeable)
        {
            if (manager.GetPlacementPoints() <= 0)
            {
                placeable = false;
                gameObject.GetComponent<Renderer>().material.color = origin;
            }
        }
    }

    private void OnMouseDown()
    {
        if(manager.GetSelectedUnit() != null)
        {
            var costOfUnit = manager.GetSelectedUnit().cost;
            if ((manager.GetPlacementPoints() - costOfUnit) >= 0 && placeable)
            {
                manager.SpawnUnit(new Vector3(transform.position.x, 1, transform.position.z));
                manager.ResetSelectedUnit();
                manager.ReducePlacementPoints(costOfUnit);
            }
        }
        if(Traversable)
        {

        }
    }

    void OnMouseOver()
    {
        if(manager.moveMode)
        {
            manager.selectedBlock = this;
            this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            
        }

    }

    private void OnMouseExit()
    {
        if(manager.moveMode)
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        occupier = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject contact = collision.gameObject;

        if (contact.tag == "Floor")
        {
            Vector3 newCoord = coordinates - contact.GetComponent<BlockScript>().coordinates;

            switch ((int)newCoord.z)
            {
                case 0:
                    switch ((int)newCoord.x)
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
                    switch ((int)newCoord.x)
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
                    switch ((int)newCoord.x)
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
