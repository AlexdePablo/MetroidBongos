using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSmooth : MonoBehaviour
{
  [SerializeField]
    private GameObject m_Target;

    void Update()
    {
        transform.position= new Vector3(m_Target.transform.position.x+2,m_Target.transform.position.y,this.transform.position.z);
       
    }
}

