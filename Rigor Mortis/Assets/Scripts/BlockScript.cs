using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockScript : MonoBehaviour
{    
    public Vector3 coordinates;

    //public GridManager manager;
    public bool placeable, exit, trigger;
    public float MoveModifier = 1;
    public bool Traversable { get; private set; }
    public bool Occupied => occupier != null;

    public static EventHandler<BlockScript> blockMousedOver;
    public static EventHandler<BlockScript> blockClicked;

    public int blockType, triggerId;

#pragma warning disable 069
    [SerializeField] private GameObject highlight;
    [SerializeField] private Renderer borderNorth, borderEast, borderSouth, borderWest;
    [SerializeField] private Color normal;
#pragma warning restore 069

    public GameObject N, NE, E, SE, S, SW, W, NW, occupier;
    public BlockScript[] UnoccupiedAdjacentTiles() => AdjacentTiles().Where(t => t.Occupied == false).ToArray();
    public BlockScript[] AdjacentTiles() => new GameObject[] { N, E, S, W }.Where(s => s != null).Select(go => go.GetComponent<BlockScript>()).ToArray();
    public Color Normal => normal;

    public GameObject[] blockPrefabs = new GameObject[5];


    // Start is called before the first frame update
    void Start()
    {

    }

    public Vector3 Location()
    {
        try
        {
            return transform.position;
        }
        catch (Exception)
        {
            return coordinates;
        }
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
        var blockMeshes = blockPrefabs.Select(b => b.GetComponent<MeshRenderer>()).ToArray(); ;

        GameObject contact = collision.gameObject;        
        //if (contact.transform.position.y == gameObject.transform.position.y + 1 &&
        //    contact.transform.position.x == gameObject.transform.position.x &&
        //    contact.transform.position.z == gameObject.transform.position.z )
        //{
        //    occupier = contact;
        //}
        if (gameObject.tag == "Floor" && contact.tag == "Floor" && contact.transform.position.y == gameObject.transform.position.y)
        {
            Vector3 newCoord = coordinates - contact.GetComponent<BlockScript>().coordinates;
            if(collision.transform.position.y > transform.position.y)
            {
                occupier = contact;
                //occupier.GetComponent<BlockScript>().manager = manager;

            }

            foreach(var item in blockMeshes) { item.enabled = false;};

            if(N && E && W && S)
            {
                blockMeshes[0].enabled = true;
            }
            else if(S && W && N)
            {
                blockMeshes[3].enabled = true;
                blockMeshes[3].transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else if (S && E && N)
            {
                blockMeshes[3].enabled = true;
                blockMeshes[3].transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else if (S && E && W)
            {
                blockMeshes[3].enabled = true;
            }
            else if (N && E && W)
            {
                blockMeshes[3].enabled = true;
                blockMeshes[3].transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if(S && E)
            {
                blockMeshes[1].enabled = true;
                blockMeshes[1].transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else if (S && W)
            {
                blockMeshes[1].enabled = true;
            }
            else if(N && E)
            {
                blockMeshes[2].enabled = true;
                blockMeshes[2].transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else if(N && W)
            {
                blockMeshes[2].enabled = true;
                blockMeshes[2].transform.eulerAngles = new Vector3(0, 180, 0);
            } else
            {
                blockMeshes[4].enabled = true;
            }
        }

        //if (gameObject.tag == "Floor-Transition" && (contact.tag == "Floor" || contact.tag == "Floor-Transition"))
        //{
        //    Vector3 newCoord = coordinates - contact.GetComponent<BlockScript>().coordinates;
        //    var contactBlock = contact.GetComponent<BlockScript>();
        //    var direction = gameObject.transform.eulerAngles.y / 90;

        //    switch ((int)newCoord.z)
        //    {
        //        case 0:
        //            switch ((int)newCoord.x)
        //            {
        //                case -1:
        //                    if ((direction == 3 && (contact.transform.position.y == gameObject.transform.position.y || contact.transform.position.y == gameObject.transform.position.y +1) ||
        //                        direction == 1 && contact.transform.position.y == gameObject.transform.position.y - 1) && E == null)
        //                    {
        //                        E = contact;
        //                        contactBlock.W = gameObject;
        //                    }
        //                    break;
        //                case 1:
        //                    if ((direction == 1 && (contact.transform.position.y == gameObject.transform.position.y || contact.transform.position.y == gameObject.transform.position.y + 1) ||
        //                        direction == 3 && contact.transform.position.y == gameObject.transform.position.y - 1) && W == null)
        //                    {
        //                        W = contact;
        //                        contactBlock.E = gameObject;
        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }
        //            break;
        //        case -1:
        //            switch ((int)newCoord.x)
        //            {
        //                case 0:
        //                    if ((direction == 2 && (contact.transform.position.y == gameObject.transform.position.y || contact.transform.position.y == gameObject.transform.position.y + 1) ||
        //                        direction == 0 && contact.transform.position.y == gameObject.transform.position.y -1) && N == null)
        //                    {
        //                        N = contact;
        //                        contactBlock.S = gameObject;
        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }
        //            break;
        //        case 1:
        //            switch ((int)newCoord.x)
        //            {
        //                case 0:
        //                    if ((direction == 0 && (contact.transform.position.y == gameObject.transform.position.y || contact.transform.position.y == gameObject.transform.position.y + 1) ||
        //                        direction == 2 && contact.transform.position.y == gameObject.transform.position.y - 1) && S == null)
        //                    {
        //                        S = contact;
        //                        contactBlock.N = gameObject;
        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }

    public void SetHighlightColour(Color colour) => SetHighlightColour(colour, new Directions[0], new Vector2(colour.a, 1-colour.a), new Vector2(colour.a, 1-colour.a));

    public void SetHighlightColour(Color colour, IEnumerable<Directions> boldHighlight, Vector2 soft, Vector2 curve)
    {        
        highlight.GetComponent<Renderer>().material.color = new Color(colour.r,colour.g, colour.b, colour.a * 0.05f);        

        borderNorth.material.SetColor("_Colour", colour);
        if (boldHighlight.Contains(Directions.North))
        {            
            borderNorth.material.SetFloat("_Soft", soft.x);
            borderNorth.material.SetFloat("_Curve", curve.x);
        }            
        else
        {
            borderNorth.material.SetFloat("_Soft", soft.y);
            borderNorth.material.SetFloat("_Curve", curve.y);
        }

        borderSouth.material.SetColor("_Colour", colour);
        if (boldHighlight.Contains(Directions.South))
        {
            borderSouth.material.SetFloat("_Soft", soft.x);
            borderSouth.material.SetFloat("_Curve", curve.x);
        }
        else
        {
            borderSouth.material.SetFloat("_Soft", soft.y);
            borderSouth.material.SetFloat("_Curve", curve.y);
        }


        borderEast.material.SetColor("_Colour", colour);
        if (boldHighlight.Contains(Directions.East))
        {
            borderEast.material.SetFloat("_Soft", soft.x);
            borderEast.material.SetFloat("_Curve", curve.x);
        }
        else
        {
            borderEast.material.SetFloat("_Soft", soft.y);
            borderEast.material.SetFloat("_Curve", curve.y);
        }

        borderWest.material.SetColor("_Colour", colour);
        if (boldHighlight.Contains(Directions.West))
        {
            borderWest.material.SetFloat("_Soft", soft.x);
            borderWest.material.SetFloat("_Curve", curve.x);
        }
        else
        {
            borderWest.material.SetFloat("_Soft", soft.y);
            borderWest.material.SetFloat("_Curve", curve.y);
        }
    }
}

public enum Directions
{
    North, South, East, West
}
