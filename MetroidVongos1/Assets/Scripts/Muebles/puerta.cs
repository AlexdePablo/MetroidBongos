using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class puerta : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite[] puertas;
    [SerializeField]
    Cositas cositas;

    private void Awake()
    {
     
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (cositas.llave1 && collision.gameObject.tag=="Player") {

            StartCoroutine("ChangeSceneDos");
        }
        if (cositas.llave2 && collision.gameObject.tag == "Player")
        {

            StartCoroutine("ChangeSceneFinal");
        }

    }

    public IEnumerator ChangeSceneDos()
    {
        spriteRenderer.sprite = puertas[0];
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Mapa2");
    }
    public IEnumerator ChangeSceneFinal()
    {
        spriteRenderer.sprite = puertas[0];
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EpicWin");
    }
}
