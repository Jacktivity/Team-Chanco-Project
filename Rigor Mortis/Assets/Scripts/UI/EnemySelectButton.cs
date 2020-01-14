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
    }

    public void SelectTarget()
    {
        character.attackSourceBlock = target.floor;
        character.Attack();
        target.GetComponentInChildren<Renderer>().material.color = Color.white;
        gridManager.ClearMap();
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        //Highlight enemy
        target.GetComponentInChildren<Renderer>().material.color = Color.cyan;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Unhighlight enemy
        target.GetComponentInChildren<Renderer>().material.color = Color.white;
    }
}
