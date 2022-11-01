using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField]
    Cositas cositas;

    private void Awake()
    {
        cositas.llave1 = false;
        cositas.llave2 = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Llave1")
        {
            cositas.llave1 = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Llave2")
        {
            cositas.llave2 = true;
            Destroy(collision.gameObject);
        }
    }
}
