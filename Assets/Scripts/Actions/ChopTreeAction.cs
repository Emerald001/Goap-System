using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopTreeAction : Action
{
    public override void OnEnter() {

    }

    public override void OnUpdate() {
        StartCoroutine(WaitForSeconds(3));
    }
}