using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public string ActionName;
    public int WaitTime;

    public Item GivenItem;
    public int ActionCost;

    public bool HasRequirement;
    public Item RequiredItem;

    public Action ActionParent;

    public int FScore {
        get { return GScore + HScore; }
    }

    [HideInInspector] public int GScore;
    [HideInInspector] public int HScore;
    [HideInInspector] public bool IsDone = false;

    public void OnEnter() {
        StartCoroutine(WaitForSeconds(WaitTime));
    }

    public void OnUpdate() {

    }

    public void OnExit() {
        GScore = 0;
        HScore = 0;
        IsDone = false;
    }

    public IEnumerator WaitForSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);

        IsDone = true;
    }
}