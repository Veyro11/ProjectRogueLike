using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyData
{
    //TODO : 필요한 데이터 생성
    public int HP { get; set; }
    public int Attack { get; set; }
    public float AttackCoolTime { get; set; }
    public float MoveSpeed { get; set; }

    public EnemyData()
    {
        HP = 10;
        Attack = 30;
        AttackCoolTime = 0.5f;
        MoveSpeed = 0.8f;
    }
}
