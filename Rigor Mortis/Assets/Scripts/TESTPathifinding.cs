using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TESTPathifinding : MonoBehaviour
{
    Pathfinder path;
    // Start is called before the first frame update
    void Start()
    {
        path = GetComponent<Pathfinder>();
        ColourArea();
    }

    public void ColourArea()
    {
        foreach (var tile in path.Map)
        {
            tile.MoveModifier = UnityEngine.Random.Range(0.9f, 1.1f);
            tile.gameObject.GetComponent<Renderer>().material.color = new Color(tile.MoveModifier / 5, tile.MoveModifier / 5, tile.MoveModifier / 5);
        }
        var randomTile = path.Map[UnityEngine.Random.Range(0, path.Map.Length-1)];

        Debug.Log(randomTile.transform.name);

        var foundTiles = path.GetTilesInRange(randomTile, 4f, false);

        Debug.Log(foundTiles.Length);

        Debug.Log(string.Join(Environment.NewLine, foundTiles.Select(s => s.name)));

        var randomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);

        foreach (var gameObj in foundTiles)
        {
            gameObj.gameObject.GetComponent<Renderer>().material.color = randomColor;
        }
    }

    // Update is called once per frame
    void Update()
    {        
    }
}
