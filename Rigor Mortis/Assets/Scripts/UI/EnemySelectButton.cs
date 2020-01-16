using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemySelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public AttackManager attackManager;
    public static EventHandler pointerExit;
    public GridManager gridManager;
    public Character character, target;

    public void AssignData(Character attacker, Character target)
    {
        character = attacker;
        this.target = target;
        gridManager = FindObjectOfType<GridManager>();

        target.beingAttacked = true;
        target.beingAttackedButton = this;
    }

    public void SelectTarget()
    {
        character.attackSourceBlock = target.floor;
        character.Attack();
        target.godRay.SetActive(false);
        gridManager.ClearMap();
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        //Highlight enemy
        target.godRay.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Unhighlight enemy
        target.godRay.SetActive(false);
    }
}
