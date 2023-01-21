using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour 
{
    [SerializeField] Transform _playerTr;
    [SerializeField] Rigidbody _playerRb;
    [SerializeField] PlayerMovement _playerSys;

    private float _nowXspeed;

    private void Start()
    {
        
    }

    private void Update()
    {
        _nowXspeed = Mathf.Abs(Input.GetAxis("Horizontal"));

        if(_nowXspeed == 0)
        {

        } 
        else
        {

        }
    }
}