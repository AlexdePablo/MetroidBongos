using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IddleState : State
{
    private string m_Animation;
    private PlayerControls CharacterControls;
    public IddleState(FSM fSM, string animation) : base(fSM)
    {
        m_Animation = animation;
    }
    public override void Init()
    {
        m_FSM.Owner.GetComponent<Animator>().Play(m_Animation);
        CharacterControls = new PlayerControls();
        CharacterControls.Player.Jump.started += Jump;
        CharacterControls.Player.AtackMele.started += AtackMele;
        CharacterControls.Player.AtackShoot.started += AtackShoot;
        CharacterControls.Player.Move.started += StartMovement;
        CharacterControls.Player.Move.canceled += StopMovement;
        CharacterControls.Player.A4.started += Agachadito;
        CharacterControls.Player.Enable();
        if (m_FSM.Owner.GetComponent<movimientoBasico>().m_ajupit == true)
            m_FSM.ChangeState<CrounchState>();
    }

    public override void Update()
    {
        base.Update();
    }
    private void Jump(InputAction.CallbackContext context)
    {
        m_FSM.ChangeState<InAir>();
    }
    private void AtackMele(InputAction.CallbackContext context)
    {
        m_FSM.ChangeState<AtackMele>();
    }
    private void AtackShoot(InputAction.CallbackContext context)
    {
        m_FSM.ChangeState<AtackShoot>();
    }
    private void Agachadito(InputAction.CallbackContext context)
    {
        m_FSM.Owner.GetComponent<movimientoBasico>().m_addVelocity = 1;
        m_FSM.ChangeState<CrounchState>();
    }
    private void StartMovement(InputAction.CallbackContext context)
    {
        m_FSM.Owner.GetComponent<Animator>().Play("Run");
        m_FSM.Owner.GetComponent<movimientoBasico>().StartCoroutine(WaitToRun2());
    }
    private void StopMovement(InputAction.CallbackContext context)
    {
        m_FSM.Owner.GetComponent<movimientoBasico>().m_addVelocity = 1;
        m_FSM.Owner.GetComponent<movimientoBasico>().StopAllCoroutines();
        m_FSM.Owner.GetComponent<Animator>().Play(m_Animation);
    }
    public override void Exit ()
    {
        CharacterControls.Player.Jump.started -= Jump;
        CharacterControls.Player.AtackMele.started -= AtackMele;
        CharacterControls.Player.AtackShoot.started -= AtackShoot;
        CharacterControls.Player.Move.started -= StartMovement;
        CharacterControls.Player.Move.canceled -= StopMovement;
        CharacterControls.Player.A4.started -= Agachadito;
        m_FSM.Owner.GetComponent<movimientoBasico>().StopAllCoroutines();
        CharacterControls.Player.Disable();
    }
    private IEnumerator WaitToRun2()
    {
        yield return new WaitForSeconds(1);
        m_FSM.Owner.GetComponent<Animator>().Play("Run2");
        m_FSM.Owner.GetComponent<movimientoBasico>().m_addVelocity = 2;
    }
}
