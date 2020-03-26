using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    //[SerializeField]GridManager gridManager;
    [SerializeField]GameObject minimap;

    private void Start() {
        GridManager.mapGenerated += SetMinimapPosition;
    }

    private void OnDestroy()
    {
        GridManager.mapGenerated -= SetMinimapPosition;
    }

    private void SetMinimapPosition(object sender, BlockScript[] e) {
        Vector3 topLeft = new Vector3();
        Vector3 bottomRight = new Vector3();

        foreach (BlockScript block in e) {
            if(block.transform.position.x < topLeft.x || (block.transform.position.x == topLeft.x && block.transform.position.z < topLeft.z)) {
                topLeft = block.transform.position;
            }
            if(block.transform.position.x > bottomRight.x || (block.transform.position.x == bottomRight.x && block.transform.position.z > bottomRight.z)) {
                bottomRight = block.transform.position;
            }
        }

        transform.position = new Vector3(topLeft.x + (bottomRight.x/2), transform.position.y, topLeft.z + (bottomRight.z/2));

        ScaleMinimap(topLeft.x + (bottomRight.x / 2), topLeft.z + (bottomRight.z / 2));

        Debug.Log(topLeft.x + (bottomRight.x / 2));
    }

    private void ScaleMinimap(float x, float y) {
        Rect tempRect = GetComponent<Camera>().rect;
        //GetComponent<Camera>().rect = new Rect(tempRect.x, tempRect.y+x, tempRect.width, tempRect.height);
    }
}
