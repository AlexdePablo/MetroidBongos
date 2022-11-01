using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardingLauncher : State
{
    private string m_Animation;
    private LayerMask LayermaskM;
    private LayerMask LayerMask;
    EnemyLauncher enemyLauncher;
    Rigidbody2D rb;
    public GuardingLauncher(FSM fSM, string animation, LayerMask layerMask, LayerMask layermask) : base(fSM)
    {

        m_Animation = animation;
        LayermaskM = layerMask;
        LayerMask = layermask;

    }

    public override void Init()
    {
        m_FSM.Owner.GetComponent<Animator>().Play(m_Animation);
        enemyLauncher = m_FSM.Owner.GetComponent<EnemyLauncher>();
        rb = m_FSM.Owner.GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        base.Update();
        if (enemyLauncher.DerechaGet)
        {
            rb.velocity = enemyLauncher.transform.right * enemyLauncher.velocity;
        }
        else
        {
            rb.velocity = -enemyLauncher.transform.right * -enemyLauncher.velocity;
        }



        RaycastHit2D hit = Physics2D.Raycast(enemyLauncher.transform.position, enemyLauncher.transform.right - enemyLauncher.transform.up, 1f, LayerMask);

        Debug.DrawLine(enemyLauncher.transform.position, hit.point, new Color(1, 0, 1));

        if (hit.collider == null && enemyLauncher.DerechaGet == true)
        {

            enemyLauncher.transform.rotation = Quaternion.Euler(0, 180, 0);
            enemyLauncher.DerechaSet = false;


        }
        else if (hit.collider == null && enemyLauncher.DerechaGet == false) {

            enemyLauncher.transform.rotation = Quaternion.Euler(0, 0, 0);
            enemyLauncher.DerechaSet = true;

        }

            


        RaycastHit2D Phit = Physics2D.CircleCast(m_FSM.Owner.transform.position, m_FSM.Owner.GetComponent<CircleCollider2D>().radius * 3f, m_FSM.Owner.transform.right, 5f, LayermaskM);
        Debug.DrawLine(m_FSM.Owner.transform.position, hit.point, new Color(1, 0, 1));
        if (Phit.collider != null)
        {
            m_FSM.ChangeState<ShootLauncher>();
        }


    }



}
