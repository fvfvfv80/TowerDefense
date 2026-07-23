# TowerDefenseGame

## 프로젝트 개요

`TowerDefenseGame`은 고박사 유튜브의 타워 디펜스 게임 제작 영상을 바탕으로 구현한 Unity 프로젝트입니다.

간단하고 확장을 크게 고려하지 않은 예제 프로젝트에 제가 생각해 온 게임플레이 구조를 적용하면서, 각 구성 요소의 책임과 사용 방식에 익숙해지는 것을 목표로 합니다.

원본의 플레이 흐름은 유지하되, 하나의 객체에 모여 있던 기능을 다음 기준에 따라 다시 구성하고 있습니다.

* 오브젝트 내부의 기능
* 객체의 생성과 관리
* 현재 게임에서 오브젝트가 수행하는 역할
* 여러 객체를 조합한 플레이 흐름

처음부터 모든 구조를 완성하기보다 실제 코드를 리팩터링하면서 각 책임의 경계를 확인하고 있습니다.

---

## 기본 구조

```text
SceneRoot
    │
    ▼
Director
    │
    ▼
Flow
    │
 ┌──┴────────────────────┐
 ▼                       ▼
Role                    Viewer
 │                       │
 ▼                       ▼
Actor                   View
 │
 ▼
Module
```

* `SceneRoot`는 Scene에서 사용되는 객체의 초기화와 조립을 담당합니다.
* `Director`는 여러 Flow의 실행 순서와 생명주기를 조율합니다.
* `Flow`는 여러 객체 사이의 실행 흐름과 게임 규칙을 구성합니다.
* `Role`은 Actor 내부에서 발생한 사건을 현재 게임의 외부 흐름에 연결합니다.
* `Actor`는 하나의 Unity 런타임 오브젝트를 대표하고 내부 Module을 연결합니다.
* `Module`은 Actor 내부의 구체적인 상태와 기능을 담당합니다.
* `Viewer`는 화면에 표시할 상태와 표현을 관리합니다.
* `View`는 실제 UI 컴포넌트와 사용자 입력을 담당합니다.

이 구조는 모든 기능이 반드시 모든 계층을 거쳐야 한다는 의미가 아닙니다.

각 기능은 `SerializeField`를 이용해 Scene에서 직접 연결하고 빠르게 테스트할 수 있으며, 전체 초기화 순서나 플레이 흐름을 제어해야 할 때는 `SceneRoot`와 `Director`를 통해 같은 객체들을 조립할 수 있도록 구성합니다.

기능의 규모와 책임에 따라 필요한 구성 요소만 사용합니다.

---

## Module

Module은 Actor 내부의 구체적인 상태와 기능을 담당합니다.

```text
체력
이동
공격
애니메이션
입력 감지
```

예를 들어 하나의 Enemy 오브젝트는 다음과 같은 Module로 구성됩니다.

```text
EnemyActor
    ├─ EnemyBaseModule
    ├─ EnemyHPModule
    ├─ EnemyMovementModule
    └─ EnemyAnimationModule
```

Module은 자신이 담당하는 기능에 집중하며, 현재 게임의 구체적인 Flow나 System을 직접 알지 않습니다.

외부 반응이 필요한 경우 Handler에 사건을 전달합니다.

```text
EnemyHPModule
    │ Hit / Despawn
    ▼
IEnemyHandler
```

기능을 무조건 Module로 분리하지는 않습니다. 오브젝트 내부에서 독립적인 상태나 동작으로 다룰 필요가 있을 때 Module로 분리합니다.

---

## System

System은 특정 종류의 객체와 데이터를 관리하고, 해당 영역에서 공통으로 사용되는 규칙과 계산을 담당합니다.

```text
객체와 데이터의 등록
객체와 데이터의 조회
객체의 생성과 반환
공통 규칙과 계산
```

현재 프로젝트의 `EnemySystem`은 게임에 존재하는 적을 관리합니다.

