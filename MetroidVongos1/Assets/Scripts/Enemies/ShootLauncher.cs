using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLauncher : State
{

    string m_animation;
    float m_StateDelta;
    float m_StateLength;
    Rigidbody2D rigidBody2d;
    public ShootLauncher(FSM fSM, string animation, float StateLenght) : base(fSM)
    {
       m_animation = animation;
       m_StateLength = StateLenght;
        rigidBody2d = m_FSM.Owner.GetComponent<Rigidbody2D>();
    }

    public override void Init()
    {
        m_FSM.Owner.GetComponent<Animator>().Play(m_animation);
        m_StateDelta = 0;
        rigidBody2d.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();
        m_StateDelta += Time.deltaTime;
        if (m_StateDelta >= m_StateLength)
        {
            m_FSM.ChangeState<GuardingLauncher>();
        }
    }
}
