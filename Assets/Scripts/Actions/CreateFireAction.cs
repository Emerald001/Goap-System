using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFireAction : Action
{
    public override void OnEnter() {
        StartCoroutine(WaitForSeconds(2));
    }

    public override void OnUpdate() {

    }
}