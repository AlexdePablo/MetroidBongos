using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : State
{

    string m_animation;
    float m_StateDelta;
    float m_StateLength;
    Rigidbody2D rigidBody2d;
    EnemySword es;
    public SwordAttack(FSM fSM, string animation, float StateLenght) : base(fSM)
    {
        m_animation = animation;
        m_StateLength = StateLenght;
        es = m_FSM.Owner.GetComponent<EnemySword>();    
        rigidBody2d = m_FSM.Owner.GetComponent<Rigidbody2D>();
    }

    public override void Init()
    {
        m_FSM.Owner.GetComponent<Animator>().Play(m_animation);
        m_StateDelta = 0;
        rigidBody2d.velocity = es.transform.right * 10f;
    }

    public override void Update()
    {
        base.Update();
        m_StateDelta += Time.deltaTime;
        if (m_StateDelta >= m_StateLength)
        {
            m_FSM.ChangeState<IdleSword>();
        }
    }

    
}
