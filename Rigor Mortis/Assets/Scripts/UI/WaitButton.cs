using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class WaitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text attackText;
    public GridManager gridManager;
    public UIManager uiManager;
    public Character character;
    public String previousText;

    public void ButtonClicked()
    {
        attackText.text = "";
        gridManager.ClearMap();
        //uiManager.CreateCancelButton(character);
        attackText.text = previousText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        attackText.text = "Wait";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        attackText.text = previousText;
    }
}
