using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
  public void Jugar()
    {
        SceneManager.LoadScene("Mapa");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Inicio");
    }
}
