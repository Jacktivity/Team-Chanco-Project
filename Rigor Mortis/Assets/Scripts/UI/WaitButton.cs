using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WaitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text attackText;
    public GridManager gridManager;
    public UIManager uiManager;
    public Character character;

    public void ButtonClicked()
    {
        attackText.text = "";
        gridManager.ClearMap();
        uiManager.CreateCancelButton(character);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        attackText.text = "Wait";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        attackText.text = "";
    }
}
