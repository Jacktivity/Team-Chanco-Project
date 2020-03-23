using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FloatingText : MonoBehaviour
{
    private Character spawnCharacter;
    public Camera lookAtCam;
    [SerializeField] private Vector3 movement;
    [SerializeField] private float lifeTime = 3f;
    private float counter = 0f;

    public void SetUp(Character spawner, string message, Color messageColor)
    {
        spawnCharacter = spawner;

        var text = GetComponent<UnityEngine.UI.Text>();
        text.text = message;

        var colourText = GetComponentsInChildren<UnityEngine.UI.Text>().First(c => c.gameObject != text.gameObject);
        colourText.text = message;
        colourText.color = messageColor;
        lookAtCam = FindObjectOfType<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAtCam.transform.position);
        transform.Rotate(0, 180, 0, Space.Self);
        transform.position = spawnCharacter.transform.position + movement * counter;
        counter += Time.deltaTime;

        if (counter >= lifeTime)
            Destroy(gameObject);
    }
}

public struct SpawnFloatingTextEventArgs
{
    public Character character;
    public string message;
    public Color textColour;

    public SpawnFloatingTextEventArgs(Character unit, string text, Color colour)
    {
        character = unit;
        message = text;
        textColour = colour;
    }
}
