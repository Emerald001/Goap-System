using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Storage : MonoBehaviour
{
    public Item ItemItCanStore;

    public List<Item> inventory = new();
}