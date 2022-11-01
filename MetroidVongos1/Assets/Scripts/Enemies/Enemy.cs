using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(collision.transform.position - transform.position * 3.5f, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<movimientoBasico>().m_FSM.ChangeState<Damage>();
        }
    }
}