```text
EnemySystem
    ├─ 적 생성 또는 획득
    ├─ 현재 적 등록
    ├─ 현재 적 조회
    └─ 적 반환
```

System은 객체가 현재 게임에서 어떤 역할을 수행하는지나, 플레이가 어떤 순서로 진행되는지는 결정하지 않습니다.

```text
적의 생성과 관리
→ EnemySystem

적 사망에 따른 보상과 외부 처리
→ Role / Flow

적의 이동과 체력
→ Module
```

현재 `EnemySystem`은 객체 관리가 중심이지만, 이후 해당 영역에서 여러 객체가 공통으로 사용하는 계산이나 규칙이 생기면 System에서 함께 제공할 수 있습니다.

System은 프로젝트 전체를 통제하는 Manager가 아니라, 자신이 맡은 영역의 객체와 데이터, 공통 규칙에 책임을 제한합니다.

---

## Actor

Actor는 하나의 Unity 런타임 오브젝트를 대표합니다.

같은 GameObject에 존재하는 Module을 연결하고 초기화하며, Module이 외부로 사건을 전달할 수 있도록 Handler를 제공합니다.

```text
EnemyActor
    ├─ EnemyHPModule 연결
    ├─ EnemyMovementModule 연결
    ├─ EnemyAnimationModule 연결
    └─ IEnemyHandler 구현
```

Module에서 발생한 사건은 Actor를 통해 오브젝트 단위의 사건으로 해석됩니다.

```text
EnemyHPModule
    │ 체력 0
    ▼
IEnemyHandler.Despawn
    │
    ▼
EnemyActor
```

Actor는 내부 Module의 구성은 알지만, 현재 게임의 구체적인 Flow와 System에는 직접 의존하지 않는 것을 기본으로 합니다.

외부 게임 흐름과 상호작용해야 할 때는 Role을 이용합니다.

---

## Role

Role은 Actor가 현재 게임에서 수행하는 외부 역할을 표현합니다.

Module에서 발생한 사건은 Actor를 거쳐 Role에 전달되고, Role은 이를 현재 게임의 외부 흐름에 맞는 결과로 연결합니다.

```text
EnemyHPModule
    │ 사망 판정
    ▼
EnemyActor
    │ Despawn
    ▼
TowerDefenseEnemyRole
    ├─ 골드 지급
    ├─ 웨이브 적 수 감소
    └─ 적 반환
```

적이 목적지에 도착했을 때도 같은 Actor와 Module을 사용하면서 Role이 타워 디펜스의 규칙을 적용합니다.

```text
EnemyMovementModule
    │ 목적지 도착
    ▼
EnemyActor
    │ Arrive
    ▼
TowerDefenseEnemyRole
    ├─ 플레이어 피해
    ├─ 웨이브 적 수 감소
    └─ 적 반환
```

이를 통해 Actor와 Module은 `PlayerHPModule`, `PlayerGoldModule`, `EnemyWaveFlow`와 같은 외부 객체를 직접 알지 않아도 됩니다.

Role은 모든 게임 규칙을 직접 구현하는 객체라기보다, Actor가 현재 게임의 Flow와 System에 접근하는 관문입니다.

---

## Flow

Flow는 여러 객체를 조합해 하나의 플레이 규칙이나 실행 과정을 구성합니다.

```text
적 생성
타워 건설
한 웨이브의 진행
여러 웨이브의 순서
```

예를 들어 적과 웨이브의 흐름은 다음과 같이 나뉩니다.

```text
EnemySpawnFlow
→ 적 생성과 초기화
→ 이동 경로 전달
→ HP UI 생성

EnemyWaveFlow
→ 한 웨이브의 적 생성 주기
→ 현재 웨이브 적 수 관리

EnemyWaveSequenceFlow
→ 여러 웨이브의 순서 진행
```

하나의 Actor 내부에서 끝나는 기능은 Module에 두고, 여러 객체의 상태와 실행 순서를 함께 다룰 때 Flow를 사용합니다.
