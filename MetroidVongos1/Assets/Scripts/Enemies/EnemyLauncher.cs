using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLauncher : MonoBehaviour
{
    private Rigidbody2D rb;

  

    [SerializeField]
    private GameObject m_gameObject;

    [SerializeField]
    private Transform m_target;
   

    private FSM fsm;

    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    LayerMask LayerMaskM;
    private bool derecha = true;
    public bool DerechaGet
    {

        get { return derecha; }

    }
    public bool DerechaSet
    {

        set { derecha = value; }

    }
    private float vel = 3f;

    public float velocity
    {

        get { return vel; }

    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
   

        fsm = new FSM(gameObject);
        fsm.AddState(new GuardingLauncher(fsm, "GuardingLauncher", layerMask, LayerMaskM));
        fsm.AddState(new ShootLauncher(fsm, "LaunchShoot", 1f));
        fsm.ChangeState<GuardingLauncher>();





    }



    void Update()
    {
        fsm.Update();
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3) { 
        
            Destroy(this.gameObject);
        
        
        }
    }

    // Update is called once per frame
    public void disparar()
    {


        GameObject shoot = Instantiate(m_gameObject);
        if (derecha)
        {
            shoot.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else {

            shoot.transform.localRotation = Quaternion.Euler(0, 180, 0);

        }
        
        MissileScript m = shoot.GetComponent<MissileScript>();
        if (m) 
            m.Objetivo = m_target;

        shoot.transform.position = transform.position;

    }

    
}
