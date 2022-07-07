using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public List<Action> ActionQueue = new();

    public Action ActionToPerform;

    public float speed;
    public bool AtActionPosition = false;
    
    private Action[] AllActions;

    private void Start() {
        AllActions = FindObjectsOfType<Action>();

        CreateActionQueue(ActionToPerform);
    }

    private void Update() {
        Checkup();
    }

    public void CreateActionQueue(Action Goal) {
        List<Action> OpenList = new();
        List<Action> ClosedList = new();

        OpenList.Add(Goal);
        ActionQueue.Add(Goal);

        bool IsDone = false;
        int BreakoutTimer = 0;

        while(!IsDone && BreakoutTimer < 10) {
            var currentAction = OpenList[0];

            for (int i = 1; i < OpenList.Count; i++) {
                if (OpenList[i].FScore < currentAction.FScore || (OpenList[i].FScore == currentAction.FScore && OpenList[i].ActionCost < currentAction.ActionCost)) {
                    currentAction = OpenList[i];
                }
            }

            ClosedList.Add(currentAction);
            OpenList.Remove(currentAction);

            if (!currentAction.HasRequirement) {
                ActionQueue.Reverse();
                IsDone = true;
            }

            foreach (var action in GetActions(currentAction)) {
                if (ClosedList.Contains(action))
                    continue;

                int newActionCost = currentAction.GScore + action.ActionCost;
                if (newActionCost < action.GScore || !OpenList.Contains(action)) {
                    action.GScore = newActionCost;

                    if (!OpenList.Contains(action)) {
                        OpenList.Add(action);
                    }
                }

                var best = GetActions(currentAction).OrderBy(Action => Action.ActionCost).First();
                OpenList.Add(best);
                ActionQueue.Add(best);
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