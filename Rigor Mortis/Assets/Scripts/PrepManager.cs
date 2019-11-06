using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepManager : MonoBehaviour
{
    public GridManager manager;
    public GameObject popUpButton;
    public GameObject battleCanvas;
    public GameObject prepCanvas;
    public Character unit;
    // Start is called before the first frame update
    void Start()
    {
        // -410, -218, -278
         Vector3 popUpOffset = new Vector3(0, 0, 0);
        Vector3 instantiationPoint = transform.position;// + popUpOffset;

        for (int i = 0; i < manager.playerPrefabs.Length; i++)
        {
            popUpOffset = new Vector3(0, 0, 3 * i);
            GameObject button = Instantiate(popUpButton, instantiationPoint + popUpOffset, prepCanvas.transform.rotation, prepCanvas.transform);
            button.GetComponentInChildren<Text>().text = manager.playerPrefabs[i].name;
        }
    }

    public void assignUnit(Character inputUnit)
    {
        unit = inputUnit;
       
    }
}
