using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.EventSystems;

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

    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            blockClicked?.Invoke( this, this );
        }
    }

    void OnMouseOver()
    {
        blockMousedOver?.Invoke(this, this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject contact = collision.gameObject;

        if (gameObject.tag == "Floor" && contact.tag == "Floor" && contact.transform.position.y == gameObject.transform.position.y)
        {
            Vector3 newCoord = coordinates - contact.GetComponent<BlockScript>().coordinates;
            if(collision.transform.position.y > transform.position.y)
            {
                occupier = contact;
                occupier.GetComponent<BlockScript>().manager = manager;
            }

            switch ((int)newCoord.z)
            {
                case 0:
                    switch ((int)newCoord.x)
                    {
                        case -1:
                            if(E==null)
                            E = contact;
                            break;
                        case 1:
                            if (W == null)
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
                            if (NE == null)
                                NE = contact;
                            break;
                        case 1:
                            if (NW == null)
                                NW = contact;
                            break;
                        case 0:
                            if (N == null)
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
                            if (SE == null)
                                SE = contact;
                            break;
                        case 1:
                            if (SW == null)
                                SW = contact;
                            break;
                        case 0:
                            if (S == null)
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

        if (gameObject.tag == "Floor-Transition")
        {
            Vector3 newCoord = coordinates - contact.GetComponent<BlockScript>().coordinates;
            var contactBlock = contact.GetComponent<BlockScript>();
            var direction = gameObject.transform.eulerAngles.y / 90;

            switch ((int)newCoord.z)
            {
                case 0:
                    switch ((int)newCoord.x)
                    {
                        case -1:
                            if (direction == 3 && contact.transform.position.y == gameObject.transform.position.y ||
                                direction == 1 && contact.transform.position.y == gameObject.transform.position.y - 1)
                            {
                                E = contact;
                                contactBlock.W = gameObject;
                            }
                            break;
                        case 1:
                            if (direction == 1 && contact.transform.position.y == gameObject.transform.position.y ||
                                direction == 3 && contact.transform.position.y == gameObject.transform.position.y - 1)
                            {
                                W = contact;
                                contactBlock.E = gameObject;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case -1:
                    switch ((int)newCoord.x)
                    {
                        case 0:
                            if ((direction == 2 && contact.transform.position.y == gameObject.transform.position.y) || 
                                direction == 0 && contact.transform.position.y == gameObject.transform.position.y -1)
                            {
                                N = contact;
                                contactBlock.S = gameObject;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    switch ((int)newCoord.x)
                    {
                        case 0:
                            if (direction == 0 && contact.transform.position.y == gameObject.transform.position.y ||
                                direction == 2 && contact.transform.position.y == gameObject.transform.position.y - 1)
                            {
                                S = contact;
                                contactBlock.N = gameObject;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void ChangeColour(Color colour)
    {
        gameObject.GetComponent<Renderer>().material.color = colour;
    }
}
