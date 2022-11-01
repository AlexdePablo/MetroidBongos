using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AtackShoot : Atacks
{
    public AtackShoot(FSM fSM, string animationGround, string animationAir, float StateLength) : base(fSM, animationGround, animationAir, StateLength) { }
    public override void Init()
    {
        base.Init();
        m_FSM.Owner.GetComponent<movimientoBasico>().m_PlayerControls.Player.AtackShoot.started += combo;
    }
    private void combo(InputAction.CallbackContext context)
    {
        if (base.m_StateLength == 0.6f && base.m_StateDelta >= 0.3f && base.m_StateDelta <= 0.4f || base.m_StateLength == 0.4f && base.m_StateDelta >= 0.2f && base.m_StateDelta <= 0.3f)
        {
            m_FSM.Owner.GetComponent<movimientoBasico>().BlacHoleShoot = true;
        }
    }
    public override void Exit()
    {
        base.Exit();
        m_FSM.Owner.GetComponent<movimientoBasico>().m_PlayerControls.Player.AtackShoot.started -= combo;
    }
}
