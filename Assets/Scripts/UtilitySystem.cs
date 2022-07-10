using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilitySystem : MonoBehaviour
{
    private Agent Agent;
    private BlackBoard Blackboard;

    private List<float> Values = new();

    [HideInInspector] public bool PickNewAction;
    
    private void Start() {
        Agent = GetComponent<Agent>();
        Blackboard = GetComponent<BlackBoard>();
    }

    void Update() {
        if (PickNewAction) {
            PickAction();
        }
    }

    public void PickAction() {
        float lowestScoringValue = Mathf.Infinity;

        Values.Add(Blackboard.WarmthValue);
        Values.Add(Blackboard.ThirstValue);
        Values.Add(Blackboard.HungerValue);
        Values.Add(Blackboard.EnergyValue);

        foreach (var value in Values) {
            if (value < lowestScoringValue)
                lowestScoringValue = value;
        }

        if (lowestScoringValue == Blackboard.WarmthValue) {
            Agent.CreateActionQueue(Blackboard.ActionToReplenishWarmth);
            Blackboard.UpWarmth();
        }
        else if (lowestScoringValue == Blackboard.ThirstValue) {
            Agent.CreateActionQueue(Blackboard.ActionToReplenishThirst);
            Blackboard.UpThirst();
        }
        else if (lowestScoringValue == Blackboard.HungerValue) {
            Agent.CreateActionQueue(Blackboard.ActionToReplenishHunger);
            Blackboard.UpHunger();
        }
        else if (lowestScoringValue == Blackboard.EnergyValue) {
            Agent.CreateActionQueue(Blackboard.ActionToReplenishEnergy);
            Blackboard.UpEnergy();
        }

        Values.Clear();
    }
}