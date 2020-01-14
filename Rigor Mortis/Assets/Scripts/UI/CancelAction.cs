using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelAction : MonoBehaviour
{
    private UIManager uiManager;
    private Character character;
    public void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
    }

    public void CancelCurrentAction()
    {
        uiManager.DeleteCurrentPopupButtons();
        uiManager.DisplayActionButtons(character.attacks, character);
    }
}
