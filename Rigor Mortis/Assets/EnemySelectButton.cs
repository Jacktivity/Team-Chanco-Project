﻿using System;
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
    }

    public void SelectTarget()
    {
        character.attackSourceBlock = target.floor;
        character.Attack();
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