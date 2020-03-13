using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DioramaGenerator : MonoBehaviour
{
    private Vector3 min, max;
    private Vector3 mapStart, mapEnd;
    [SerializeField] private DioramaPiece[] dioramaPieces;
    [SerializeField] private GameObject dioramaParent;
    [SerializeField] private Vector3 dioramaSize;
    public Vector2 averageDioramaSize;
    // Start is called before the first frame update
    void Start()
    {
        GridManager.mapGenerated += GenerateDiorama;

        float avgX = 0f, avgY = 0f;        

        foreach (var piece in dioramaPieces)
        {
            var size = piece.Max - piece.Min;
            avgX += size.x < 0? -size.x : size.x;
            avgY += size.z < 0? -size.z : size.z;
        }

        averageDioramaSize = new Vector2(avgX / dioramaPieces.Length, avgY / dioramaPieces.Length);
    }

    private void GenerateDiorama(object sender, BlockScript[] e)
    {

        mapStart = e.First(s => s.coordinates == new Vector3(0, 0, 0)).transform.position;
        mapEnd = new Vector3(e.OrderByDescending(t => t.coordinates.x).First().transform.position.x,0, e.OrderByDescending(t => t.coordinates.z).First().transform.position.z);
        min = mapStart - dioramaSize;
        max = mapEnd + dioramaSize;

        var area = (max - min);
        int columns = (int)(area.x / averageDioramaSize.x);
        int rows = (int)(area.z / averageDioramaSize.y);

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                var position = min + new Vector3(averageDioramaSize.x * x, 0, averageDioramaSize.y * y);
                var piece = RandPiece;
                var bounds = (piece.Max - piece.Min) / 2;                

                if (PieceOverlapsMap(position, piece, bounds) == false)
                {
                    var section = Instantiate(piece.gameObject, position, new Quaternion(0, 0, 0, 0),transform);
                    section.name += " Cord: " + x + "," + y;
                    //section.transform.Rotate(new Vector3(0,UnityEngine.Random.Range(0, 4) * 90,0), Space.World);
                }
            }
        }
    }

    private bool PieceOverlapsMap(Vector3 position, DioramaPiece piece, Vector3 bounds)
    {
        return (position + piece.Center + bounds).x < mapEnd.x &&
            (position + piece.Center + bounds).z < mapEnd.z &&
            (position + piece.Center - bounds).x > mapStart.x &&
            (position + piece.Center - bounds).z > mapStart.z;
    }   

    private DioramaPiece RandPiece => dioramaPieces[UnityEngine.Random.Range(0, dioramaPieces.Length - 1)];
}
