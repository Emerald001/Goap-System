using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemAction : Action
{
    public override void OnEnter() {
        StartCoroutine(WaitForSeconds(1));
    }

    public override void OnUpdate() {

    }
}