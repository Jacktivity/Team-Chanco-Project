using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Placement : MonoBehaviour
{
    public Canvas placementCanvas;
    public GameObject activeBlock;
    public GameObject tempBlock;

    public GameObject blockContainer, enemyContainer;

    Vector3 locationBlockPos;
    Quaternion locationBlockRot;
    string locationBlockTag;
    GameObject locationBlock;
    Vector3 locationBlockNormal;

    Ray ray;
    RaycastHit hit;

    bool mapGenerated = false;
    bool deleteMode;
    Color delete = Color.red;

    BlockScript block;
    public Toggle deleteToggle, placementToggle, aiEditToggle;
    GameObject occupier;
    bool placementMode, aiEditMode;

    public GameObject terrainOptions, enemyOptions, enemyDetails;

    Character selectedEnemy;

    public Text enemyName;
    public Toggle captainToggle, repeatSpawn;

    Color highlight = Color.yellow;
    // Start is called before the first frame update
    void Start()
    {
        tempBlock = Instantiate(activeBlock, locationBlockPos, locationBlockRot);
        tempBlock.GetComponent<BoxCollider>().enabled = false;
        tempBlock.active = false;

        placementMode = false;
        deleteMode = false;

    }

    // Update is called once per frame
    void Update()
    {
        ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(locationBlock != null)
            {
                block = locationBlock.GetComponent<BlockScript>();
            }
            if (mapGenerated && locationBlockTag == "Floor" && Physics.Raycast(ray, out hit) && !deleteMode && !placementMode && !aiEditMode)
            {
                if (activeBlock.GetComponent<BlockScript>())
                {
                   occupier = activeBlock.GetComponent<BlockScript>().occupier;
                }
                if (occupier == null && activeBlock.name != "Difficult" && activeBlock.tag != "Enemy")
                {
                    var placedBlock = Instantiate(activeBlock, locationBlockPos, tempBlock.transform.rotation);
                    placedBlock.transform.parent = blockContainer.transform;
                    placedBlock.GetComponent<BlockScript>().coordinates = block.coordinates + locationBlockNormal;
                }
                else if((occupier != null || activeBlock.name == "Difficult") && activeBlock.tag != "Enemy")
                {
                    var placedBlock = Instantiate(activeBlock, hit.transform.position, tempBlock.transform.rotation);
                    placedBlock.GetComponent<BlockScript>().coordinates = block.coordinates;
                    Destroy(locationBlock);
                    placedBlock.transform.parent = blockContainer.transform;
                }
                if(activeBlock.tag == "Enemy")
                {
                    var placedEnemy = Instantiate(activeBlock, locationBlockPos - locationBlockNormal, tempBlock.transform.rotation);
                    placedEnemy.GetComponent<Character>().floor = block;
                    placedEnemy.transform.parent = enemyContainer.transform;
                    block.occupier = activeBlock;
                }
            }
            if (deleteMode)
            {
                Destroy(locationBlock);
            }
            if(aiEditMode)
            {
                if (selectedEnemy == null && hit.transform.gameObject.tag == "Enemy")
                {
                    selectedEnemy = hit.transform.gameObject.GetComponent<Character>();
                    captainToggle.isOn = selectedEnemy.isCaptain;
                    repeatSpawn.isOn = selectedEnemy.repeatSpawn;


                }
                else if (selectedEnemy == hit.transform.gameObject.GetComponent<Character>())
                {
                    selectedEnemy = null;
                }
            }
            if(block != null)
            {
                if (placementMode && block.occupier == null)
                {
                    block.placeable = !block.placeable;
                    block.Highlight(true);
                    if (block.placeable)
                    {
                        block.SetHighlightColour(highlight);
                    }
                    else
                    {
                        block.SetHighlightColour(block.Normal);
                    }

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
                if(!block.placeable)
                {
                    block.Highlight(false);
                    block.SetHighlightColour(block.Normal);
                }
            }
            locationBlockTag = hit.transform.tag;
            locationBlockNormal = hit.normal;
            locationBlockPos = hit.transform.position + hit.normal;
            locationBlockRot = hit.transform.rotation;
            locationBlock = hit.transform.gameObject;
            block = locationBlock.transform.gameObject.GetComponent<BlockScript>(); 
            if (activeBlock.GetComponent<BlockScript>() != null)
            {
                occupier = activeBlock.GetComponent<BlockScript>().occupier;
            } else
            {
               occupier = null;
            }
            if (!deleteMode && occupier == null && activeBlock.name != "Difficult")
            {
                tempBlock.transform.position = locationBlockPos;
            }
            else if (!deleteMode && occupier != null || activeBlock.name == "Difficult")
            {
                tempBlock.transform.position = hit.transform.position;
            }
            else if(deleteMode)
            {
                tempBlock.transform.position = new Vector3(-10, -10, -10);
                block.Highlight(true);
                block.SetHighlightColour(delete);
            }
            else if (placementMode)
            {
                tempBlock.transform.position = new Vector3(-10, -10, -10);
                block.Highlight(true);
                block.SetHighlightColour(highlight);
            }
            else if (aiEditMode)
            {
                tempBlock.transform.position = new Vector3(-10, -10, -10);
            }

        }
    }

    public void ChangeActiveBlock(GameObject newBlock)
    {
        if (!placementMode && !deleteMode)
        {
            activeBlock = newBlock;
            Destroy(tempBlock);
            tempBlock = Instantiate(activeBlock, locationBlockPos, new Quaternion());
            tempBlock.GetComponent<BoxCollider>().enabled = false;
            if (tempBlock.GetComponent<BlockScript>() != null)
            {
                if (tempBlock.GetComponent<BlockScript>().occupier != null)
                {
                    tempBlock.GetComponent<BlockScript>().occupier.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }

    public void MapGenerated()
    {
        mapGenerated = true;
        tempBlock.SetActive(true);
    }

    public void DeleteMode()
    {
        deleteMode = !deleteMode;
        placementMode = false;
        tempBlock.SetActive(deleteMode);
        placementToggle.isOn = false;
        deleteToggle.isOn = deleteMode;
        aiEditMode = false;
    }

    public void PlaceableMode()
    {
        placementMode = !placementMode;
        deleteMode = false;
        tempBlock.SetActive(placementMode);
        deleteToggle.isOn = false;
        placementToggle.isOn = placementMode;
        aiEditMode = false;
    }
    public void EditAiMode()
    {
        aiEditMode = !aiEditMode;
        deleteMode = false;
        placementMode = false;
        tempBlock.SetActive(aiEditMode);
        deleteToggle.isOn = false;
        deleteToggle.isOn = false;
        aiEditToggle.isOn = aiEditMode;

        selectedEnemy = null;
        enemyDetails.SetActive(true);
        enemyOptions.SetActive(false);
        terrainOptions.SetActive(false);
    }

    public void TerrainOptions()
    {
        terrainOptions.SetActive(true);
        enemyOptions.SetActive(false);
        enemyDetails.SetActive(false);
    }
    public void EnemyOptions()
    {
        terrainOptions.SetActive(false);
        enemyOptions.SetActive(true);
        enemyDetails.SetActive(false);
    }
    public void toggleCaptain()
    {
        selectedEnemy.isCaptain = !selectedEnemy.isCaptain;
        captainToggle.isOn = selectedEnemy.isCaptain;
    }
}
