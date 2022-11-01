using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class EnemySpada : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject m_gameObject;

    private FSM fsm;

    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    LayerMask LayerMaskM;
    private bool derecha = true;
    public bool DerechaGet {

        get { return derecha; }

    }
    public bool DerechaSet { 
    
        set { derecha = value; }
    
    }
    private float vel = 2f;

    public float velocity { 
    
        get { return vel; }
    
    }

    private SpriteRenderer sr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        fsm = new FSM(gameObject);
        fsm.AddState(new Guarding(fsm, "GuardingShoot", layerMask, LayerMaskM));
        fsm.AddState(new Shoot(fsm, "ShootShoot", 0.6f));
        fsm.ChangeState<Guarding>();

       
      
      
        
    }


    
   void Update()
    {
        fsm.Update();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {

            Destroy(this.gameObject);


        }
    }
    // Update is called once per frame

    public void disparar() {


        GameObject shoot = Instantiate(m_gameObject);
        shoot.transform.position = transform.position;  
        shoot.GetComponent<Rigidbody2D>().velocity = transform.right * 5f;

    
    }

  

}
