using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class WeaponAnimation : MonoBehaviour
{
    protected Animator m_Animator;
    protected Weapon m_Weapon;

    public virtual void Start() {
        m_Animator = GetComponent<Animator>();
        m_Weapon = GetComponent<Weapon>();
    }
}
