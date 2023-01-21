using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    [SerializeField] private int _id;
    private float _ForceSys = 15;
    private GameObject _gameManager;
    private GameManager _script;
    private PlayerMovement _player;
    private int _pNum;
    private int _eNum;
    private float _offsetTime;
    private SkillSet _skill;
    private bool isHit;

    private string _enemyNum = "null";
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager");
        _script = _gameManager.GetComponent<GameManager>();
        _player = transform.parent.GetComponent<PlayerMovement>();
        string PATH = "UnityChan/" + _id;

        

        if (transform.parent.gameObject.tag == "P1")
        {
            _enemyNum = "P2";
            _pNum = 0;
            _eNum = 1;
        } 
        else if(transform.parent.gameObject.tag == "P2")
        {
            _enemyNum = "P1";
            _pNum = 1;
            _eNum = 0;
        }
        _skill = Resources.Load<SkillSet>(PATH);
    }

    private void OnEnable()
    {
        isHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == _enemyNum && !isHit)
        {
            _script.PlayerDamage[_eNum] += _skill.damage;
            //float KB = ((0.1f + _script.PlayerDamage[_pNum] * 0.05f) * _script.PlayerDamage[_eNum] / 98f * 1.4f + 18f) * _skill.KBG * 0.01f + _skill.BKB;
            float KB = (((_script.PlayerDamage[_eNum] + 0.01f) * _skill.KBG)*(0.1f + _script.PlayerDamage[_pNum] * 0.05f) * _ForceSys / (98f * 2) )+ _skill.BKB * 0.1f;
            Debug.Log(KB);
            Rigidbody _enemyrb = other.GetComponent<Rigidbody>();
            Vector3 _vec = _skill.vector.normalized;
            _enemyrb.AddForce(new Vector3(_vec.x * _player.direction * KB * 0.25f, _vec.y * KB * 0.25f, 0), ForceMode.Impulse);

            Debug.Log(_script.PlayerDamage[_eNum]);
            isHit = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == _enemyNum && !isHit)
        {
            _script.PlayerDamage[_eNum] += _skill.damage;
            //float KB = ((0.1f + _script.PlayerDamage[_pNum] * 0.05f) * _script.PlayerDamage[_eNum] / 98f * 1.4f + 18f) * _skill.KBG * 0.01f  + _skill.BKB;
            float KB = (((_script.PlayerDamage[_eNum] + 0.01f) * _skill.KBG) * (0.1f + _script.PlayerDamage[_pNum] * 0.05f) * _ForceSys / (98f * 2)) + _skill.BKB * 0.1f;
            Debug.Log(KB);
            Rigidbody _enemyrb = other.GetComponent<Rigidbody>();
            Vector3 _vec = _skill.vector.normalized;
            _enemyrb.AddForce(new Vector3(_vec.x * _player.direction * KB * 0.25f, _vec.y * KB * 0.25f, 0), ForceMode.Impulse);
            Debug.Log(_script.PlayerDamage[_eNum]);
            isHit = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == _enemyNum)
        {
            _offsetTime = 0f;
        }
    }
}
