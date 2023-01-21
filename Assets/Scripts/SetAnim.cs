using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SetAnim : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _animator;
    private GroundChecker _groundChecker;
    private PlayerMovement _playerMovement;
    private float _nowSpeed;
    private bool _isGround;
    private bool Rolling = false;
    private Vector3 preposition;
    private GameObject _shield;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerMovement = GetComponent<PlayerMovement>();   
        _animator = GetComponent<Animator>();
        _groundChecker = transform.Find("GroundChecker").GetComponent<GroundChecker>();
        _shield = transform.Find("Shield").gameObject;

    }

    private void Update()
    {

        _nowSpeed = Mathf.Abs(Input.GetAxis("Horizontal"));
        JudgeAnim();
        //var currentVelocity = (transform.position - preposition) /Time.deltaTime;
        //preposition = transform.position;
        //_nowSpeed = Mathf.Abs(currentVelocity.x);
        SetAnimFloat("Speed", _nowSpeed, 0f);
        if (_groundChecker!= null) 
        {
            _isGround = _groundChecker.IsGround();
            SetAnimBool("IsGround", _isGround);
        }
        if (Input.GetKeyDown(KeyCode.W) && _playerMovement.CanMoveOffset != 0)
        {
            //SetAnimBool("IsJump", true);
            if (_isGround)
            {
                SetAnimBool("IsJump", true);
            } 
            else
            {
                SetAnimTrigger("JumpButton");
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetAnimTrigger("AButton");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SetAnimTrigger("BButton");
        }

        if (Input.GetKey(KeyCode.G)) 
        {
            SetAnimBool("IsGuard", true);
        } 
        else
        {
            SetAnimBool("IsGuard", false);
        }
        if(_rb != null)
        {
            if (_rb.velocity.y < 0)
            {
                if (!_isGround)
                {
                    SetAnimBool("IsJump", false);
                }
                SetAnimBool("IsFalling", true);
            }
            else
            {
                SetAnimBool("IsFalling", false);
            }
        }
    }

    private void SetAnimFloat(string Target, float num, float dump)
    {
        if(_animator!= null)
        {
            _animator.SetFloat(Target, num, dump,Time.deltaTime);
        }
    }
    private void SetAnimBool(string Target, bool value) 
    {
        if(_animator!= null)
        {
            _animator.SetBool(Target, value);
        }
    }
    private void SetAnimTrigger(string Target)
    {
        _animator.SetTrigger(Target);
    }
    private void JudgeAnim()
    {
        
        if(_animator != null)
        {
            bool _isJump;
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") || _animator.GetCurrentAnimatorStateInfo(0).IsName("JumpUp") || _animator.GetCurrentAnimatorStateInfo(0).IsName("JumpFall") || _animator.GetCurrentAnimatorStateInfo(0).IsName("Landing") || _animator.GetCurrentAnimatorStateInfo(0).IsName("SecondJump"))
            {
                _isJump = true;
            }
            else
            {
                _isJump = false;
            }
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") || _isJump && !Input.GetKey(KeyCode.F) && !Input.GetKey(KeyCode.G) && !Input.GetKey(KeyCode.R))
            {
                
                _playerMovement.CanMoveOffset = 1;
            }
            else
            {
                _playerMovement.CanMoveOffset = 0;
            }

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
            {
                _shield.SetActive(true);
            } else
            {
                _shield.SetActive(false);
            }

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Rolling"))
            {
                transform.Translate(0.02f * _playerMovement.direction, 0, 0, Space.World);

                if (!Rolling)
                {
                    transform.Translate(0.1f,0, 0, Space.World);
                   
                }
                Rolling = true;
            }
            else
            {
                Rolling = false;
            }
            if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Rolling") || _animator.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
            {
                _playerMovement.IsGuard = true;
            }
            else
            {
                _playerMovement.IsGuard = false;
            }
        }
    }

}
