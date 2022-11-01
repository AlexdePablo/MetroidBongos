using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;

public class Damage : State
{
    private string m_Animation;
    float m_StateDelta;
    public Damage(FSM fSM, string animation) : base(fSM)
    {
        m_Animation = animation;
    }
    public override void Init()
    {
        m_FSM.Owner.GetComponent<movimientoBasico>().m_addVelocity = 1;
        m_FSM.Owner.GetComponent<movimientoBasico>().m_PlayerControls.Player.Disable();
        m_FSM.Owner.GetComponent<Animator>().Play(m_Animation);
        m_FSM.Owner.GetComponent<movimientoBasico>().m_Damage.Raise(0.3f);
    }
    public override void Update()
    {
        base.Update();
        RaycastHit2D hit = Physics2D.Raycast(m_FSM.Owner.transform.position, -m_FSM.Owner.transform.up, 0.5f, m_FSM.Owner.GetComponent<movimientoBasico>().LayermaskMGround);
        if (hit.collider != null && m_FSM.Owner.GetComponent<Rigidbody2D>().velocity.y < 0)
            m_FSM.ChangeState<IddleState>();
    }
    public override void Exit()
    {
        m_FSM.Owner.GetComponent<movimientoBasico>().m_PlayerControls.Player.Enable();
    }
}
