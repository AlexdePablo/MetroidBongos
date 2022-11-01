using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MissileScript : MonoBehaviour
{
    [SerializeField]
    private Transform t;
    public Transform Objetivo
    {
        set => t = value;
    }

    private Rigidbody2D rb;

    public float vel = 9f;

    public float rSpeed = 100f;

    public float time = 2f;
    public float MaxFollowTime = 3f;
    private float elapsedTime = 0;
    private float angleChangingSpeed = 90f;
    private float tiempo = 3f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
    }


     void FixedUpdate() {
        if (tiempo > 0)
        {

            tiempo -= Time.deltaTime;

        }
        else {

            Destroy(this.gameObject);
        }
       
        Vector2 direction = (Vector2)t.position - (Vector2)transform.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.right).z;
        rb.angularVelocity = -angleChangingSpeed * rotateAmount;





        rb.velocity = direction * vel;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) {

            Destroy(this.gameObject);
        
        }
    }

}
