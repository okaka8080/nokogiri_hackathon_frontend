using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //シリアライズフィールド
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int PNum;

    //パブリック変数
    public string nowState;
    public float CanMoveOffset;
    public bool IsGuard = false;
    public int direction;
    public bool CanMove;

    //プライベート変数
    private Rigidbody _rb;
    private GroundChecker _groundChecker;
    private Animator _animator;
    private int _jumpCount;
    private bool _isGround;
    private float _inputdata;
    private ImputP _input;
    private bool _isDamaged;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _groundChecker = transform.Find("GroundChecker").GetComponent<GroundChecker>();
        _animator = GetComponent<Animator>();
        CanMoveOffset = 1;
        nowState = "Stand";
        _jumpCount = 0;
        if(transform.eulerAngles.y == 90)
        {
            direction = 1;
        } else if(transform.eulerAngles.y == 270)
        {
            direction = -1;
        }


        _input = GetComponent<ImputP>();

        if(gameObject.tag == "P1")
        {
            PNum = 1;
        } else if(gameObject.tag == "P2")
        {
            PNum = 2;
        }
    }

    void FixedUpdate()
    {
        if (CanMove)
        {
            if (_rb != null)
            {
                if (PNum == 1)
                {
                    Movement1();
                }
                else if (PNum == 2)
                {
                    Movement2();
                }
            }
        }

    }
    void Update()
    {
        if (CanMove)
        {
            if (_groundChecker != null)
            {
                _isGround = _groundChecker.IsGround();
                if (_isGround)
                {
                    _jumpCount = 0;
                }
            }
            if (_rb != null)
            {
                if (PNum == 1)
                {
                    _inputdata = _input.XAxis1;
                    Jump1();
                }
                else if (PNum == 2)
                {
                    _inputdata = _input.XAxis2;
                    Jump2();
                }

            }
        }

        _isDamaged = _animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged");
        
        
        
    }

    private void Movement1()
    {
        float offset;
        if (_isGround)
        {
            offset = 0.0f;
        } 
        else
        {
            offset = 2.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (_isGround)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                direction = 1;
            }
            _rb.AddForce((new Vector3(1,0,0)*((5 - offset - _rb.velocity.x)* _runSpeed) * Mathf.Abs(_inputdata))* CanMoveOffset);
        } 
        else if (Input.GetKey(KeyCode.A)) 
        {
            if (_isGround)
            {
                transform.eulerAngles = new Vector3(0, -90, 0);
                direction = -1;
            }
            _rb.AddForce((new Vector3(-1, 0, 0)* ((5 - offset + _rb.velocity.x) * _runSpeed) * Mathf.Abs(_inputdata))* CanMoveOffset);
        } 
        else
        {

            if (!_isDamaged)
            {
                //_rb.velocity = new Vector3(_inputdata * CanMoveOffset, _rb.velocity.y, _rb.velocity.z);
            }
        }
    }

    private void Movement2()
    {
        float offset;
        if (_isGround)
        {
            offset = 0.0f;
        }
        else
        {
            offset = 2.0f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (_isGround)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                direction = 1;
            }
            _rb.AddForce((new Vector3(1, 0, 0) * ((5 - offset - _rb.velocity.x) * _runSpeed) * Mathf.Abs(_inputdata)) * CanMoveOffset);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (_isGround)
            {
                transform.eulerAngles = new Vector3(0, -90, 0);
                direction = -1;
            }
            _rb.AddForce((new Vector3(-1, 0, 0) * ((5 - offset + _rb.velocity.x) * _runSpeed) * Mathf.Abs(_inputdata)) * CanMoveOffset);
        }
        else
        {
            if (!_isDamaged)
            {
                //_rb.velocity = new Vector3(_inputdata * CanMoveOffset, _rb.velocity.y, _rb.velocity.z);
            }
            
        }
    }

    private void Jump1()
    {

        if (_jumpCount < 1 && CanMoveOffset != 0.0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (_isDamaged)
                {
                    _rb.velocity = Vector3.zero;
                }
                _rb.AddForce(new Vector3(0, 1, 0) * _jumpForce, ForceMode.Impulse);
                _jumpCount++;
            }
        } 
    }

    private void Jump2()
    {

        if (_jumpCount < 1 && CanMoveOffset != 0.0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_isDamaged)
                {
                    _rb.velocity = Vector3.zero;
                }
                
                _rb.AddForce(new Vector3(0, 1, 0) * _jumpForce, ForceMode.Impulse);
                _jumpCount++;
            }
        }
    }

    public void MoveManager(bool value)
    {
        CanMove = value;
    }


}
