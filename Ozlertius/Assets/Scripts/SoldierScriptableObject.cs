using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Soldier", menuName = "ScriptableObjects/Soldier")]

public class SoldierScriptableObject : ScriptableObject
{
    public GameObject prefab;

    public int _health = 100;
    public int _damage = 20;
}
