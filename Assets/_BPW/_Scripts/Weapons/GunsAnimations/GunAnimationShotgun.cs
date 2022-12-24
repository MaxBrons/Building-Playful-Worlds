using System.Collections;
using UnityEngine;


public class GunAnimationShotgun : GunAnimation
{
    private const string ATTACK_TRIGGER_NAME = "ShouldFire";
    private const string RELOAD_BOOL_NAME = "ShouldReload";

    protected override void OnAttack() {
        m_Animator.SetTrigger(ATTACK_TRIGGER_NAME);
    }

    protected override void OnGunReloadStarted(bool magazineEmpty) {
        m_Animator.SetBool(RELOAD_BOOL_NAME, true);
    }

    protected override void OnGunReloadEnded() {
        m_Animator.SetBool(RELOAD_BOOL_NAME, false);
    }
}
