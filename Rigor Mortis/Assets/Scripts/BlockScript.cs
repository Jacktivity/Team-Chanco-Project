using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BlockScript : MonoBehaviour
{
    public Vector3 coordinates;
    public GridManager manager;
    public bool placeable;
    public float MoveModifier = 1;
    public bool Traversable { get; private set; }
    public bool Occupied => occupier != null;

    public static EventHandler<BlockScript> blockMousedOver;
    public static EventHandler<BlockScript> blockClicked;

#pragma warning disable 069
    [SerializeField] private Color normal;
#pragma warning restore 069

    public GameObject N, NE, E, SE, S, SW, W, NW, occupier;
    public BlockScript[] UnoccupiedAdjacentTiles() => AdjacentTiles().Where(t => t.Occupied == false).ToArray();
    public BlockScript[] AdjacentTiles() => new GameObject[] { N, E, S, W }.Where(s => s != null).Select(go => go.GetComponent<BlockScript>()).ToArray();
    public Color Normal => normal;


    // Start is called before the first frame update
    void Start()
    {
        manager = gameObject.transform.parent.GetComponent<GridManager>();        

        if (placeable)
        {
            ChangeColour(manager.SpawnColor);
            if (manager.GetPlacementPoints() <= 0)
            {
                placeable = false;
                ChangeColour(normal);
            }
        }
    }

    private void Update()
    {

        //if (placeable)
        //{
        //    if (manager.GetPlacementPoints() <= 0)
        //    {
        //        placeable = false;
        //        ChangeColour(normal);
        //    }
        //}
    }

    private void OnMouseDown()
    {
        blockClicked?.Invoke(this, this);

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
        blockMousedOver?.Invoke(this, this);
    }

    private void OnMouseExit()
    {
        if(manager.moveMode)
        {
            ChangeColour(Color.white);
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
            if(collision.transform.position.y > transform.position.y)
            {
                occupier = contact;
            }

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

    public void ChangeColour(Color colour)
    {
        gameObject.GetComponent<Renderer>().material.color = colour;
    }
}
