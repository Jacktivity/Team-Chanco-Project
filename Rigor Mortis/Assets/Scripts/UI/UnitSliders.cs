using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnitSliders : MonoBehaviour
{
    public Character unit;
    public Slider healthSlider;
    public Slider manaSlider;
    public Vector3 offset;
    public Scene activeScene;

    public Vector3 originalScale;
    bool manaEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        activeScene = SceneManager.GetActiveScene();
        healthSlider.maxValue = unit.GetHealth;
        healthSlider.value = unit.GetHealth;

        if(unit.maxManaPoints <= 0)
        {
            manaSlider.gameObject.SetActive(false);
            manaEnabled = false;
        } else
        {
            manaEnabled = true;
            manaSlider.maxValue = unit.maxManaPoints;
            manaSlider.value = unit.manaPoints;
        }

        Offset();
        originalScale = healthSlider.transform.localScale;
    }

    void Offset() {
        switch (unit.name) {
            case "Necromancer":
                offset.y = 1.95f;
                break;
            case "Skeleton":
                offset.y = 1.65f;
                break;
            case "Skeleton_Axe":
                offset.y = 1.65f;
                break;
            case "Skeleton_Rifle":
                offset.y = 1.65f;
                break;
            case "Skeleton_Spear":
                offset.y = 1.65f;
                break;
            case "FlamingSkull":
                offset.y = 2.2f;
                break;
            default:
                offset.y = 1;
                break;
        }
        healthSlider.transform.position = unit.transform.position + offset;
    }

    public void Update()
    {
        healthSlider.transform.position = unit.transform.position + offset;

        healthSlider.transform.LookAt(FindObjectOfType<Camera>().transform);
        healthSlider.transform.Rotate(0, 180, 0, Space.Self);
    }
}
