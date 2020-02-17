using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placement : MonoBehaviour
{
    public Canvas placementCanvas;
    public GameObject activeBlock;
    private GameObject tempBlock;

    public GameObject blockContainer;

    Vector3 locationBlockPos;
    Quaternion locationBlockRot;
    string locationBlockTag;
    GameObject locationBlock;

    Ray ray;
    RaycastHit hit;

    bool mapGenerated = false;
    bool deleteMode;
    [SerializeField] private Color delete;

    BlockScript block;
    public Button deleteButton;
   
    // Start is called before the first frame update
    void Start()
    {
        tempBlock = Instantiate(activeBlock, locationBlockPos, locationBlockRot);
        tempBlock.GetComponent<BoxCollider>().enabled = false;
        tempBlock.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mapGenerated && locationBlockTag != "UI" && Physics.Raycast(ray, out hit))
            {
                if(!deleteMode)
                {
                    var placedBlock = Instantiate(activeBlock, locationBlockPos, tempBlock.transform.rotation);
                    placedBlock.transform.parent = blockContainer.transform;
                }
                else
                {
                    Destroy(locationBlock);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            tempBlock.transform.Rotate(0, 90.0f, 0);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            tempBlock.transform.Rotate(0, -90.0f, 0);
        }

        ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if(block != null)
            {
                block.ChangeColour(block.Normal);
            }
            locationBlockTag = hit.transform.tag;
            locationBlockPos = hit.transform.position + hit.normal;
            locationBlockRot = hit.transform.rotation;
            locationBlock = hit.transform.gameObject;
            block = locationBlock.transform.gameObject.GetComponent<BlockScript>();
            if (!deleteMode)
            {
                tempBlock.transform.position = locationBlockPos;
            }
            else
            {
                tempBlock.transform.position = new Vector3(-10,-10,-10);
                block.ChangeColour(delete);
            }

        }
    }

    public void ChangeActiveBlock(GameObject newBlock)
    {
        activeBlock = newBlock;

        Destroy(tempBlock);
        tempBlock = Instantiate(activeBlock, locationBlockPos, new Quaternion());
        tempBlock.GetComponent<BoxCollider>().enabled = false;
    }

    public void MapGenerated()
    {
        mapGenerated = true;
        tempBlock.active = true;
    }

    public void DeletMode()
    {
        deleteMode = !deleteMode;
    }
}
