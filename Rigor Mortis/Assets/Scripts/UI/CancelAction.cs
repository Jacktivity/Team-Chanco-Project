using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CancelAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PlayerManager playerManager;
    private Character character;
    public static EventHandler<BlockScript> cancelActions;
    public Text attackText;

    public void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void SetActions(Character character/*, IEnumerable<MoveButton> moveButtons*/)
    {
        this.character = character;
    }

    public void CancelCurrentAction()
    {
        playerManager.PlayerUnitChosen(character);
        attackText.text = "";
        //FindObjectOfType<PlayerCharacterMover>().ResetMovement();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        attackText.text = "Cancel";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        attackText.text = "";
    }
}
