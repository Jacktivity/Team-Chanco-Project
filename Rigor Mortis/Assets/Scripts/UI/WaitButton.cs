using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WaitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text attackText;
    public GridManager gridManager;

    public void ButtonClicked()
    {
        attackText.text = "";
        gridManager.ClearMap();
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
