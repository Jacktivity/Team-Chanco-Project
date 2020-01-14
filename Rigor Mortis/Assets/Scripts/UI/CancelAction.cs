using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CancelAction : MonoBehaviour
{
    private PlayerManager playerManager;
    private Character character;
    public static EventHandler<BlockScript> cancelActions;
    public void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void SetActions(Character character, IEnumerable<MoveButton> moveButtons)
    {
        this.character = character;
    }

    public void CancelCurrentAction()
    {
        playerManager.PlayerUnitChosen(character);
        FindObjectOfType<PlayerCharacterMover>().ResetMovement();
    }
}
