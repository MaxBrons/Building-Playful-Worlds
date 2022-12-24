using System.Collections;
using UnityEngine;

public class GunAnimationSniper : GunAnimation
{
    private const string ATTACK_TRIGGE_RNAME = "ShouldFire";
    private const string RELOAD_TRIGGER_NAME = "ShouldReload";
    private const string RELOAD_SINGLE_BOOL_NAME = "ShouldReloadSingle";

    protected override void OnAttack() {
        m_Animator.SetTrigger(ATTACK_TRIGGE_RNAME);
    }

    protected override void OnGunReloadStarted(bool magazineEmpty) {
        m_Animator.SetBool(RELOAD_SINGLE_BOOL_NAME, !magazineEmpty);
        m_Animator.SetTrigger(RELOAD_TRIGGER_NAME);
    }

    protected override void OnGunReloadEnded() {
        m_Animator.SetBool(RELOAD_SINGLE_BOOL_NAME, false);
    }
}