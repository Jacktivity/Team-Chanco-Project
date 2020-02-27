using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DioramaGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] dioramaPieces;
    [SerializeField] private GameObject dioramaParent;
    // Start is called before the first frame update
    void Start()
    {
        GridManager.mapGenerated += GenerateDiorama;
    }

    private void GenerateDiorama(object sender, BlockScript[] e)
    {
        var min = e.First(s => s.coordinates == new Vector3(0, 0, 0)).transform.position;
        var max = new Vector3(e.OrderByDescending(t => t.coordinates.x).First().transform.position.x,0, e.OrderByDescending(t => t.coordinates.z).First().transform.position.z);

        FillEdge(min, max, min - new Vector3(max.x, 0, 0));
        FillEdge(min, max, min + new Vector3(max.x, 0, 0));
        FillEdge(min, max, min + new Vector3(0, 0, max.z));
        FillEdge(min, max, min - new Vector3(0, 0, max.z));
        FillEdge(min, max, min + new Vector3(-max.x, 0, max.z));
        FillEdge(min, max, min - new Vector3(max.x, 0, max.z));
        FillEdge(min, max, min + new Vector3(max.x, 0, -max.z));
        FillEdge(min, max, max);
    }

    private void FillEdge(Vector3 min, Vector3 max, Vector3 origin)
    {
        var piece = Instantiate(RandPiece, origin, new Quaternion(), dioramaParent.transform);
        var bounds = piece.GetComponent<Collider>().bounds.max;
        var scale = piece.transform.localScale;

        var scaleRequirements = new float[] { scale.x / bounds.x, scale.z / bounds.z };
        var scaleFactor = scaleRequirements.Max();

        piece.transform.localScale = new Vector3(scale.x * scaleFactor, scale.y * scaleFactor, scale.z * scaleFactor);
    }

    private GameObject RandPiece => dioramaPieces[UnityEngine.Random.Range(0, dioramaPieces.Length - 1)];
}
