using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    [SerializeField] private int _id;
    private GameObject _gameManager;
    private GameManager _script;
    private PlayerMovement _player;
    private int _eNum;
    private SkillSet _skill;
    private bool isHit;
    private float opption;
    private int _pNum;

    private NetWorkManager net;

    private string _enemyNum = "null";
    private void Start()
    {
        net = GameObject.Find("NetWorkManager").GetComponent<NetWorkManager>();
        _gameManager = GameObject.Find("GameManager");
        _script = _gameManager.GetComponent<GameManager>();
        _player = transform.parent.GetComponent<PlayerMovement>();
        string PATH = "UnityChan/" + _id;
       



        if (transform.parent.gameObject.tag == "P1")
        {
            _enemyNum = "P2";
            _eNum = 1;
            _pNum = 0;
        } 
        else if(transform.parent.gameObject.tag == "P2")
        {
            _enemyNum = "P1";
            _eNum = 0;
            _pNum = 1;
        }
        _skill = Resources.Load<SkillSet>(PATH);
    }

    private void OnEnable()
    {
        SetAnim newanim = transform.parent.GetComponent<SetAnim>();
        opption = newanim.opption;
        isHit = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _enemyNum && !isHit)
        {
            
            SetAnim anim = other.GetComponent<SetAnim>();
            
            if (anim._isGuard)
            {
                anim._shieldsize -= new Vector3(1, 1, 1) * _skill.damage * 0.01f;
                anim.GuardHit();
                isHit = true;
            }
            if (!anim.IsInvincible)
            {
                anim.Damaged(1f);

                _script.PlayerDamage[_eNum] += _skill.damage * opption * net.cheerPower[_pNum];

                //float KB = ((0.1f + _script.PlayerDamage[_pNum] * 0.05f) * _script.PlayerDamage[_eNum] / 98f * 1.4f + 18f) * _skill.KBG * 0.01f + _skill.BKB;
                //float KB = (((_script.PlayerDamage[_eNum] + 0.01f) * _skill.KBG)*((0.1f + _script.PlayerDamage[_pNum] * 0.05f ) * 0.01f) * _ForceSys / (98f * 2) )+ _skill.BKB * 0.1f;
                float KB = ((((_script.PlayerDamage[_eNum] + 0.01f) * _skill.KBG)) / (98f * 2) + _skill.BKB * 0.1f ) * net.cheerPower[_pNum];
                Debug.Log(KB);
                Rigidbody _enemyrb = other.GetComponent<Rigidbody>();
                Vector3 _vec = _skill.vector.normalized;
                _enemyrb.AddForce(new Vector3(_vec.x * _player.direction * KB * 0.25f * (opption) * net.cheerPower[_pNum], _vec.y * KB * 0.25f * (opption) * net.cheerPower[_pNum], 0), ForceMode.Impulse);

                Debug.Log(_script.PlayerDamage[_eNum]);
                isHit = true;
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
                anim._shieldsize -= new Vector3(1, 1, 1) * _skill.damage * 0.01f * net.cheerPower[_pNum];
                anim.GuardHit();
                isHit = true;
            }
            if (!anim.IsInvincible)
            {
                anim.Damaged(1f);

                _script.PlayerDamage[_eNum] += _skill.damage * opption;

                //float KB = ((0.1f + _script.PlayerDamage[_pNum] * 0.05f) * _script.PlayerDamage[_eNum] / 98f * 1.4f + 18f) * _skill.KBG * 0.01f + _skill.BKB;
                //float KB = (((_script.PlayerDamage[_eNum] + 0.01f) * _skill.KBG)*((0.1f + _script.PlayerDamage[_pNum] * 0.05f ) * 0.01f) * _ForceSys / (98f * 2) )+ _skill.BKB * 0.1f;
                float KB = ((((_script.PlayerDamage[_eNum] + 0.01f) * _skill.KBG)) / (98f * 2) + _skill.BKB * 0.1f) * net.cheerPower[_pNum];
                Debug.Log(KB);
                Rigidbody _enemyrb = other.GetComponent<Rigidbody>();
                Vector3 _vec = _skill.vector.normalized;
                _enemyrb.AddForce(new Vector3(_vec.x * _player.direction * KB * 0.25f * (opption) * net.cheerPower[_pNum], _vec.y * KB * 0.25f * (opption) * net.cheerPower[_pNum], 0), ForceMode.Impulse);

                Debug.Log(_script.PlayerDamage[_eNum]);
                isHit = true;
            }
        }
    }

}
