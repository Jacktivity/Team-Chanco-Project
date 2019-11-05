using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject[] Units => transform.GetComponentsInChildren<GameObject>();
    private AIStates currentPlayState = AIStates.Attack;

    public void MoveUnit()
    {
        //var unitToMove = Units.First(u => u.AddComponent<Character>())
    }
}

public enum AIStates
{
    Regroup,Retreat,Attack
}
