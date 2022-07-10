using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour
{
    [Range(0, 100)]
    public float WarmthValue;
    public Action ActionToReplenishWarmth;
    public float ShrinkFactorWarmth;

    [Range(0, 100)]
    public float ThirstValue;
    public Action ActionToReplenishThirst;
    public float ShrinkFactorThirst;

    [Range(0, 100)]
    public float HungerValue;
    public Action ActionToReplenishHunger;
    public float ShrinkFactorHunger;

    [Range(0, 100)]
    public float EnergyValue;
    public Action ActionToReplenishEnergy;
    public float ShrinkFactorEnergy;

    private void Update() {
        WarmthValue -= Time.deltaTime * ShrinkFactorWarmth;
        ThirstValue -= Time.deltaTime * ShrinkFactorThirst;
        HungerValue -= Time.deltaTime * ShrinkFactorHunger;
        EnergyValue -= Time.deltaTime * ShrinkFactorEnergy;
    }

    public void UpWarmth() {
        WarmthValue = 100;
    }
    public void UpThirst() {
        ThirstValue = 100;
    }
    public void UpHunger() {
        HungerValue = 100;
    }
    public void UpEnergy() {
        EnergyValue = 100;
    }
}