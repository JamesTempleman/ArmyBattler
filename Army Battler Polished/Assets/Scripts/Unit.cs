using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Unit
{
    //units stats
    public string Name;
    public int[] Stats;
    public string keywords;
    public int factionID;
    public bool canFly;
    public int actionsAvaliable;
    public GameObject unit;
    public Button unitSelector;
    public Collider2D unitCollider;
    public int unitID;
    public int totalWounds;

    //weapons stats
    public string wName;
    public string wType;
    public int[] wStats;

}