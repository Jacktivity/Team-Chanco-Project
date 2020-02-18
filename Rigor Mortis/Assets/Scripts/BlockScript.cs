using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockScript : MonoBehaviour
{
    public Vector3 coordinates;

    //public GridManager manager;
    public bool placeable;
    public float MoveModifier = 1;
    public bool Traversable { get; private set; }
    public bool Occupied => occupier != null;

    public static EventHandler<BlockScript> blockMousedOver;
    public static EventHandler<BlockScript> blockClicked;

#pragma warning disable 069
    [SerializeField] private GameObject highlight;
    [SerializeField] private Renderer borderNorth, borderEast, borderSouth, borderWest;
    [SerializeField] private Color normal;
#pragma warning restore 069

    public GameObject N, NE, E, SE, S, SW, W, NW, occupier;
    public BlockScript[] UnoccupiedAdjacentTiles() => AdjacentTiles().Where(t => t.Occupied == false).ToArray();
    public BlockScript[] AdjacentTiles() => new GameObject[] { N, E, S, W }.Where(s => s != null).Select(go => go.GetComponent<BlockScript>()).ToArray();
    public Color Normal => normal;
    
    // Start is called before the first frame update
    void Start()

    {        
        //manager = gameObject.transform.parent.GetComponent<GridManager>();        

        //if (placeable)
        //{
        //    Highlight(true);
        //    ChangeColour(manager.SpawnColor);
        //    if (manager.GetPlacementPoints() <= 0)
        //    {
        //        placeable = false;
        //        Highlight(false);
        //        ChangeColour(normal);
        //    }
        //}
    }

    public void Highlight(bool highlighted)
    {
        highlight.SetActive(highlighted);
    }    

    public static void ResetBlockScriptEvents()
    {
        blockClicked = null;
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

        if (contact.transform.position.y == gameObject.transform.position.y + 1 && 
            contact.transform.position.x == gameObject.transform.position.x && 
            contact.transform.position.z == gameObject.transform.position.z )
        {
            occupier = contact;
        }
        if (gameObject.tag == "Floor" && contact.tag == "Floor" && contact.transform.position.y == gameObject.transform.position.y)
        {
            Vector3 newCoord = coordinates - contact.GetComponent<BlockScript>().coordinates;
            if(collision.transform.position.y > transform.position.y)
            {
                occupier = contact;
                //occupier.GetComponent<BlockScript>().manager = manager;

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

        if (gameObject.tag == "Floor-Transition" && (contact.tag == "Floor" || contact.tag == "Floor-Transition"))
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
                            if ((direction == 3 && (contact.transform.position.y == gameObject.transform.position.y || contact.transform.position.y == gameObject.transform.position.y +1) ||
                                direction == 1 && contact.transform.position.y == gameObject.transform.position.y - 1) && E == null)
                            {
                                E = contact;
                                contactBlock.W = gameObject;
                            }
                            break;
                        case 1:
                            if ((direction == 1 && (contact.transform.position.y == gameObject.transform.position.y || contact.transform.position.y == gameObject.transform.position.y + 1) ||
                                direction == 3 && contact.transform.position.y == gameObject.transform.position.y - 1) && W == null)
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
                            if ((direction == 2 && (contact.transform.position.y == gameObject.transform.position.y || contact.transform.position.y == gameObject.transform.position.y + 1) || 
                                direction == 0 && contact.transform.position.y == gameObject.transform.position.y -1) && N == null)
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
                            if ((direction == 0 && (contact.transform.position.y == gameObject.transform.position.y || contact.transform.position.y == gameObject.transform.position.y + 1) ||
                                direction == 2 && contact.transform.position.y == gameObject.transform.position.y - 1) && S == null)
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

    public void SetHighlightColour(Color colour) => SetHighlightColour(colour, new Directions[0]);

    public void SetHighlightColour(Color colour, IEnumerable<Directions> boldHighlight)
    {
        highlight.GetComponent<Renderer>().material.color = new Color(colour.r,colour.g, colour.b, colour.a * 0.05f);
        if (boldHighlight.Contains(Directions.North))
            borderNorth.material.color = colour;
        else
            borderNorth.material.color = new Color(colour.r, colour.g, colour.b, colour.a * 0.1f);
        if (boldHighlight.Contains(Directions.South))
            borderSouth.material.color = colour;
        else
            borderSouth.material.color = new Color(colour.r, colour.g, colour.b, colour.a * 0.1f);
        if (boldHighlight.Contains(Directions.East))
            borderEast.material.color = colour;
        else
            borderEast.material.color = new Color(colour.r, colour.g, colour.b, colour.a * 0.1f);
        if (boldHighlight.Contains(Directions.West))
            borderWest.material.color = colour;
        else
            borderWest.material.color = new Color(colour.r, colour.g, colour.b, colour.a * 0.1f);
    }
}

public enum Directions
{
    North, South, East, West
}
