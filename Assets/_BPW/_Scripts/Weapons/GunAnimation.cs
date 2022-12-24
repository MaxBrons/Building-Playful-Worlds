using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public abstract class GunAnimation : WeaponAnimation
{
    protected Gun m_Gun;

    public override void Start() {
        base.Start();

        m_Gun = m_Weapon as Gun;
        if (m_Gun) {
            m_Gun.OnAttack += OnAttack;
            m_Gun.OnGunReloadStarted += OnGunReloadStarted;
            m_Gun.OnGunReloadEnded += OnGunReloadEnded;
        }
    }

    protected abstract void OnGunReloadStarted(bool magazineEmpty);
    protected abstract void OnGunReloadEnded();

    protected abstract void OnAttack();
}
