using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : GameUnit
{
    public override void Initialize(string name, int maxHealth, float speed)
    {
        Debug.Log("Warning! Boss is spawning!");
        base.Initialize(name, maxHealth, speed);
        Debug.Log("Boss has spawned!");
    }
}
