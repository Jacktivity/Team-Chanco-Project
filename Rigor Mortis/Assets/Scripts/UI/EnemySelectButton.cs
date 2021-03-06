﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemySelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public AttackManager attackManager;
    public static EventHandler pointerExit;
    public Text attackText;
    public GridManager gridManager;
    public Character character, target;
    public Attack attackChosen;
    public UIManager uiManager;
    public String previousText;
    public String hitStatPreviousText;
    public Button button;
    //Why not make an array? And default 0?
    public Sprite necromancerSprite, skeletonSprite, floatingSkullSprite, rifleSkeletonSprite, axeSkeletonSprite, spearSkeletonSprite;
    public Sprite necromancerSpriteHL, skeletonSpriteHL, floatingSkullSpriteHL, rifleSkeletonSpriteHL, axeSkeletonSpriteHL, spearSkeletonSpriteHL;

    void Awake() {
        button = GetComponent<Button>();
    }

    public void AssignData(Character attacker, Character target, Attack attack)
    {
        character = attacker;
        this.target = target;
        attackChosen = attack;
        gridManager = FindObjectOfType<GridManager>();

        target.beingAttacked = true;
        target.beingAttackedButton = this;

        SetTargetSprite(target);
    }

    public void SelectTarget()
    {
        character.attackSourceBlock = target.floor;
        character.Attack();
        target.godRay.SetActive(false);
        gridManager.ClearMap();
        uiManager.DisableAPText();
        uiManager.ResetPanelSize();
        attackText.text = "";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Highlight enemy
        target.godRay.SetActive(true);
        attackText.text = target.name;
        uiManager.HitStatTextActive(target);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Unhighlight enemy
        target.godRay.SetActive(false);
        attackText.text = previousText;
        uiManager.HitStatTextDeactivate();
    }

    public void SetTargetSprite(Character target) {
        switch (target.ID) {
            case 0:
                TargetButtonSpriteSwitch(necromancerSprite, necromancerSpriteHL);
                break;
            case 1:
                TargetButtonSpriteSwitch(skeletonSprite, skeletonSpriteHL);
                break;
            case 2:
                TargetButtonSpriteSwitch(floatingSkullSprite, floatingSkullSpriteHL);
                break;
            case 3:
                TargetButtonSpriteSwitch(rifleSkeletonSprite, rifleSkeletonSpriteHL);
                break;
            case 4:
                TargetButtonSpriteSwitch(axeSkeletonSprite, axeSkeletonSpriteHL);
                break;
            case 5:
                TargetButtonSpriteSwitch(spearSkeletonSprite, spearSkeletonSpriteHL);
                break;
            default:
                TargetButtonSpriteSwitch(necromancerSprite, necromancerSpriteHL);
                Debug.LogError("Missing switch case for attack ID " + target.ID);
                break;
        }
    }

    public void TargetButtonSpriteSwitch(Sprite sprite, Sprite spriteHL) {
        SpriteState st = new SpriteState();
        st = button.spriteState;

        if (sprite == null || spriteHL == null) {
            sprite = necromancerSprite;
            spriteHL = necromancerSpriteHL;

            Debug.Log("Missing sprite for character ID " + target.ID + " (" + target.name + ")");
        }

        button.GetComponent<Image>().sprite = sprite;
        st.pressedSprite = spriteHL;
        st.highlightedSprite = spriteHL;
        button.spriteState = st;
    }
}
