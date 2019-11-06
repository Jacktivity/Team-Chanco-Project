using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAttackButton : MonoBehaviour
{
    public UIManager uiManager;
    public Attacks attack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseAttack()
    {
        uiManager.AssignAttack(attack);
        //uiManager.Attack();
    }
}
