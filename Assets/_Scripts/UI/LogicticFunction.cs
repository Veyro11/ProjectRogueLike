//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LogicticFunction
//{
//    public static float Logistic(float x, float K, float A)
//    {
//        /// <summary>
//        /// 로지스틱 함수를 이용해 UI의 체력바의 부드러운 움직임을 구현.
//        /// 해당 함수는 현재 시간값을 x로 받아 로지스틱함수 f(x) = K + (A-K) / (1 + e^(-k(x-x0))) 의 결과값을 반환.
//        /// 즉, UI매니저에서 코루틴을 통한 호출이 이루어질 예정.
//        /// ...굳이 UI매니저가 아니라 따로 바로 관리되는 UI 전반에 걸쳐 이렇게 처리해도 되지 않나?
//        /// </summary>

//        // y1-y0 / (1 + e^(-k(x-x0))) + y0
//        // K : 곡선의 최대값 (현재 체력: 
//        // k : 곡선의 기울기 (증가율)
//        // x0 : 곡선의 중앙점 (x축 이동) = 50
//        // min : 곡선의 최소값 (y축 이동)
//        // x : 현재 좌표상 x값 (시간 값)

//        // K, A는 조절 가능하도록 매개변수로 받음: 현재 HP에서 데미지를 받은 HP로 갈 수 있도록.
//        // 단계를 100 단위로 나눠서 표현하도록 하고, 데미지가 중복되면 현재 HP는 유지하되, 최소 HP가 감소하도록 하자.
//        // 데미지를 받는 도중 회복을 받을 때도 고려하도록 해야한다: 현재 체력을 시작점으로, 회복 후 체력(플레이어 값)을 종점으로 하여 로지스틱함수를 다시금 시작하는 것으로.
//        // x < 0일때 y = y1, x > 100일때 y = y0을 반환한다.

//        float errorBound = 0.005f; // 오차범위: 0.5%

//        float x1 = 0.05f; // 상한선 탈출점: 차후 수정이 필요할 수도 있어 따로 변수로 남겨둠.
//        float x2 = 0.95f; // 하한선 탈출점: 차후 수정이 rpy)

//        // 기울기 조절: 모든 로지스틱 함수가 같은 변곡점 x0을 지나면서 같은 탈출점 x_1, x_2를 가지도록 하는 상수 k의 값
//        // 수식 k = (2/(x2-x1)) * ln((1-δ)/δ) 에 기반함.
//        float k = (float)(2f / ((0.9/Time.deltaTime)) * Mathf.Log((1f - errorBound) / errorBound));

//        return K + (A - K) / (1 + Mathf.Exp(-k * (x - (float)Time.deltaTime)));
//    }

//}
// 사용되지 않는 클래스
