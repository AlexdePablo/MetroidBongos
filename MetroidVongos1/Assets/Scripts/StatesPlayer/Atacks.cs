using FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Atacks : State
{
    private string m_AnimationG;
    private string m_AnimationA;
    public float m_StateDelta;
    public float m_StateLength;
    Boolean m_Air;

    public Atacks(FSM fSM, string animationGround, string animationAir, float StateLength) : base(fSM)
    {
        m_AnimationG = animationGround;
        m_AnimationA = animationAir;
        m_StateLength = StateLength;
    }
    public override void Init()
    {
        if (m_FSM.Owner.GetComponent<Rigidbody2D>().velocity.y <= 0.1f && m_FSM.Owner.GetComponent<Rigidbody2D>().velocity.y >= -0.1f)
        {
            m_StateLength = 0.6f;
            m_FSM.Owner.GetComponent<Animator>().Play(m_AnimationG);
            m_Air = false;
        }
        else
        {
            m_StateLength = 0.4f;
            m_FSM.Owner.GetComponent<Animator>().Play(m_AnimationA);
            m_Air = true;
        }
        m_StateDelta = 0;
    }

    public override void Update()
    {
        base.Update();
        m_StateDelta += Time.deltaTime;
        if (m_StateDelta >= m_StateLength)
        {
            if (m_Air)
                m_FSM.ChangeState<InAir>();
            else
                m_FSM.ChangeState<IddleState>();
            return;
        }
    }
    public override void Exit()
    {
        m_FSM.Owner.GetComponent<movimientoBasico>().Atck = false;
    }
}
