using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private GameObject bala;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(disparar());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator disparar() {
        while (true) {

            GameObject bullet = Instantiate(bala);

            bullet.transform.position = transform.position;

            bullet.GetComponent<Rigidbody2D>().velocity = -transform.right * 5f;

            yield return new WaitForSeconds(5f);
        
        }
    
    
    }
}
