using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BossData
{
    //TODO : 필요한 데이터 생성
    public float HP { get; set; }
    public int Attack { get; set; }
    public float AttackCoolTime { get; set; }
    public float MoveSpeed { get; set; }

    public BossData()
    {
        HP = 200;
        Attack = 30;
        AttackCoolTime = 0.5f;
        MoveSpeed = 0.8f;
    }
}
