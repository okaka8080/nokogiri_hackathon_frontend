using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float[] PlayerDamage = {0,0};

    public void DamaageTo(int attacker, int enemy, float damage)
    {
        PlayerDamage[attacker - 1] += damage;
    }


}
