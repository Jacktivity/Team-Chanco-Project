using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        tempBlock = Instantiate(activeBlock, locationBlockPos, locationBlockRot);
        tempBlock.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mapGenerated && locationBlockTag != "UI")
            {
                var placedBlock = Instantiate(activeBlock, locationBlockPos, locationBlockRot);
                placedBlock.transform.parent = blockContainer.transform;

            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (mapGenerated && locationBlockTag != "UI")
            {
                Destroy(locationBlock);
            }
        }

        ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            locationBlockTag = hit.transform.tag;
            locationBlockPos = hit.transform.position + hit.normal;
            locationBlockRot = hit.transform.rotation;
            locationBlock = hit.transform.gameObject;
            tempBlock.transform.position = locationBlockPos;
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
    }
}
