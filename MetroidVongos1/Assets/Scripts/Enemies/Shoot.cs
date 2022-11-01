using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Shoot : Attacks {

 
    Rigidbody2D rigidBody2d;

    public Shoot(FSM fSM, string animation, float StateLenght) : base(fSM, animation, StateLenght)
    {
      
        rigidBody2d = m_FSM.Owner.GetComponent<Rigidbody2D>();
    }

    public override void Init()
    {
        base.Init();
        rigidBody2d.velocity = Vector2.zero;
    }


    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}