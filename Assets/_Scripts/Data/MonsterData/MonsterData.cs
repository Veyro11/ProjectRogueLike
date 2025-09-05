using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData 
{
    //TODO : 필요한 데이터 생성
    public float HP { get; set; }
    public int Attack { get; set; }
    public float AttackCoolTime { get; set; }
    public float MoveSpeed { get; set; }

    public MonsterData()
    {
        HP = 50;
        Attack = 10;
        AttackCoolTime = 0.5f;
        MoveSpeed = 0.8f;
    }
}
