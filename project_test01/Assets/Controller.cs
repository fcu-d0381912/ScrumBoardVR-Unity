using UnityEngine;
using System.Collections;
using Valve.VR;
public class Controller : MonoBehaviour {
    SteamVR_TrackedObject trackObj;
    //public GameObject setpanel;
    bool cath;
    GameObject target;
    public GameObject hand;
    FixedJoint point;

    public mananger boolmanager;
    // Use this for initialization
    void Start () {
        trackObj = GetComponent<SteamVR_TrackedObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 moveangle = device.GetAxis();
            var top = SteamVR_Render.Top().transform;
            top.Translate(moveangle.x,0, moveangle.y);
        }
        //if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        //{
         //   if(setpanel.activeSelf)
          //  {
        //        setpanel.SetActive(false);
       //         boolmanager.havetext = false;
         //       boolmanager.first = false;
        //    }
         //   else
        //    {
       //         setpanel.SetActive(true);
       //     }
            
       // }
        if(cath == true)
        {
            if(point==null&& device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
             {
                target.transform.position = this.transform.position;
                point=target.AddComponent<FixedJoint>();
                this.GetComponent<Rigidbody>();
                point.connectedBody=this.GetComponent<Rigidbody>();
            }
        }
        
        else if(point != null&&device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Object.DestroyImmediate(point);
            point = null;
            cath = false;
        }
    }


    void OnStriggerEnter(Collider c)
    {
        cath = true;
        target = c.gameObject;
    }
}
