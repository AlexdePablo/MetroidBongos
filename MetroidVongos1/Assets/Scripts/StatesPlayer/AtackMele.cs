using FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AtackMele : Atacks
{
    public AtackMele(FSM fSM, string animationGround, string animationAir, float StateLength) : base(fSM, animationGround, animationAir, StateLength) { }
    public override void Init()
    {
        base.Init();
        m_FSM.Owner.GetComponent<movimientoBasico>().m_PlayerControls.Player.AtackMele.started += combo;
    }
    private void combo (InputAction.CallbackContext context)
    {
        if (base.m_StateLength == 0.6f && base.m_StateDelta >= 0.5f && base.m_StateDelta <= 0.6f)
            m_FSM.ChangeState<OndaAtack>();
    }
    public override void Exit()
    {
        base.Exit();
        m_FSM.Owner.GetComponent<movimientoBasico>().m_PlayerControls.Player.AtackMele.started -= combo;
    }
}
