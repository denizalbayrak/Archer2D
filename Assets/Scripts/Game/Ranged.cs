using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Enemy
{
    private RangedMovement rangedMovement;
    private RangedHealth rangedHealth;
    protected override void Start()
    {
        base.Start();

        rangedMovement = GetComponent<RangedMovement>();
        rangedHealth = GetComponent<RangedHealth>();
        if (rangedMovement != null)
        {
            rangedMovement.SetValues(Speed, Animator, Player);
        }
        if (rangedHealth != null)
        {
            rangedHealth.SetValues(Animator, Health);
        }
    }
}
