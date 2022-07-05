using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public List<Action> AllActions = new();
    public List<Action> ActionQueue = new();
    public List<Item> Inventory = new();

    public Action ActionToPerform;

    public float speed;
    public bool AtActionPosition = false;

    private void Start() {
        CreateActionQueue(ActionToPerform);
    }

    private void Update() {
        Checkup();
    }

    public void CreateActionQueue(Action action) {
        List<Action> OpenList = new();
        OpenList.Add(action);
        ActionQueue.Add(action);

        bool IsDone = false;
        int BreakoutTimer = 0;

        while(!IsDone && BreakoutTimer < 10) {
            var currentAction = OpenList[0];

            if(currentAction.HasRequirement)
                foreach (var act in GetActions(currentAction)) {
                    OpenList.Add(act);
                    ActionQueue.Add(act);
                    break;
                }

            Debug.Log(OpenList.Count);

            OpenList.RemoveAt(0);

            if (OpenList.Count < 1) {
                ActionQueue.Reverse();
                IsDone = true;
            }
            BreakoutTimer++;
        }
    }

    public void Checkup() {
        if (ActionQueue.Count == 0) {
            Debug.Log("No Actions");
            return;
        }

        if (ActionQueue[0].IsDone) {
            AtActionPosition = false;
            ActionQueue.RemoveAt(0);
            return;
        }

        if (!AtActionPosition)
            MoveToAction();
        else
            PerformAction();
    }

    public void PerformAction() {
        ActionQueue[0].OnUpdate();
    }

    public void MoveToAction() {
        if(transform.position != ActionQueue[0].transform.position)
            transform.position = Vector3.MoveTowards(transform.position, ActionQueue[0].transform.position, speed * Time.deltaTime);
        else {
            ActionQueue[0].OnEnter();
            Debug.Log("Running");
            AtActionPosition = true;
        }
    }

    public List<Action> GetActions(Action action) {
        List<Action> actions = new();

        foreach (var act in AllActions) {
            if (act.GivenItem == action.RequiredItem)
                actions.Add(act);
        }

        return actions;
    }
}

/* foreach (var item in currentAction.RequiredItem) {
                if (Inventory.Contains(item)) {
                    break;
                }
                else {
                    bool foundItem = false;
                    foreach (var storage in AllStorageObjects) {
                        bool breakout = false;
                        foreach (var stoItem in storage.inventory) {
                            if (stoItem == item) {
                                ActionQueue.Add(storage.GetComponent<GetItemAction>());
                                foundItem = true;
                                breakout = true;
                                break;
                            }
                        }
                        if (breakout)
                            break;
                    }
                    if (foundItem)
                        break;

                    foreach (var act in AllActions) {
                        bool breakout = false;
                        foreach (var effect in act.Effects) {
                            if (effect == item) {
                                OpenList.Add(act);
                                ActionQueue.Add(act);
                                breakout = true;
                                break;
                            }
                        }
                        if (breakout)
                            break;
                    }
                }
            } */