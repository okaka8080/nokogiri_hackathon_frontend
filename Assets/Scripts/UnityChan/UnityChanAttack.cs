using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanAttack : MonoBehaviour
{
    [SerializeField] private GameObject Hadou;
    [SerializeField] private Transform HadouPos;
    private GameObject[] _attackArea = new GameObject[5];
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _attackArea[0] = transform.Find("1Attack").gameObject;
        _attackArea[1] = transform.Find("2Attack").gameObject;
        _attackArea[2] = transform.Find("3Attack").gameObject;
        _attackArea[3] = transform.Find("4Attack").gameObject;
        _attackArea[4] = transform.Find("5Attack").gameObject;
    }

    public void MakeHitArea(int num)
    {
        _attackArea[num].SetActive(true);
    }
    public void FadeHitArea(int num)
    {
        _attackArea[num].SetActive(false);
    }
    public void MakeHadou()
    {
        _rb.velocity = Vector3.zero;
        Instantiate(Hadou, HadouPos.position, Quaternion.identity);
    }

}
