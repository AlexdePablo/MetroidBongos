using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    private Rigidbody2D rb;






    private FSM fsm;

    [SerializeField]
    LayerMask layerMask;
 
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
        fsm.AddState(new IdleSword(fsm, "IdleSword", layerMask));
        fsm.AddState(new SwordAttack(fsm, "Attack", 1f));
        fsm.AddState(new Deflect(fsm, "Deflect", 1f));
        fsm.ChangeState<IdleSword>();





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


    public void deflect() {


        fsm.ChangeState<Deflect>();
    
    
    }
}
