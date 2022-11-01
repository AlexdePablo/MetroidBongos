using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletDamageScript : MonoBehaviour
{
    [SerializeField]
    GameEventDamage DamagePlayer;
    [SerializeField]
    GameEventDamage DamageEnemy;
    [SerializeField]
    RectTransform m_vidaEnemy;
    [SerializeField]
    float Damage_Enemy;
    [SerializeField]
    float Damage_Player;
    Camera Cam;
    private void Awake()
    {
        Cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            return;
        }
        if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                DamagePlayer.Raise(Damage_Enemy);
                Destroy(gameObject);
            }
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (collision.gameObject.GetComponentInParent<Transform>().GetChild(0).GetComponent<Transform>().childCount == 0)
                {
                    RectTransform slider = Instantiate(m_vidaEnemy);
                    slider.transform.parent = collision.GetComponentInChildren<Transform>().transform;
                    Canvas m_Canvas = collision.gameObject.GetComponentInChildren<Canvas>();
                    slider.transform.parent = m_Canvas.transform;
                    RectTransform CanvasRect = m_Canvas.GetComponent<RectTransform>();
                    Vector2 ViewportPosition = Cam.WorldToViewportPoint(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z));
                    Vector2 WorldObject_ScreenPosition = new Vector2(
                    ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
                    ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));
                    slider.anchoredPosition = WorldObject_ScreenPosition;
                }
                DamageEnemy.Raise(Damage_Player);
                Destroy(gameObject);
            }
    }
}
