using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {
    PhotonView m_PhotonView;
    GameObject plv;
    // Use this for initialization
    void Start()
    {
        m_PhotonView = GetComponent<PhotonView>();
        plv = GetComponent<Transform>().gameObject;
    }

    void Update()
    {
        if (m_PhotonView.isMine)
        {
            if (Input.GetKey(KeyCode.A))
            {
                plv.transform.position += new Vector3(0.1f, 0, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                plv.transform.position += new Vector3(-0.1f, 0, 0);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                plv.transform.position += new Vector3(0, 0, 0.1f);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                plv.transform.position += new Vector3(0, 0, -0.1f);
            }
        }
    }
}
