using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class IdleSword : State
{
    private string m_Animation;
    private LayerMask LayerMask;
    EnemySword EnemySword;
    Rigidbody2D rb;
    public IdleSword(FSM fSM, string animation, LayerMask layermask) : base(fSM)
    {

        m_Animation = animation;
        LayerMask = layermask;

    }

    public override void Init()
    {
        m_FSM.Owner.GetComponent<Animator>().Play(m_Animation);
        EnemySword = m_FSM.Owner.GetComponent<EnemySword>();
        rb = m_FSM.Owner.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();
        RaycastHit2D Phit = Physics2D.Raycast(m_FSM.Owner.transform.position, m_FSM.Owner.transform.right, 5f, LayerMask);
        Debug.DrawLine(m_FSM.Owner.transform.position, Phit.point, new Color(1, 0, 1));
        if (Phit.collider != null && Phit.collider.tag != "Bullet" && Phit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            m_FSM.ChangeState<SwordAttack>();
        }
    }
}
