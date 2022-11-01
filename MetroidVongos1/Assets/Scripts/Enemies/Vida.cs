using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    Camera Cam;
    Canvas m_Canvas;
    private void Start()
    {
        //Srry Hector ns com hacerlo sin find
        Cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_Canvas = gameObject.GetComponentInParent<Canvas>();
    }
    private void FixedUpdate()
    {
        Vector3 pos = gameObject.GetComponentInParent<Transform>().parent.parent.position;
        transform.position = new Vector3(pos.x, pos.y, pos.z);
        RectTransform CanvasRect = m_Canvas.GetComponent<RectTransform>();
        Vector2 ViewportPosition = Cam.WorldToViewportPoint(new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z));
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));
        GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
    }
    public void ReciveDamage(float Damage)
    {
        gameObject.GetComponent<Slider>().value -= Damage;
        if (gameObject.GetComponent<Slider>().value <= 0)
        {
            Destroy(gameObject);
            Destroy(gameObject.GetComponentInParent<Transform>().parent.parent.gameObject);
        }
    }
}
