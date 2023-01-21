using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private bool _isGround;
    private bool _isGroundEnter, _isGroundStay, _isGroundExit;

    private void Start()
    {
        _isGround= false;
    }
    public bool IsGround()
    {
        if(_isGroundEnter || _isGroundStay)
        {
            _isGround = true;
        } 
        else if(_isGroundExit)
        {
            _isGround = false;
        }
        _isGroundEnter = false;
        _isGroundStay = false;
        _isGroundExit = false;

        return _isGround;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            _isGroundEnter = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Ground")
        {
            _isGroundStay = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ground")
        {
            _isGroundExit = true;
        }
    }
}
