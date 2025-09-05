

![4](https://github.com/user-attachments/assets/2e8350f3-a123-4323-9a96-350a3b0d06f1)

# HAL SWORD

# 프로젝트 소개
스파르타코딩클럽 내일배움캠프 - 중간 프로젝트 Unity 기반의 2D 액션 로그라이크 게임 간단한 조작으로도 시원한 액션과 전략적 회피, 반복 성장의 재미를 제공

# 개발 기간
2025.09.01 (월) - 2023.09.05 (금)

# 팀원 구성


# Development Environment
Language: C#

Engine: Unity 2022.3.2f1

IDE: Visual Studio 2022


## 게임 흐름
타이틀 화면

마을: 강화 및 회복, 포탈 이동

튜토리얼: 조작 학습 및 아이템 획득

보스방: 구조물 없는 전투 공간에서 보스와의 전투
## 조작법
| 동작             | 키보드 입력     |
|------------------|-----------------|
| 이동             | A / D           |
| 점프             | Space           |
| 대쉬             | Shift           |
| 공격             | 마우스 좌클릭   |
| 회복 아이템 사용 | Q               |
| 상호작용         | E               |
| NPC대화         | F               |
| 메뉴 열기        | ESC             |


공격 중 점프/대쉬로 모션 캔슬 가능

피격 시 1초간 무적 상태 (깜빡임 처리)

# 주요 기능

## 몬스터 시스템
일반 몬스터: 단순 FSM 기반 근접 공격

보스 몬스터:

상태 흐름: Idle → Chase → Attack → Return → Die

공격 패턴:

전방 2m 찍기 (대미지 계수 1.5)

전방 4m 베기 (대미지 계수 1.0)

예고 이펙트 → 공격 판정 → 후딜 → 대응 유도

## 성장 및 강화 시스템
자원: 영혼

사망 시 1개 / 보스 처치 시 3개 획득

강화 항목:

체력 / 공격력 / 회복 포션 / 필살기 해금

강화 방식: 마을 NPC와 상호작용하여 영구 강화

필살기: 기류 15개 소모 → 강력한 공격 발동

## 아이템 시스템
스탯 아이템: 공격력, 이동속도 등 즉시 성장

회복 아이템: Q키 사용, 최대 체력의 30% 회복

아이템은 탐험 중 획득, 사망 시 초기화됨

## 맵 구성
마을: 강화, 회복, 포탈

튜토리얼: 조작 학습, 아이템 획득

보스방: 구조물 없음, 전투 집중 공간 (27m 넓이)

플랫폼: 타일 기반, 충돌 처리 명확, 클라이밍 요소 포함

## UI/UX
HP바, 무기 아이콘, 스탯 아이템 표시

오브젝트 상호작용 팝업: Press E

강화 UI: 영혼 수량, 강화 단계, 버튼 색상으로 상태 표현

Game Over 시 반투명 레이어 + 재시작/종료 선택

## 저장 시스템
자동 저장: 포탈 상호작용, 보스 처치, 강화 시점

프로필 데이터: 영혼, 강화 상태 등 영구 저장

진행 데이터: 현재 체력, 아이템, 위치 등 임시 저장

이어하기 기능 지원

## 사운드 구성
타이틀/전투 BGM

공격/피격/대쉬/점프/포탈 이동 효과음

Game Over 및 UI 클릭 사운드 포함

## 주요 기술
상태 머신(FSM, Finite State Machine)
플레이어, 몬스터, 보스의 행동을 상태 단위(Idle, Chase, Attack, Die 등)로 관리
각 상태별 로직을 분리하여 코드 유지보수성과 확장성을 강화

오브젝트 풀링(Object Pooling)
투사체, 이펙트, 피격 판정 등 빈번히 생성/파괴되는 객체를 풀로 관리
ObjectPoolManager를 통해 성능 최적화 및 GC(가비지 컬렉션) 최소화

오디오 매니저(Audio Manager)
BGM과 SFX를 리소스(Resources) 폴더 기반으로 로드하여 관리
싱글톤 패턴 적용, 채널 분리 및 볼륨 제어 지원

카메라 시스템(Cinemachine)
플레이어를 따라다니는 부드러운 카메라 추적 구현
줌인/줌아웃, 화면 흔들림(Shake) 연출 적용

아이템 관리(ScriptableObject 기반)
무기, 스탯, 포션 등 아이템 데이터를 ScriptableObject로 관리
확장성과 재사용성 강화, 인스펙터 기반 손쉬운 데이터 수정 가능

UI 프레임워크 기반 관리
HP바, 강화 UI, 팝업 등 UI 요소를 이벤트 기반 시스템으로 관리
BarEventManager 등을 활용해 데이터 변화 → UI 자동 반영 구조 구축


## 👥 개발팀 소개

<table align="center">
  <tr>
    <td align="center" width="200px">
      <br/>
      <b>팀장 [김우민]</b>
      <br/>
      <sub>맵 디자인/씬 관리/아이템 상호작용</sub>
      <br/>
      <a href="https://github.com/woomin0011">
        <img src="https://img.shields.io/badge/GitHub-181717?style=flat&logo=github&logoColor=white"/>
      </a>
    </td>
    <td align="center" width="200px">
      <br/>
      <b>팀원 [원정우]</b>
      <br/>
      <sub>몬스터 FSM/몬스터&보스 패턴/오디오 매니저/</sub>
      <br/>
      <a href="https://github.com/ONEJEUNGWOO">
        <img src="https://img.shields.io/badge/GitHub-181717?style=flat&logo=github&logoColor=white"/>
      </a>
    </td>
    <td align="center" width="200px">
      <br/>
      <b>팀원 [최우영]</b>
      <br/>
      <sub>플레이어 FSM/전투/타이틀/세이브 로드</sub>
      <br/>
      <a href="https://github.com/wooyoung-1">
        <img src="https://img.shields.io/badge/GitHub-181717?style=flat&logo=github&logoColor=white"/>
      </a>
    </td>
    <td align="center" width="200px">
      <br/>
      <b>팀원 [정찬혁]</b>
      <br/>
      <sub>강화 시스템/UI</sub>
      <br/>
      <a href="https://github.com/Veyro11">
        <img src="https://img.shields.io/badge/GitHub-181717?style=flat&logo=github&logoColor=white"/>
      </a>
    </td>
    <td align="center" width="200px">
      <br/>
      <b>팀원 [김민]</b>
      <br/>
      <sub>기획/QA</sub>
      <br/>
      <a href="https://github.com/Min545">
        <img src="https://img.shields.io/badge/GitHub-181717?style=flat&logo=github&logoColor=white"/>
      </a>
    </td>
    <td align="center" width="200px">
      <br/>
      <b>팀원 [서승우]</b>
      <br/>
      <sub>기획/일정관리</sub>
      <br/>
      <a href="https://github.com/polaris2910">
        <img src="https://img.shields.io/badge/GitHub-181717?style=flat&logo=github&logoColor=white"/>
      </a>
    </td>
  </tr>
</table>
