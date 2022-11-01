using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBulletinTime : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Destoryer());
    }
    private IEnumerator Destoryer()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
