using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Enemy
{
    private MeleeMovement meleeMovement;
    private MeleeHealth meleeHealth;
    protected override void Start()
    {
        base.Start();

        meleeMovement = GetComponent<MeleeMovement>();
        meleeHealth = GetComponent<MeleeHealth>();
        if (meleeMovement != null)
        {
            meleeMovement.SetValues(Speed, Animator, Player);
        }
        if (meleeHealth != null)
        {
            meleeHealth.SetValues(Animator, Health);
        }
    }
}
