using System.Collections;
using UnityEngine;


public class GunAnimationAssaultRifle : GunAnimation
{
    private const string ATTACK_TRIGGER_NAME = "ShouldFire";
    private const string RELOAD_TRIGGER_NAME = "ShouldReload";

    protected override void OnAttack() {
        m_Animator.SetTrigger(ATTACK_TRIGGER_NAME);
    }

    protected override void OnGunReloadStarted(bool magazineEmpty) {
        m_Animator.SetTrigger(RELOAD_TRIGGER_NAME);
    }

    protected override void OnGunReloadEnded() {
        m_Animator.ResetTrigger(RELOAD_TRIGGER_NAME);
    }
}
