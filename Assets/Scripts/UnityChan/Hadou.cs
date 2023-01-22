using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;

public class Hadou : MonoBehaviour
{
    [SerializeField]private float _deadtime;
    [SerializeField] private float _speed;
    [SerializeField] private string _playerNum;
    [SerializeField] private string _enemyNum;
    [SerializeField] private int _id;
    private Rigidbody _rb;
    private float x;
    private int _eNum;

    private GameObject _gameManager;
    private GameManager _script;
    private PlayerMovement _player1;
    private PlayerMovement _player2;
    private bool isHit = false;
    private SkillSet _skill;
    private NetWorkManager net;
    private int _pNum;

    private void Start()
    {
        net = GameObject.Find("NetWorkManager").GetComponent<NetWorkManager>();
        _rb = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("GameManager");
        _script = _gameManager.GetComponent<GameManager>();
        
        _player1 = GameObject.Find("P1").gameObject.GetComponent<PlayerMovement>();
        _player2 = GameObject.Find("P2").gameObject.GetComponent<PlayerMovement>();
        string PATH = "UnityChan/" + _id;
        _skill = Resources.Load<SkillSet>(PATH);

        if (_playerNum == "P1")
        {
            x = _player1.direction;
            _eNum = 1;
            _pNum = 0;
        }
        else if ((_playerNum == "P2"))
        {
            x = _player2.direction;
            _eNum = 0;
            _pNum = 1;

        }

    }

    void Update()
    {
        //this.transform.Translate(x,0,0, Space.World);
        _rb.AddForce(new Vector3(x, 0, 0));
        _deadtime -= Time.deltaTime;

        if(_deadtime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _enemyNum && !isHit)
        {

            SetAnim anim = other.GetComponent<SetAnim>();
            if (anim._isGuard)
            {
                anim._shieldsize -= new Vector3(1, 1, 1) * _skill.damage * 0.01f ;
                anim.GuardHit();
                isHit = true;
                Destroy(this.gameObject);
            }

            if (!anim.IsInvincible)
            {
                anim.Damaged(1f);


                _script.PlayerDamage[_eNum] += _skill.damage * net.cheerPower[_pNum];

                //float KB = ((0.1f + _script.PlayerDamage[_pNum] * 0.05f) * _script.PlayerDamage[_eNum] / 98f * 1.4f + 18f) * _skill.KBG * 0.01f + _skill.BKB;
                //float KB = (((_script.PlayerDamage[_eNum] + 0.01f) * _skill.KBG)*((0.1f + _script.PlayerDamage[_pNum] * 0.05f ) * 0.01f) * _ForceSys / (98f * 2) )+ _skill.BKB * 0.1f;
                float KB = (((_script.PlayerDamage[_eNum] + 0.01f) * _skill.KBG)) / (98f * 2) + _skill.BKB * 0.1f;
                Debug.Log(KB);
                Rigidbody _enemyrb = other.GetComponent<Rigidbody>();
                Vector3 _vec = _skill.vector.normalized;
                _enemyrb.AddForce(new Vector3(_vec.x * x * KB * 0.25f * net.cheerPower[_pNum], _vec.y * KB * 0.25f * net.cheerPower[_pNum], 0), ForceMode.Impulse);
                Debug.Log(_script.PlayerDamage[_eNum]);
                isHit = true;
                Destroy(this.gameObject);
            }
            
        }
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.tag == _enemyNum && !isHit)
        {

            SetAnim anim = other.GetComponent<SetAnim>();
            if (anim._isGuard)
            {
                anim._shieldsize -= new Vector3(1, 1, 1) * _skill.damage * 0.1f;
                isHit = true;
                Destroy(this.gameObject);
            }

            if (!anim.IsInvincible)
            {
                anim.Damaged(1f);

                _script.PlayerDamage[_eNum] += _skill.damage * net.cheerPower[_pNum];

                //float KB = ((0.1f + _script.PlayerDamage[_pNum] * 0.05f) * _script.PlayerDamage[_eNum] / 98f * 1.4f + 18f) * _skill.KBG * 0.01f + _skill.BKB;
                //float KB = (((_script.PlayerDamage[_eNum] + 0.01f) * _skill.KBG)*((0.1f + _script.PlayerDamage[_pNum] * 0.05f ) * 0.01f) * _ForceSys / (98f * 2) )+ _skill.BKB * 0.1f;
                float KB = (((_script.PlayerDamage[_eNum] + 0.01f) * _skill.KBG)) / (98f * 2) + _skill.BKB * 0.1f;
                Debug.Log(KB);
                Rigidbody _enemyrb = other.GetComponent<Rigidbody>();
                Vector3 _vec = _skill.vector.normalized;
                _enemyrb.AddForce(new Vector3(_vec.x * x * KB * 0.25f * net.cheerPower[_pNum], _vec.y * KB * 0.25f * net.cheerPower[_pNum], 0), ForceMode.Impulse);
                Debug.Log(_script.PlayerDamage[_eNum]);
                isHit = true;
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == _enemyNum)
        {
            isHit = false;
        }
    }
}
