using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;
using UnityEngine.InputSystem;

public class CrounchState : State
{
    private PlayerControls CharacterControls;
    public CrounchState(FSM fSM, PlayerControls CharacterControls) : base(fSM)
    {
        this.CharacterControls = CharacterControls;
    }
    public override void Init()
    {
        m_FSM.Owner.GetComponent<Animator>().Play("Crouch");
        m_FSM.Owner.GetComponent<movimientoBasico>().StartCoroutine(WaitToCrouch());
        CharacterControls.Player.Enable();
    }
    public override void Update()
    {
        base.Update();
        if(!CharacterControls.Player.A4.IsPressed())
            m_FSM.ChangeState<IddleState>();
    }
    private IEnumerator WaitToCrouch()
    {
        yield return new WaitForSeconds(0.1f);
        m_FSM.Owner.GetComponent<Animator>().Play("WhileCrouch");
    }
    public override void Exit()
    {
        m_FSM.Owner.GetComponent<movimientoBasico>().m_addVelocity = 1;
        m_FSM.Owner.GetComponent<movimientoBasico>().StopAllCoroutines();
    }
}
