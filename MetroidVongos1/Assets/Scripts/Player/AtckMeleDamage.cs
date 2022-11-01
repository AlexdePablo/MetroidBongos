using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AtckMeleDamage : MonoBehaviour
{
    [SerializeField]
    RectTransform m_vidaEnemy;
    [SerializeField]
    GameEventDamage EnemyDamage;
    [SerializeField]
    Camera Cam;
    [SerializeField]
    float Damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (collision.gameObject.GetComponentInParent<Transform>().GetChild(0).GetComponent<Transform>().childCount == 0)
            {
                RectTransform slider = Instantiate(m_vidaEnemy);
                slider.transform.SetParent(collision.gameObject.GetComponentInChildren<Transform>().transform);
                Canvas m_Canvas = collision.gameObject.GetComponentInChildren<Canvas>();
                slider.transform.SetParent(m_Canvas.transform);
                RectTransform CanvasRect = m_Canvas.GetComponent<RectTransform>();
                Vector2 ViewportPosition = Cam.WorldToViewportPoint(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z));
                Vector2 WorldObject_ScreenPosition = new Vector2(
                ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
                ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));
                slider.anchoredPosition = WorldObject_ScreenPosition;
            }
            EnemyDamage.Raise(Damage);
        }
    }
}
