using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placement : MonoBehaviour
{
    public Canvas placementCanvas;
    public GameObject activeBlock;
    public GameObject tempBlock;

    public GameObject blockContainer;

    Vector3 locationBlockPos;
    Quaternion locationBlockRot;
    string locationBlockTag;
    GameObject locationBlock;
    Vector3 locationBlockNormal;

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
        ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (mapGenerated && locationBlockTag != "UI" && Physics.Raycast(ray, out hit))
            {
                var occupier = activeBlock.GetComponent<BlockScript>().occupier;
                if (!deleteMode && occupier == null && activeBlock.name != "Difficult")
                {
                    var placedBlock = Instantiate(activeBlock, locationBlockPos, tempBlock.transform.rotation);
                    placedBlock.transform.parent = blockContainer.transform;
                    placedBlock.GetComponent<BlockScript>().coordinates = locationBlock.GetComponent<BlockScript>().coordinates + locationBlockNormal;
                }
                else if(!deleteMode && occupier != null || activeBlock.name == "Difficult")
                {
                    var placedBlock = Instantiate(activeBlock, hit.transform.position, tempBlock.transform.rotation);
                    placedBlock.GetComponent<BlockScript>().coordinates = locationBlock.GetComponent<BlockScript>().coordinates;
                    if (hit.transform.gameObject != tempBlock) Destroy(hit.transform.gameObject);
                    placedBlock.transform.parent = blockContainer.transform;
                }
                else if(deleteMode)
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
        if (Physics.Raycast(ray, out hit))
        {
            if (block != null)
            {
                block.SetHighlightColour(block.Normal);
            }
            locationBlockTag = hit.transform.tag;
            locationBlockNormal = hit.normal;
            locationBlockPos = hit.transform.position + hit.normal;
            locationBlockRot = hit.transform.rotation;
            locationBlock = hit.transform.gameObject;
            block = locationBlock.transform.gameObject.GetComponent<BlockScript>();
            var occupier = activeBlock.GetComponent<BlockScript>().occupier;
            if (!deleteMode && occupier == null && activeBlock.name != "Difficult")
            {
                tempBlock.transform.position = locationBlockPos;
            }
            else if (!deleteMode && occupier != null || activeBlock.name == "Difficult")
            {
                tempBlock.transform.position = hit.transform.position;
            }
            else
            {
                tempBlock.transform.position = new Vector3(-10, -10, -10);
                block.SetHighlightColour(delete);
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

    public void SaveMap()
    {

    }
}
