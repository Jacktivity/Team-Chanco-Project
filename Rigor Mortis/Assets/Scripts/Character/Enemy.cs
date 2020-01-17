using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public BlockScript SpawnBlock {get; private set;}
    public AIStates DefaultBehaviour { get; private set; }
    public int UnitID { get; private set; }
    public int[] LinkedUnitIDs { get; private set; }
    public Character Unit { get; private set; }
    public static EventHandler<AIStateChangeEvent> stateChange;

    public void AssignData(EnemySpawn spawnData)
    {
        SpawnBlock = spawnData.unit.floor;
        DefaultBehaviour = spawnData.defaultState;
        UnitID = spawnData.id;
        LinkedUnitIDs = spawnData.linkedUnits;
        Unit = spawnData.unit;
    }   
    
    public struct AIStateChangeEvent
    {
        public int changeUnitID;
        public AIStates stateToChangeTo;

        public AIStateChangeEvent(AIStates state, int linkedUnitID)
        {
            stateToChangeTo = state;
            changeUnitID = linkedUnitID;
        }
    }
}
