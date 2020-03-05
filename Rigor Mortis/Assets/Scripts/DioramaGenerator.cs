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
        var bottomRight = max;

        Instantiate(new GameObject(), topLeft, new Quaternion(0, 0, 0, 0)).name = "TL";
        Instantiate(new GameObject(), topMid, new Quaternion(0, 0, 0, 0)).name = "TM"; ;
        Instantiate(new GameObject(), topRight, new Quaternion(0, 0, 0, 0)).name = "TR"; ;
        Instantiate(new GameObject(), midRight, new Quaternion(0, 0, 0, 0)).name = "MR"; ;
        Instantiate(new GameObject(), midLeft, new Quaternion(0, 0, 0, 0)).name = "ML"; ;
        Instantiate(new GameObject(), bottomLeft, new Quaternion(0, 0, 0, 0)).name = "BL"; ;
        Instantiate(new GameObject(), bottomMid, new Quaternion(0, 0, 0, 0)).name = "BM"; ;
        Instantiate(new GameObject(), bottomRight, new Quaternion(0, 0, 0, 0)).name = "BR"; ;

        FillEdge(topLeft, topMid, "TopLeft");
        FillEdge(topMid, topRight, "TopMid");
        FillEdge(topRight, topRight * 2, "TopRight");        
        FillEdge(midRight, midRight * 2, "MidRight");
        FillEdge(midLeft, midLeft * 2, "MidLeft");     
        FillEdge(bottomLeft, bottomLeft * 2, "BtmLeft");
        FillEdge(bottomMid, bottomMid * 2, "BtmMid");
        FillEdge(max, max * 2, "BtmRight");
    }

    private void GenerateMap(Vector3 start, Vector3 end, string name)
    {

    }

    private void FillEdge(Vector3 origin, Vector3 endPoint, string name)
    {
        var scalar = endPoint - origin;
        var spawnedPiece = RandPiece;
        var spawnAt = origin + max - min;
        var endAt = endPoint + max - min;
        
        var bounds = spawnedPiece.GetComponent<DioramaPiece>().Max - spawnedPiece.GetComponent<DioramaPiece>().Min;
        var scale = spawnedPiece.transform.localScale;

        
        var scaleFactor = Math.Abs(Math.Abs(scalar.x) > Math.Abs(scalar.z)? scalar.x / bounds.x : scalar.z / bounds.z);
        
        var piece = Instantiate(spawnedPiece, spawnAt, new Quaternion(), dioramaParent.transform);
        piece.name = name + scaleFactor;
        piece.transform.localScale = new Vector3(scale.x * scaleFactor, scale.y * scaleFactor, scale.z * scaleFactor);
        //piece.transform.position += bounds * scaleFactor;
    }

    private GameObject RandPiece => dioramaPieces[UnityEngine.Random.Range(0, dioramaPieces.Length - 1)];
}
