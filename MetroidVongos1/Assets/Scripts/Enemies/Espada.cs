using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Espada : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;

    GameObject m_gameObject;

    int layerEnemigo;

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && collision.gameObject.layer == LayerMask.NameToLayer("Player")) { 
            m_gameObject = collision.gameObject;
            Destroy(collision.gameObject);
            this.GetComponentInParent<EnemySword>().deflect();
            deflectar();
        }
    }
    public void deflectar()
    {
        layerEnemigo = LayerMask.NameToLayer("Enemy");
        m_gameObject.layer = layerEnemigo;
        GameObject deflect = Instantiate(m_gameObject);
        deflect.transform.position = transform.position;
        deflect.GetComponent<Rigidbody2D>().velocity = transform.right * 13f;
        deflect.transform.rotation = transform.rotation;
        deflect.GetComponent<BoxCollider2D>().enabled = true;
        deflect.GetComponent<Animator>().enabled = true;
        deflect.GetComponent<DestroyBulletinTime>().enabled = true;
    }
}
