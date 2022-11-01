using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;

public abstract class Attacks : State
{
    string m_animation;
    float m_StateDelta;
    float m_StateLength;
    public Attacks(FSM fSM, string animation, float StateLenght) : base(fSM)
    {
        m_animation = animation; 
        m_StateLength = StateLenght;

    }

    public override void Init()
    {
        m_FSM.Owner.GetComponent<Animator>().Play(m_animation);
        m_StateDelta = 0;
    }

    public override void Update()
    {
        base.Update();
        m_StateDelta += Time.deltaTime;
        if (m_StateDelta >= m_StateLength)
        {
            m_FSM.ChangeState<Guarding>();
        }
    }
}
