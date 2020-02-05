

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSliders : MonoBehaviour
{
    public Slider xSlider;
    public Slider zSlider;
    public Text xText;
    public Text zText;

    public BlockScript block;
    public Canvas generatorCanvas;
    public GameObject placementCanvas;
    public GameObject blockContainer;
    public Placement placement;

    // Start is called before the first frame update
    void Start()
    {
        xSlider.value = 0;
        zSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        xText.text = xSlider.value.ToString();
        zText.text = zSlider.value.ToString();

    }

    public void generate()
    {
        int j = 0;
        for(int i = 0; i <= xSlider.value; i++)
        {
            for (j = 0; j <= zSlider.value; j++)
            {
                var tile = Instantiate(block, new Vector3(i, 0, j), block.transform.rotation);
                tile.transform.parent = blockContainer.transform;
                
            }
        }

        generatorCanvas.enabled = false;
        //placementCanvas.GetComponent<Canvas>().enabled = true;
        placement.enabled = true;
        
    }
}
