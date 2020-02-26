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
    public Toggle deleteToggle, aiEditToggle;
    GameObject occupier;
    bool aiEditMode;

    public GameObject terrainOptions, enemyOptions, enemyDetails, blockDetails;

    Character selectedEnemy;
    BlockScript selectedBlock;

    public Text enemyName, delayText, blockPos, triggerId, enemyOnTrigger;
    public Text captainToggle, repeatSpawn, exitToggle, triggerToggle, placementText, enemyTriggerId;
    public Slider delay, trigger, enemyTrigger;

    Color highlight = Color.yellow;
    // Start is called before the first frame update
    void Start()
    {
        tempBlock = Instantiate(activeBlock, locationBlockPos, locationBlockRot);
        tempBlock.GetComponent<BoxCollider>().enabled = false;
        tempBlock.active = false;

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
            if (mapGenerated && locationBlockTag == "Floor" && Physics.Raycast(ray, out hit) && !deleteMode && !aiEditMode)
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
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    if (selectedEnemy)
                    {
                        selectedEnemy.godRay.SetActive(false);
                    }
                    selectedEnemy = hit.transform.gameObject.GetComponent<Character>();
                    enemyName.text = selectedEnemy.name;
                    captainToggle.text = "Is Captain: " + selectedEnemy.isCaptain;
                    repeatSpawn.text = "Repeat Spawn: " + selectedEnemy.repeatSpawn;
                    delay.value = selectedEnemy.delaySpawn;
                    selectedEnemy.godRay.SetActive(true);
                    delayText.text = "Delay: " + delay.value;

                    enemyDetails.SetActive(true);
                    blockDetails.SetActive(false);
                }
                if (hit.transform.gameObject.tag == "Floor")
                {
                    selectedBlock = hit.transform.gameObject.GetComponent<BlockScript>();
                    blockPos.text = "Block Pos: " + selectedBlock.coordinates.x + ", " + selectedBlock.coordinates.y + ", " + selectedBlock.coordinates.z;
                    exitToggle.text = "Is Exit: " + selectedBlock.exit;
                    triggerToggle.text = "Trigger: " + selectedBlock.trigger;
                    placementText.text = "Spawn Point: " + selectedBlock.placeable;
                    trigger.value = selectedBlock.triggerId;
                    triggerId.text = "Trigger ID: " + delay.value;

                    enemyDetails.SetActive(false);
                    blockDetails.SetActive(true);
                    
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
            if(deleteMode)
            {
                tempBlock.transform.position = new Vector3(-10, -10, -10);
                block.Highlight(true);
                block.SetHighlightColour(delete);
            }
            if (aiEditMode)
            {
                tempBlock.transform.position = new Vector3(-10, -10, -10);
            }

        }
    }

    public void ChangeActiveBlock(GameObject newBlock)
    {
        if (!deleteMode)
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
        tempBlock.SetActive(deleteMode);
        deleteToggle.isOn = deleteMode;
        aiEditMode = false;
        if (selectedEnemy)
        {
            selectedEnemy.godRay.SetActive(false);
        }
    }

    public void TogglePlaceable()
    {
        selectedBlock.placeable = !selectedBlock.placeable;
        placementText.text = "Is Placeable: " + selectedBlock.placeable;
        selectedBlock.Highlight(selectedBlock.placeable);
        selectedBlock.SetHighlightColour(highlight);
    }
    public void ToggleExit()
    {
        selectedBlock.exit = !selectedBlock.exit;
        exitToggle.text = "Is Exit: " + selectedBlock.exit;
        selectedBlock.Highlight(selectedBlock.exit);
        selectedBlock.SetHighlightColour(Color.blue);
    }
    public void ToggleTrigger()
    {
        selectedBlock.trigger = !selectedBlock.trigger;
        triggerToggle.text = "Is Trigger: " + selectedBlock.trigger;
        selectedBlock.Highlight(selectedBlock.trigger);
        selectedBlock.SetHighlightColour(Color.magenta);
    }
    public void EditAiMode()
    {
        aiEditMode = !aiEditMode;
        deleteMode = false;
        tempBlock.SetActive(aiEditMode);
        deleteToggle.isOn = false;
        deleteToggle.isOn = false;
        aiEditToggle.isOn = aiEditMode;

        selectedEnemy = null;
        enemyDetails.SetActive(false);
        blockDetails.SetActive(false);
        enemyOptions.SetActive(false);
        terrainOptions.SetActive(false);

        if (selectedEnemy)
        {
            selectedEnemy.godRay.SetActive(false);
        }
        if(selectedBlock)
        {
            selectedBlock.Highlight(true);
        }
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
    public void ToggleCaptain()
    {
        selectedEnemy.isCaptain = !selectedEnemy.isCaptain;
        captainToggle.text = "Is Captain: " + selectedEnemy.isCaptain;
    }
    public void toggleRepeatSpawn()
    {
        selectedEnemy.repeatSpawn = !selectedEnemy.repeatSpawn;
        repeatSpawn.text = "Repeat Spawn: " + selectedEnemy.repeatSpawn;
    }
    public void EditSpawnDelay()
    {
        selectedEnemy.delaySpawn = (int)delay.value;
        delayText.text = "Delay: " + delay.value;
    }
    public void EditTrigger()
    {
        selectedBlock.triggerId = (int)trigger.value;
        triggerId.text = "Trigger ID: " + trigger.value;
    }
    public void EditEnemyTriggerID()
    {
        selectedEnemy.triggerId = (int)enemyTrigger.value;
        enemyTriggerId.text = "Trigger ID: " + enemyTrigger.value;
    }
    public void ToggleEnemyTrigger()
    {
        selectedEnemy.onTrigger = !selectedEnemy.onTrigger;
        enemyOnTrigger.text = "On Trigger: " + selectedEnemy.onTrigger;
    }
}
