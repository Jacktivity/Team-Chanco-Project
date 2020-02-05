using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public Canvas placementCanvas;
    public GameObject activeBlock;

    GameObject locationBlock;
    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var targetPos = locationBlock.transform.position;
            var placedBlock = Instantiate(activeBlock, new Vector3(targetPos.x, targetPos.y + 1, targetPos.z), locationBlock.transform.rotation);

        }

        ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            locationBlock = hit.collider.gameObject;
        }
    }

}
