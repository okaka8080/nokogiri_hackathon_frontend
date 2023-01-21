using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanAttack : MonoBehaviour
{
    private GameObject[] _attackArea = new GameObject[3];

    private void Start()
    {
        _attackArea[0] = transform.Find("1Attack").gameObject;
        _attackArea[1] = transform.Find("2Attack").gameObject;
        _attackArea[2] = transform.Find("3Attack").gameObject;
    }

    public void MakeHitArea(int num)
    {
        _attackArea[num].SetActive(true);
    }
    public void FadeHitArea(int num)
    {
        _attackArea[num].SetActive(false);
    }
}
