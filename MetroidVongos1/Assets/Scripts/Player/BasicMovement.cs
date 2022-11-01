using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMovement : MonoBehaviour
{

Rigidbody2D rb;
    [SerializeField] GameObject blackHole;




    [SerializeField]
    private float m_MoveSpeed = 10f;
    [SerializeField]
    private float m_JumpForce = 10f;
    private bool m_onAir = true;
    private Rigidbody2D m_rigidBody;
    private bool m_ajupit = false;
    private Vector3 vectorSalto;
    private int salto = 2;
    PlayerControls m_PlayerControls;

    void Awake()
    {
        vectorSalto = new Vector3(0, m_JumpForce, 0);
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_PlayerControls = new PlayerControls();        
        m_PlayerControls.Player.Move.canceled += StopMovement;
        m_PlayerControls.Player.Jump.started += Jump;
        m_PlayerControls.Player.A4.performed += Agachadito;
        m_PlayerControls.Player.A4.canceled += YaNoAgachadito;
        m_PlayerControls.Player.Enable();

    }

    private void YaNoAgachadito(InputAction.CallbackContext obj)
    {
        m_ajupit=false; 
    }

    private void Agachadito(InputAction.CallbackContext context)
    {
        if(!m_onAir){
          m_rigidBody.velocity = new Vector2(0, m_rigidBody.velocity.y);
        m_ajupit=true;
        }
        else{
            m_ajupit=true;
        }
    }

    void FixedUpdate()
    {  print(salto);
         RaycastHit2D hit = Physics2D.Raycast( transform.position, -transform.up, GetComponent<CircleCollider2D>().radius*1.3f,m_LayerMask);
           Debug.DrawLine(transform.position, hit.point, new Color(1, 0, 1));
   
        
          if (hit.collider != null)
        {
            salto=1;
            }
       
        if(!m_ajupit){
        Vector2 movement = m_PlayerControls.Player.Move.ReadValue<Vector2>();
        if (movement.x > 0)
            m_rigidBody.velocity = new Vector2(m_MoveSpeed, m_rigidBody.velocity.y);
        else if (movement.x < 0)
            m_rigidBody.velocity = new Vector2(-m_MoveSpeed, m_rigidBody.velocity.y);
    }
    }

 private void StopMovement(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Canceled)
        {
            m_rigidBody.velocity = new Vector2(0, m_rigidBody.velocity.y);
        }
    }


    [SerializeField]
    LayerMask m_LayerMask;


    private void Jump(InputAction.CallbackContext context)
    {
        if (salto>0)
        {
            m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x,0 );
            m_rigidBody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
            salto--;
          
        }
    }

    private float calculFuerza(float tiempo)
    {
        float fuersa=(float) (m_rigidBody.position.y+2.5);
        fuersa=fuersa-m_rigidBody.position.y;
        fuersa=fuersa-(m_rigidBody.velocity.y*tiempo);
        fuersa=fuersa*2;
        if(fuersa<0){
            fuersa=fuersa*-1;
            if(salto==1){
            fuersa=fuersa/2;
            }
            return fuersa;
        }
      else{
         if(salto==1){
            fuersa=fuersa/2;
            }
        return fuersa;
      }
        
    }

    private float calculTemps( )
    {
        float temps=2*m_rigidBody.position.y;
        temps=temps+5;
        if(m_rigidBody.velocity.y!=0){
        temps=temps/m_rigidBody.velocity.y;
        
        return temps;
        }
        else{
            print(temps);
        return temps;}
    }

    void Update()
    {
      
       if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine("disparar");
		}
    }

    IEnumerator disparar()
    {
        	GameObject dispar = Instantiate(blackHole);
			dispar.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
			dispar.GetComponent<Rigidbody2D>().velocity = transform.right * 5f;
            yield return new WaitForSeconds(6.3f);
            Destroy(dispar.gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Suelo"){
            m_onAir=false;
            }

        if(m_ajupit)
                    m_rigidBody.velocity = new Vector2(0, m_rigidBody.velocity.y);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Suelo")
            m_onAir = true;
    }
}
