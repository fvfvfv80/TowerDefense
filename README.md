# Tower Defense - Architecture Refactoring

## 프로젝트 소개

유튜브 **'고박사의 유니티 노트'** 채널의 타워 디펜스 게임 개발 영상을 기반으로 제작한 프로젝트입니다.

기능 구현 중심으로 작성된 코드를 바탕으로, 실제 게임 개발에서 **확장성, 유연성, 재사용성, 생산성**을 함께 고려할 수 있는 구조를 실험하고 있습니다.

특히 게임플레이를 개발하면서 발생하는 **기능 자체의 변화**와 **서로 다른 실행 주체 사이의 관계 변화**를 가능한 한 분리하는 것을 중요하게 보고 있습니다.

---

## 구조 개요

이 프로젝트에서는 `Director`와 `Actor`를 주요 실행 주체로 사용합니다.

실행 주체 내부의 기능은 필요에 따라 `Gameplay`, `Module`로 분리하고, 외부 gameplay와의 관계는 `Scenario`를 통해 구성합니다. 관계를 별도의 단위로 관리할 필요가 생기면 `Flow`로 분리합니다.

```text
    기능 변화                   실행 주체                 외부 관계

Gameplay / Module  ←──────  Director / Actor  ──────→  Scenario
                                                             │
                                                             ↓
                                                            Flow
```

작은 기능이나 단순한 관계는 별도의 역할로 분리하지 않고 그대로 구현합니다.

---

## Director / Actor

### Director

`Director`는 하나의 의미 있는 gameplay 영역을 책임지는 실행 주체입니다.

예를 들면 다음과 같습니다.

- `TowerBuildDirector`
- `EnemyWaveDirector`
- `PlayerActionDirector`

해당 영역의 gameplay 판단과 조율을 담당하며, 내부 책임이 커지면 `Gameplay`나 `Module`로 분리할 수 있습니다.

외부 gameplay와의 관계가 필요한 경우에는 자신에게 필요한 기능을 Interface로 정의할 수 있습니다.

```text
TowerBuildDirector
        ↓
ITowerBuildScenario
```

### Actor

`Actor`는 게임 세계에 존재하는 하나의 의미 있는 객체를 책임지는 실행 주체입니다.

예를 들면 다음과 같습니다.

- `TowerActor`
- `EnemyActor`

객체의 상태, lifecycle, animation, gameplay 동작 등을 중심으로 구성하며, 필요에 따라 `Gameplay`와 `Module`을 내부 책임으로 가질 수 있습니다.

Actor 역시 외부 gameplay와의 관계를 내부 기능과 분리해 구성할 수 있습니다.

---

## 내부 책임

### Gameplay

`Gameplay`는 Director / Actor 내부의 **하나의 gameplay 책임을 별도로 다룰 필요가 생겼을 때** 분리합니다.

플레이 규칙, 상태, 동작의 조율 등을 해당 gameplay 책임 안에서 다룹니다.

```text
TowerActor
    ↓
TowerGameplay
    ↓
WeaponModule / TowerStatModule
```

### Module

`Module`은 비교적 작은 기능이나 역할을 담당합니다.

예를 들면 Weapon, Movement, HP, Stat, Spawn 등이 있으며, 재사용 가능성이 높거나 독립적으로 다룰 가치가 있는 기능을 Module로 분리합니다.

---

## 외부 관계

### Scenario

`Scenario`는 Director / Actor가 **현재 게임에서 외부 gameplay와 맺는 관계를 구성하는 역할**입니다.

실행 주체에서 발생한 gameplay의 의미를 현재 게임 구성에 맞게 다른 실행 주체나 관계와 연결합니다.

예를 들어 Tower가 생성되었을 때 `TowerBuildDirector`는 Tower Build의 결과를 전달하고, 생성된 Tower가 다른 gameplay와 어떻게 연결되는지는 `TowerBuildScenario`에서 구성할 수 있습니다.

```text
TowerBuildDirector
    ↓ NotifyTowerBuilt

TowerBuildScenario
    ├─ Tower 관계 연결
    └─ TowerScenario 구성
```

단순한 관계는 Scenario 안에서 직접 구성할 수 있습니다.

### Flow

`Flow`는 여러 실행 주체가 함께 참여하는 관계를 별도의 단위로 관리할 필요가 생겼을 때 사용합니다.

