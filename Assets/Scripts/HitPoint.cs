using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    [SerializeField] Target targetParent;
    [SerializeField] float dmgModifier;

    public void OnHit(int dmg)
    {
        targetParent.TakeDamage((int)(dmg * dmgModifier));
    }
}
