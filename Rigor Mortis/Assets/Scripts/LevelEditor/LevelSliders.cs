

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LevelSliders : MonoBehaviour
{
    public Slider xSlider;
    public Slider zSlider;
    public Text xText;
    public Text zText;
    public Text pText;
    public Slider pSlider;
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
        xText.text = "X: " + xSlider.value.ToString();
        zText.text = "Z: " + zSlider.value.ToString();
        pText.text = "Player Points: " + (pSlider.value * 5).ToString();

    }

    public void generate()
    {
        var map = new List<BlockScript>();

        int j = 0;
        for(int i = 0; i < xSlider.value; i++)
        {
            for (j = 0; j < zSlider.value; j++)
            {
                var tile = Instantiate(block, new Vector3(j, 0, i), block.transform.rotation);
                tile.transform.parent = blockContainer.transform;
                tile.coordinates = new Vector3(j, 0, i);
                map.Add(tile);

            }
        }
        for (int i = 0; i < map.Count; i++)
        {
            var tile = map[i];
            var pos = tile.coordinates;

                var blockscript = tile.GetComponent<BlockScript>();
                blockscript.coordinates = new Vector3(pos.x, pos.y, pos.z);
                tile.name = tile.name.Replace("(Clone)", "");
                tile.name = tile.name + '(' + pos.x + ',' + pos.y + ',' + pos.z + ')';


                var sBlock = map.FirstOrDefault(t => t.coordinates == blockscript.coordinates + new Vector3(0, 0, -1));
                if (sBlock != null)
                {
                    sBlock.N = tile.gameObject;
                    blockscript.S = sBlock.gameObject;
                }
                var swBlock = map.FirstOrDefault(t => t.coordinates == blockscript.coordinates + new Vector3(-1, 0, -1));
                if (swBlock != null)
                {
                    swBlock.NE = tile.gameObject;
                    blockscript.SW = swBlock.gameObject;
                }
                var wBlock = map.FirstOrDefault(t => t.coordinates == blockscript.coordinates + new Vector3(-1, 0, 0));
                if (wBlock != null)
                {
                    wBlock.E = tile.gameObject;
                    blockscript.W = wBlock.gameObject;
                }
                var nwBlock = map.FirstOrDefault(t => t.coordinates == blockscript.coordinates + new Vector3(-1, 0, 1));
                if (nwBlock != null)
                {
                    nwBlock.SE = tile.gameObject;
                    blockscript.NW = nwBlock.gameObject;
                }
                var below = map.FirstOrDefault(t => t.coordinates == blockscript.coordinates + new Vector3(0, -1, 0));
                if (below != null)
                {
                    below.occupier = tile.gameObject;
                }
        }

        generatorCanvas.enabled = false;
        placementCanvas.GetComponent<Canvas>().enabled = true;
        placement.enabled = true;
        placement.MapGenerated();

        GridManager.mapGenerated?.Invoke(this, map.ToArray());
        
    }
}
