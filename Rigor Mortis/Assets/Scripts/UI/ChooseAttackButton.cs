using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAttackButton : MonoBehaviour
{
    //public AttackManager attackManager;
    public Character character;
    public Attack attack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseAttack() => character.selectedAttack = attack;//uiManager.Attack();
}
