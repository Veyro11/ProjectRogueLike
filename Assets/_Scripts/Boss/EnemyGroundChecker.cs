using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundChecker : MonoBehaviour
{
    public Transform groundCheck;          // 발밑 위치
    public LayerMask groundLayer;          // Ground 레이어
    public float groundCheckDistance = 0.3f; // 레이 길이

    private void Start()
    {
        StartCoroutine(IsWallChecker());
    }

    /// 발밑에 땅이 있는지 체크 후 bool값으로 반환 하는 메서드 입니다.
    public bool IsGroundChecker()
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

    //벽 끝에 있을 경우 플레이어 가 밀면 밀려나도록 했습니다. 이때 그냥 추적 멈추고 원래 자리로 돌아가는 건 어떤가 싶습니다.
    private IEnumerator IsWallChecker()
    { 
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (IsGroundChecker())
            {
                this.gameObject.GetComponentInParent<Rigidbody2D>().mass = 50;
            }
            else
            {
                this.gameObject.GetComponentInParent<Rigidbody2D>().mass = 5;
            }
        }

    }
}
