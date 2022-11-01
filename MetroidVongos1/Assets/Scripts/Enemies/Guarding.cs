using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Guarding : State{

    private string m_Animation;
    private LayerMask LayermaskM;
    private LayerMask LayerMask;
    EnemySpada enemySpada;
    Rigidbody2D rb;
    public Guarding(FSM fSM, string animation, LayerMask layerMask, LayerMask layermask) : base(fSM)
    {
        m_Animation = animation;
        LayermaskM = layerMask;
        LayerMask = layermask;
    }

    public override void Init()
    {
        m_FSM.Owner.GetComponent<Animator>().Play(m_Animation);
        enemySpada = m_FSM.Owner.GetComponent<EnemySpada>();
        rb = m_FSM.Owner.GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        base.Update();
        RaycastHit2D ghit = Physics2D.Raycast(enemySpada.transform.position, -enemySpada.transform.up, 0.5f, LayerMask);
        if (ghit.collider == null)
            return;
        if (enemySpada.DerechaGet)
        {
            rb.velocity = enemySpada.transform.right * enemySpada.velocity;
        }
        else
        {
            rb.velocity = -enemySpada.transform.right * -enemySpada.velocity;
        }
        RaycastHit2D hit = Physics2D.Raycast(enemySpada.transform.position, enemySpada.transform.right - enemySpada.transform.up, 1f, LayerMask);
        if (hit.collider == null && enemySpada.DerechaGet == true)
        {
            enemySpada.transform.rotation = Quaternion.Euler(0, 180, 0);
            enemySpada.DerechaSet = false;
        }
        else if (hit.collider == null && enemySpada.DerechaGet == false)
        {
            enemySpada.transform.rotation = Quaternion.Euler(0, 0, 0);
            enemySpada.DerechaSet = true;
        }
        RaycastHit2D Phit = Physics2D.Raycast(m_FSM.Owner.transform.position, m_FSM.Owner.transform.right, 3f, LayermaskM);
        if (Phit.collider != null) {
            m_FSM.ChangeState<Shoot>();
        }
    }
}
