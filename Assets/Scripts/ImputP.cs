using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ImputP : MonoBehaviour
{
    public float XAxis1;
    public float XAxis2;
    public float rawaxis;
    private void Update()
    {
        rawaxis = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.D))
        {
            if(XAxis1 < 0)
            {
                XAxis1 = 0;
            }
            XAxis1 += Time.deltaTime * 3f;
        } 
        else if(Input.GetKey(KeyCode.A)) 
        {
            if (XAxis1 > 0)
            {
                XAxis1 = 0;
            }
            XAxis1 -= Time.deltaTime * 3f;
        }
        else
        {
            XAxis1 = Mathf.MoveTowards(XAxis1, 0, Time.deltaTime * 3f);
        } 

        if(XAxis1 > 1)
        {
            XAxis1 = 1;
        } else if 
            (XAxis1 < -1)
        {
            XAxis1 = -1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (XAxis2 < 0)
            {
                XAxis2 = 0;
            }
            XAxis2 += Time.deltaTime * 3f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (XAxis2 > 0)
            {
                XAxis2 = 0;
            }
            XAxis2 -= Time.deltaTime * 3f;
        }
        else
        {
            XAxis2 = Mathf.MoveTowards(XAxis2, 0, Time.deltaTime * 3f);
        }

        if (XAxis2 > 1)
        {
            XAxis2 = 1;
        }
        else if
            (XAxis2 < -1)
        {
            XAxis2 = -1;
        }
    }
}