현재 프로젝트에서는 다음과 같은 Flow가 있습니다.

- `PlayerTowerBuildFlow`
- `SelectedTowerMaintenanceFlow`
- `TowerBindFlow`


Scenario가 관계를 구성하고, 그 관계의 상태나 시작과 종료를 별도로 관리할 필요가 생기면 Flow로 분리할 수 있습니다.

---

## 구성

### SceneRoot

`SceneRoot`는 현재 Scene에서 필요한 실제 객체와 관계를 구성하는 `Composition Root`입니다.

주로 Director와 Scenario를 구성하고, runtime에 필요한 Scenario / Flow의 생성 방법을 연결합니다.

```text
Director / Actor / Scenario
= gameplay에서 필요한 관계를 판단하고 사용

SceneRoot
= 실제 객체와 생성 방법을 구성
```

Gameplay 규칙 자체는 SceneRoot에 두지 않고 구성에 집중합니다.

---

## 설계 방향

### 기능과 관계의 변화를 분리

게임플레이 기능 자체의 변화와 다른 gameplay와 맺는 관계의 변화는 서로 다른 이유로 발생할 수 있습니다.

```text
기능 자체의 변화
→ Director / Actor
→ Gameplay / Module

외부 관계의 변화
→ Scenario
→ Flow
```

한쪽의 변경이 다른 쪽으로 불필요하게 퍼지는 것을 줄이는 것이 목적입니다.

### 필요한 만큼만 구조화

처음에는 `Director`, `Actor`, `Scenario` 안에서 필요한 기능과 관계를 직접 구현합니다.

개발하면서 따로 나눌 필요가 생기면 `Gameplay`, `Module`, `Flow`로 분리합니다.

```text
gameplay 책임이 커짐
→ Gameplay

재사용해서 쓰고 싶은 기능이 생김
→ Module

외부 관계를 따로 관리할 필요가 생김
→ Flow
```

처음부터 구조를 많이 나누기보다,
개발하면서 실제로 필요해질 때 하나씩 분리합니다.

### 재사용성과 유연성을 고려한 의존성 설계

역할의 재사용성과 변화 가능성을 고려해 직접 연결할지, Interface로 경계를 둘지 판단합니다.

```text
Module / Gameplay  ─────→  Director / Actor  ─────→  Scenario / Flow
                 Interface 고려            Interface 고려

Module / Gameplay  ←─────  Director / Actor  ←─────  Scenario / Flow
                 직접 연결 고려            직접 연결 고려
```

예를 들어 Module은 자신을 사용하는 실제 Actor를 모르더라도 필요한 기능을 기준으로 구현할 수 있습니다.

```text
WeaponModule
        ↓
IWeaponModuleHost


TowerActor
    implements IWeaponModuleHost
```

Director 역시 실제 Scenario의 내부 구성을 몰라도 외부 관계에서 필요한 기능을 기준으로 자신의 gameplay를 구현할 수 있습니다.

```text
PlayerActionDirector
        ↓
IPlayerActionScenario

PlayerActionScenario
    implements IPlayerActionScenario
```

이러한 경계를 통해 각 기능과 gameplay 영역을 자신의 책임 안에서 먼저 구현하고, 실제 게임을 구성할 때 관계를 연결할 수 있습니다.

**Interface는 모든 관계에 강제하지 않습니다.**  
생산성을 위해 단순한 관계에서는 생략할 수 있지만, 생략하더라도 Interface로 표현할 수 있는 의존성 경계를 의식하며 개발합니다.

---

## 현재 구조 예시

### Player Action → Tower Build

Player Action에서 Tower Build로 이어지는 현재 구조는 다음과 같습니다.

```text
PlayerInputGameplay
        ↓
IPlayerInputHost


PlayerActionDirector
        ↓
IPlayerActionScenario


PlayerActionScenario
        ↓
PlayerTowerBuildFlow
        ↓
TowerBuildDirector
```

각 부분은 다음 책임을 가집니다.

```text
PlayerInputGameplay
= 입력 처리

PlayerActionDirector
= Player Action 판단

PlayerActionScenario
= 외부 관계 구성

PlayerTowerBuildFlow
= Tower Build 관계

TowerBuildDirector
= Tower Build 실행
```

내부 기능, 실행 주체의 gameplay 판단, 외부 관계 구성을 각각 분리해서 관리합니다.