using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Progress;

public class SetAnim : MonoBehaviour
{

    private int PNum;
    private Rigidbody _rb;
    private Animator _animator;
    private GroundChecker _groundChecker;
    private PlayerMovement _playerMovement;
    private float _nowSpeed;
    private bool _isGround;
    private bool Rolling = false;
    private Vector3 preposition;
    private GameObject _shield;
    private float _inputdata;
    private ImputP _input;
    private bool _isDamaged;
    private bool _isGuard;

    public bool IsInvincible;
    public float opption = 1;
   
    
    

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerMovement = GetComponent<PlayerMovement>();   
        _animator = GetComponent<Animator>();
        _groundChecker = transform.Find("GroundChecker").GetComponent<GroundChecker>();
        _shield = transform.Find("Shield").gameObject;
        _input = GetComponent<ImputP>();
        if (gameObject.tag == "P1")
        {
            PNum = 1;
        }
        else if (gameObject.tag == "P2")
        {
            PNum = 2;
        }
    }

    private void Update()
    {
        
        _nowSpeed = Mathf.Abs(_inputdata);
        
        //var currentVelocity = (transform.position - preposition) /Time.deltaTime;
        //preposition = transform.position;
        //_nowSpeed = Mathf.Abs(currentVelocity.x);
        SetAnimFloat("Speed", _nowSpeed, 0f);
        if (_groundChecker!= null) 
        {
            _isGround = _groundChecker.IsGround();
            SetAnimBool("IsGround", _isGround);
        }
        if (PNum == 1)
        {
            _inputdata = _input.XAxis1;
            JudgeAnim1();
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
            if (Input.GetKey(KeyCode.R))
            {
                opption += Time.deltaTime;
                SetAnimBool("ChargeB", true);
            } else
            {
                SetAnimBool("ChargeB", false);
            }

            if (Input.GetKey(KeyCode.G))
            {
                SetAnimBool("IsGuard", true);
            }
            else
            {
                SetAnimBool("IsGuard", false);
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                SetAnimTrigger("BButton");
                
                
            }
        }
        else if (PNum == 2)
        {
            _inputdata = _input.XAxis2;
            JudgeAnim2();
            if (Input.GetKeyDown(KeyCode.UpArrow) && _playerMovement.CanMoveOffset != 0)
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
            if (Input.GetKeyDown(KeyCode.L))
            {
                SetAnimTrigger("AButton");
            }
            if (Input.GetKey(KeyCode.K))
            {
                opption += Time.deltaTime * 0.1f;
                SetAnimBool("ChargeB", true);
            }
            else
            {
                SetAnimBool("ChargeB", false);
            }

            if (Input.GetKey(KeyCode.O))
            {
                SetAnimBool("IsGuard", true);
            }
            else
            {
                SetAnimBool("IsGuard", false);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                
                SetAnimTrigger("BButton");

               

            }
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

        if(opption > 2)
        {
            opption = 2;
        }

        if (Rolling || _isGuard)
        {
            IsInvincible = true;
        } else
        {
            IsInvincible = false;
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
    private void JudgeAnim1()
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
                _isGuard = true;
                _shield.SetActive(true);
            } else
            {
                _isGuard = false;
                _shield.SetActive(false);
            }

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Rolling"))
            {
                
                transform.Translate(0.008f * _playerMovement.direction, 0, 0, Space.World);

                
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

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("ChargeAttackA") || _animator.GetCurrentAnimatorStateInfo(0).IsName("ChargeAttackASmash"))
            {

                
            } else
            {
                opption = 1;
            }
        }
    }
    private void JudgeAnim2()
    {

        if (_animator != null)
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
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") || _isJump && !Input.GetKey(KeyCode.L) && !Input.GetKey(KeyCode.O) && !Input.GetKey(KeyCode.P))
            {

                _playerMovement.CanMoveOffset = 1;
            }
            else
            {
                _playerMovement.CanMoveOffset = 0;
            }

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
            {
                _isGuard = true;
                _shield.SetActive(true);
            }
            else
            {
                _isGuard = false;
                _shield.SetActive(false);
            }

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Rolling"))
            {
                transform.Translate(0.008f * _playerMovement.direction, 0, 0, Space.World);

                
                Rolling = true;
            }
            else
            {
                Rolling = false;
            }
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Rolling") || _animator.GetCurrentAnimatorStateInfo(0).IsName("Guard"))
            {
                _playerMovement.IsGuard = true;
            }
            else
            {
                _playerMovement.IsGuard = false;
            }

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("ChargeAttackA") || _animator.GetCurrentAnimatorStateInfo(0).IsName("ChargeAttackASmash"))
            {


            }
            else
            {
                opption = 1;
            }
        }
    }

    public void Damaged(float param)
    {
        SetAnimTrigger("Damaged");
        SetAnimFloat("DamageDegree", param, 0f);
    }
}
