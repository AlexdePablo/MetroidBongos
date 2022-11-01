using FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class movimientoBasico : MonoBehaviour
{
    [SerializeField]
    private float m_MoveSpeed = 10f;
    [SerializeField]
    private float m_JumpForce = 10f;
    [SerializeField]
    public LayerMask LayermaskMGround;
    public float m_addVelocity;
    private bool m_onAir = true;
    private Rigidbody2D m_rigidBody;
    public bool m_ajupit = false;
    private Vector3 vectorSalto;
    private int salto = 2;
    public PlayerControls m_PlayerControls;
    public FSM m_FSM;
    public Boolean Atck;
    [SerializeField]
    private GameObject NormalBullet;
    [SerializeField]
    private GameObject BlackHole;
    [SerializeField]
    public GameEventDamage m_Damage;
    [SerializeField]
    private DamageInSlider Dead;
    public Boolean BlacHoleShoot;
    void Awake()
    {
        Dead.PressF += Morir;
        m_PlayerControls = new PlayerControls();
        m_PlayerControls.Player.Move.canceled += StopMovement;
        m_PlayerControls.Player.Jump.started += Jump;
        m_PlayerControls.Player.A4.performed += Agachadito;
        m_PlayerControls.Player.A4.canceled += YaNoAgachadito;
        m_PlayerControls.Player.AtackShoot.started += Atack;
        m_PlayerControls.Player.AtackMele.started += Atack;
        m_PlayerControls.Player.Enable();
        m_addVelocity = 1;
        vectorSalto = new Vector3(0, m_JumpForce, 0);
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_FSM = new FSM(gameObject);
        m_FSM.AddState(new OndaAtack(m_FSM, "Onda", 0.35f));
        m_FSM.AddState(new Damage(m_FSM, "Damage"));
        m_FSM.AddState(new IddleState(m_FSM, "Idle"));
        m_FSM.AddState(new AtackMele(m_FSM, "Hit", "HitAir", 0.6f));
        m_FSM.AddState(new AtackShoot(m_FSM, "GroundShoot", "AirShoot", 0.6f));
        m_FSM.AddState(new InAir(m_FSM, "WhileAir"));
        m_FSM.AddState(new CrounchState(m_FSM, m_PlayerControls));
        m_FSM.ChangeState<IddleState>();

    }
    private void Atack(InputAction.CallbackContext obj)
    {
        if(!m_ajupit)
        {
            Atck = true;
        }
    }
        private void YaNoAgachadito(InputAction.CallbackContext obj)
    {
        m_ajupit=false; 
    }

    private void Agachadito(InputAction.CallbackContext context)
    {
        if (!m_onAir){
            m_rigidBody.velocity = new Vector2(0, m_rigidBody.velocity.y);
            m_ajupit = true;
        }
        else {
            m_ajupit = true;
        }
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 0.5f, LayermaskMGround);
        if (hit.collider != null)
        {
            m_onAir = false;
            salto = 1;
        } else
        {
            m_onAir = true;
        }
        if ((!m_ajupit && !Atck) || (!Atck && m_onAir))
        {
        Vector2 movement = m_PlayerControls.Player.Move.ReadValue<Vector2>();
        if (movement.x > 0)
            {
                m_rigidBody.velocity = new Vector2(m_MoveSpeed*m_addVelocity, m_rigidBody.velocity.y);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (movement.x < 0)
            {
                m_rigidBody.velocity = new Vector2(-m_MoveSpeed*m_addVelocity, m_rigidBody.velocity.y);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        if (m_ajupit)
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, m_rigidBody.velocity.y - 0.5f);
        }
    }

     private void StopMovement(InputAction.CallbackContext context)
     {
        if (context.phase == InputActionPhase.Canceled)
        {
            m_rigidBody.velocity = new Vector2(0, m_rigidBody.velocity.y);
        }
     }

    private void Jump(InputAction.CallbackContext context)
    {
        if (salto>0 && !m_ajupit && !Atck)
        {
            m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, 0);
            m_rigidBody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
            salto--;
        }
    }

    void Update()
    {
        m_FSM.Update();
    }
    public void disparar()
    {
        GameObject dispar;
        if (BlacHoleShoot)
        {
            dispar = Instantiate(BlackHole);
            BlacHoleShoot = false;
        }
        else
        {
            dispar = Instantiate(NormalBullet);
            dispar.layer = LayerMask.NameToLayer("Player");
        }
        dispar.transform.position = new Vector3(transform.position.x + transform.right.x * 0.5f, transform.position.y + transform.up.y * 0.2f, transform.position.z);
        dispar.GetComponent<Rigidbody2D>().velocity = transform.right * 5f;
        dispar.transform.rotation = transform.rotation;
    }

    private void Morir()
    {
        m_PlayerControls.Player.Disable();
        SceneManager.LoadScene("GameOver");
    }
}
