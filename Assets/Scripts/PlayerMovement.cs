using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //シリアライズフィールド
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;

    //パブリック変数
    public string nowState;
    public float CanMoveOffset;
    public bool IsGuard = false;
    public int direction;

    //プライベート変数
    private Rigidbody _rb;
    private GroundChecker _groundChecker;
    private Animator _animator;
    private int _jumpCount;
    private bool _isGround;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _groundChecker = GameObject.Find("GroundChecker").GetComponent<GroundChecker>();
        _animator = GetComponent<Animator>();
        CanMoveOffset = 1;
        nowState = "Stand";
        _jumpCount = 0;
        direction = 1;
    }

    void FixedUpdate()
    {
        if(_rb != null)
        {
            Movement();
        }
    }
    void Update()
    {
        if(_groundChecker!= null)
        {
            _isGround = _groundChecker.IsGround();
            if(_isGround )
            {
                _jumpCount = 0;
            }
        }
        if (_rb != null)
        {
            Jump();
        }
        
    }

    private void Movement()
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
            _rb.AddForce((new Vector3(1,0,0)*((5 - offset - _rb.velocity.x)* _runSpeed) * Mathf.Abs(Input.GetAxis("Horizontal")))* CanMoveOffset);
        } 
        else if (Input.GetKey(KeyCode.A)) 
        {
            if (_isGround)
            {
                transform.eulerAngles = new Vector3(0, -90, 0);
                direction = -1;
            }
            _rb.AddForce((new Vector3(-1, 0, 0)* ((5 - offset + _rb.velocity.x) * _runSpeed) * Mathf.Abs(Input.GetAxis("Horizontal")))* CanMoveOffset);
        } 
        else
        {

            _rb.velocity = new Vector3(Input.GetAxis("Horizontal") * CanMoveOffset, _rb.velocity.y, _rb.velocity.z);
        }
    }

    private void Jump()
    {

        if (_jumpCount < 1 && CanMoveOffset != 0.0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _rb.velocity = Vector3.zero;
                _rb.AddForce(new Vector3(0, 1, 0) * _jumpForce, ForceMode.Impulse);
                _jumpCount++;
            }
        } 
    }

    private void IsAttack()
    {

    }

    IEnumerator MoveFoword()
    {
        
        yield return null;
    }
    
}
