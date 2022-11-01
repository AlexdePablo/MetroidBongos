using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class InAir : State
{
    private string m_Animation;
    private PlayerControls CharacterControls;
    public InAir(FSM fSM, string animation) : base(fSM)
    {
        m_Animation = animation;
    }
    public override void Init()
    {
        m_FSM.Owner.GetComponent<Animator>().Play("WhileAir");
        CharacterControls = new PlayerControls();
        CharacterControls.Player.AtackMele.started += AtackMele;
        CharacterControls.Player.AtackShoot.started += AtackShoot;
        CharacterControls.Player.Enable();
    }
    public override void Update()
    {
        base.Update();
        RaycastHit2D hit = Physics2D.Raycast(m_FSM.Owner.transform.position, -m_FSM.Owner.transform.up, 0.5f, m_FSM.Owner.GetComponent<movimientoBasico>().LayermaskMGround);
        if (hit.collider != null && m_FSM.Owner.GetComponent<Rigidbody2D>().velocity.y < 0)
            m_FSM.ChangeState<IddleState>();
    }
    private void AtackMele(InputAction.CallbackContext context)
    {
        m_FSM.ChangeState<AtackMele>();
    }
    private void AtackShoot(InputAction.CallbackContext context)
    {
        m_FSM.ChangeState<AtackShoot>();
    }
    public override void Exit()
    {
        CharacterControls.Player.AtackMele.started -= AtackMele;
        CharacterControls.Player.AtackShoot.started -= AtackShoot;
        CharacterControls.Player.Disable();
    }
}
