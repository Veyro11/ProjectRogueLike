using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundChecker : MonoBehaviour
{
    public static EnemyGroundChecker Instance;
    public Transform groundCheck;          // 발밑 위치
    public LayerMask groundLayer;          // Ground 레이어
    public float groundCheckDistance = 0.3f; // 레이 길이

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    /// 발밑에 땅이 있는지 체크
    public bool IsGroundAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        // Scene 뷰에서 빨간 레이 확인용
        Debug.DrawRay(groundCheck.position, Vector2.down * groundCheckDistance, Color.red);

        return hit.collider != null; // true = 땅 있음 / false = 없음
    }
}
