using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;
using UnityEngine.PlayerLoop;

public class OndaAtack : State
{
    private string m_Animation;
    private float m_StateLength;
    private float m_StateDelta;

    public OndaAtack(FSM fSM, string animation, float StateLength) : base(fSM)
    {
        m_Animation = animation;
        m_StateLength = StateLength;
    }
    public override void Init()
    {
        m_FSM.Owner.GetComponent<Animator>().Play(m_Animation);
        m_StateDelta = 0;
    }
    public override void Update()
    {
        m_StateDelta += Time.deltaTime;
        if (m_StateDelta >= m_StateLength)
            m_FSM.ChangeState<IddleState>();
    }
}
