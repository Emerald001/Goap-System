using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public string ActionName;

    public Item GivenItem;
    public int GivenAmount;
    public int ActionCost;
    public bool HasRequirement;
    public Item RequiredItem;
    public int RequiredAmount;

    [HideInInspector] public bool IsDone = false;

    public int FScore {
        get { return GScore + HScore; }
    }

    [HideInInspector] public int GScore;
    [HideInInspector] public int HScore;

    public abstract void OnEnter();

    public abstract void OnUpdate();

    public IEnumerator WaitForSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);

        IsDone = true;
    }
}