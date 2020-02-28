using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DioramaGenerator : MonoBehaviour
{
    public Vector3 min, max;
    [SerializeField] private GameObject[] dioramaPieces;
    [SerializeField] private GameObject dioramaParent;
    // Start is called before the first frame update
    void Start()
    {
        GridManager.mapGenerated += GenerateDiorama;
    }

    private void GenerateDiorama(object sender, BlockScript[] e)
    {
        min = e.First(s => s.coordinates == new Vector3(0, 0, 0)).transform.position;
        max = new Vector3(e.OrderByDescending(t => t.coordinates.x).First().transform.position.x,0, e.OrderByDescending(t => t.coordinates.z).First().transform.position.z);

        var topLeft = min - new Vector3(max.x, 0, max.z);
        var topMid = min - new Vector3(max.x, 0, 0);
        var topRight = min + new Vector3(-max.x, 0, max.z);
        var midRight = min + new Vector3(0, 0, max.z);
        var midLeft = min - new Vector3(0, 0, max.z);
        var bottomLeft = min + new Vector3(max.x, 0, -max.z);
        var bottomMid = min + new Vector3(max.x, 0, 0);

        FillEdge(topLeft, topMid, "TopLeft");
        FillEdge(topMid, topRight, "TopMid");
        FillEdge(topRight, topRight * 2, "TopRight");        
        FillEdge(midRight, midRight, "MidRight");
        FillEdge(midLeft, midLeft, "MidLeft");     
        FillEdge(bottomLeft, bottomLeft, "BtmLeft");
        FillEdge(bottomMid, bottomMid, "BtmMid");
        FillEdge(max, max, "BtmRight");
    }

    private void FillEdge(Vector3 origin, Vector3 endPoint, string name)
    {
        var spawnedPiece = RandPiece;
        var spawnAt = origin + max - min;
        var endAt = origin + max - min;
        
        var bounds = spawnedPiece.GetComponent<Collider>().bounds.max;
        var scale = spawnedPiece.transform.localScale;

        var scalar = endPoint - origin;
        var scaleFactor = scalar.x > scalar.z? bounds.x / (endPoint.x - origin.x) : bounds.z / (endPoint.z - origin.z);


        spawnAt += spawnedPiece.GetComponent<Collider>().bounds.center;
        var piece = Instantiate(spawnedPiece, spawnAt, new Quaternion(), dioramaParent.transform);
        piece.name = name + scaleFactor;
        //piece.transform.localScale = new Vector3(scale.x * scaleFactor, scale.y * scaleFactor, scale.z * scaleFactor);
    }

    private GameObject RandPiece => dioramaPieces[UnityEngine.Random.Range(0, dioramaPieces.Length - 1)];
}
