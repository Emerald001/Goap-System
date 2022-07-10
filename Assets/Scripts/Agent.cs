using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Agent : MonoBehaviour
{
    public List<Action> ActionQueue = new();

    public float speed;
    public bool atActionPosition = false;

    public Text InformationText;

    private Action[] AllActions;
    private UtilitySystem UtilitySystem;

    private void Start() {
        AllActions = FindObjectsOfType<Action>();
        UtilitySystem = GetComponent<UtilitySystem>();
    }

    private void Update() {
        Checkup();
    }

    public void CreateActionQueue(Action goal) {
        List<Action> openList = new();
        List<Action> closedList = new();

        openList.Add(goal);

        bool isDone = false;
        int breakoutTimer = 0;

        while(!isDone && breakoutTimer < 100) {
            var currentAction = openList[0];

            for (int i = 1; i < openList.Count; i++) {
                if (openList[i].FScore < currentAction.FScore || (openList[i].FScore == currentAction.FScore && openList[i].HScore < currentAction.HScore)) {
                    currentAction = openList[i];
                }
            }

            closedList.Add(currentAction);
            openList.Remove(currentAction);

            if (!currentAction.HasRequirement) {
                while (currentAction != goal) {
                    ActionQueue.Add(currentAction);
                    currentAction = currentAction.ActionParent;
                }
                ActionQueue.Add(goal);
                break;
            }

            foreach (var action in GetActions(currentAction)) {
                if (closedList.Contains(action))
                    continue;

                int newActionCost = currentAction.GScore + action.ActionCost + Mathf.RoundToInt(Vector3.Distance(currentAction.transform.position, action.transform.position));

                if (newActionCost < action.GScore || !openList.Contains(action)) {
                    action.GScore = newActionCost;
                    action.HScore = action.ActionCost + Mathf.RoundToInt(Vector3.Distance(currentAction.transform.position, action.transform.position));
                    action.ActionParent = currentAction;

                    if (!openList.Contains(action)) {
                        openList.Add(action);
                    }
                }
            }

            breakoutTimer++;
        }

        UtilitySystem.PickNewAction = false;
    }

    public void Checkup() {
        if (ActionQueue.Count == 0) {
            InformationText.text = "Done With Actions";
            UtilitySystem.PickNewAction = true;
            return;
        }

        if (ActionQueue[0].IsDone) {
            atActionPosition = false;
            ActionQueue[0].OnExit();
            ActionQueue.RemoveAt(0);
            return;
        }

        if (!atActionPosition) {
            MoveToAction();
            InformationText.text = "Moving to: " + ActionQueue[0].ActionName;
        }
        else {
            PerformAction();
            InformationText.text = "Performing: " + ActionQueue[0].ActionName;
        }
    }

    public void PerformAction() {
        ActionQueue[0].OnUpdate();
    }

    public void MoveToAction() {
        if(transform.position != ActionQueue[0].transform.position)
            transform.position = Vector3.MoveTowards(transform.position, ActionQueue[0].transform.position, speed * Time.deltaTime);
        else {
            ActionQueue[0].OnEnter();
            atActionPosition = true;
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