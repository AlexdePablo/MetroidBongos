using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DamageInSlider : MonoBehaviour
{
    public delegate void PlayerHit();
    public event PlayerHit PressF;
    public void TakeDamage(float damage)
    {
        GetComponent<Slider>().value -= damage;
        if (GetComponent<Slider>().value <= 0)
            PressF.Invoke();
    }
}
