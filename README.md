# Tower Defense - Architecture Refactoring

## 프로젝트 소개

유튜브 **'고박사의 유니티 노트'** 채널의 타워 디펜스 게임 개발 영상을 기반으로 제작한 프로젝트입니다.

기능 구현 중심으로 작성된 타워 디펜스 코드를 바탕으로, 실제 게임 개발에서 **확장성, 유연성, 재사용성, 생산성**을 함께 고려할 수 있는 구조를 실험하고 있습니다.

구조를 세분화하는 것 자체를 목표로 하지 않고,  
게임플레이 기능의 확장과 서로 다른 영역 사이의 관계 변화를 분리하면서도 개발 비용이 과도하게 증가하지 않는 방향을 찾는 것을 목표로 합니다.


## 구조 소개

이 프로젝트에서는 게임을 기본적으로 **Director와 Actor라는 주요 실행 주체**를 중심으로 구성합니다.

각 실행 주체의 내부 기능은 **Gameplay, Module, Presenter**로 분리하여 기능 확장과 재사용을 다루고, 서로 독립적인 실행 주체 사이의 특정 플레이 관계는 **Flow**로 분리하여 게임 흐름 변화에 대한 유연성을 확보합니다.

**Context**는 이러한 실행 주체가 현재 게임 구성의 외부 기능과 관계에 접근할 수 있는 범위를 제공합니다.

```text
                 Director / Actor
                  주요 실행 주체
                 /            \
                /              \
       내부 기능 분리 ↓          → 외부 관계 구성
 Gameplay / Module / Presenter   Flow / Context
      확장성 / 재사용성        유연성 / Composition
````

### Director / Actor

Director와 Actor는 게임을 구성하는 주요 실행 주체입니다.

**Director**는 Enemy Wave, Tower Build, Player Input처럼 하나의 gameplay 영역을 책임지고, 해당 영역의 기능과 플레이 흐름을 조율합니다.

**Actor**는 Tower, Enemy처럼 게임 세계에 존재하는 하나의 객체를 대표하며, 해당 객체의 기능과 동작을 구성합니다.

두 역할 모두 필요한 기능을 직접 구현할 수 있으며, 책임이 커지거나 독립적으로 변경할 필요가 생기면 Gameplay / Module / Presenter 등의 구성 요소로 분리합니다.

게임플레이의 복잡도가 증가할 때 새로운 상위 관리 계층을 계속 추가하기보다, 먼저 Director / Actor가 가진 구체적인 책임을 내부 구성 요소로 분리하는 방향을 사용합니다.

### Gameplay / Module / Presenter

Gameplay, Module, Presenter는 Director / Actor가 가진 책임을 필요에 따라 세분화하는 구성 요소입니다.

* **Gameplay**
  Director / Actor에 직접 구현할 수도 있었던 하나의 gameplay 책임을 분리합니다.
  가독성, 변경 분리, 재사용 등의 필요가 있을 때 추출합니다.

* **Module**
  공격, 이동, 체력, Spawn처럼 비교적 작은 기능 단위를 담당합니다.
  여러 구성에서 다시 사용할 가능성이 높은 기능일수록 구체적인 gameplay 구조와의 결합을 줄이는 방향을 고려합니다.

* **Presenter**
  UI 표시와 binding 등 presentation 책임을 담당합니다.

이 역할들은 반드시 거쳐야 하는 Layer가 아닙니다.
작은 기능은 Director / Actor에 직접 구현하고, 책임을 분리할 이유가 생겼을 때 필요한 역할만 추가합니다.

### Flow

Flow는 서로 독립적인 Director / Actor가 **특정 작업을 함께 수행할 때 필요한 관계와 일시적인 상태**를 담습니다.

예를 들어 Tower를 선택한 동안 Upgrade / Sell 작업을 수행하는 관계는 `SelectedTowerMaintenanceFlow`로 분리할 수 있습니다.

Flow는 특정 task의 시작부터 종료까지 필요한 참여자와 상태를 묶어, 독립적인 gameplay 영역 사이의 플레이 관계를 구성합니다.

어떤 Flow가 필요한지는 해당 gameplay 영역을 책임지는 Director가 판단할 수 있으며, Director는 자신이 시작한 Flow의 lifetime을 관리할 수 있습니다.

이를 통해 서로 다른 gameplay 영역 사이의 관계를 각 영역의 내부 구현과 분리합니다.

### Context

Context는 Director / Actor가 **현재 게임 구성에서 사용할 수 있는 외부 기능과 관계의 범위**를 제공합니다.

예를 들어:

* `TowerContext`는 Tower가 현재 공격 대상 후보에 접근할 수 있도록 합니다.
* `EnemyContext`는 Enemy의 사망이나 Goal 도달을 현재 Wave / Player 구성과 연결합니다.

실행 주체가 외부의 구체적인 구성이나 넓은 Creator 기능을 직접 알기보다, 자신에게 필요한 기능만 Context를 통해 접근하도록 합니다.

필요한 경우 Context는 현재 구성에서 사용할 수 있는 **Flow나 다른 Context의 생성 기능**도 제한된 형태로 제공할 수 있습니다.

### SceneRoot / Creator

SceneRoot는 현재 Scene의 concrete dependency를 알고 실제 객체를 구성하는 **Composition Root**입니다.

Flow와 Context의 구체적인 생성 방법은 SceneRoot에서 구성하고, Director / Actor는 필요한 생성 기능을 Creator 또는 Context를 통해 사용합니다.

이를 통해 **무엇이 필요한지 판단하는 책임**과 **어떤 concrete dependency로 구성할지 결정하는 책임**을 분리합니다.

```text 
Director / Actor
= 현재 gameplay에서 무엇이 필요한지 판단

SceneRoot
= 필요한 객체를 어떤 concrete dependency로 구성할지 결정

Flow / Context
= 현재 task와 외부 관계를 구성
```

## 개발 방향

### Director / Actor를 중심으로 기능을 분해

게임의 주요 책임은 Director / Actor에서 시작합니다.

책임이 커질 때 새로운 상위 Manager를 계속 추가하기보다, 해당 실행 주체 안의 구체적인 기능을 Gameplay / Module / Presenter로 분리하는 방향을 우선합니다.

이를 통해 게임의 주요 실행 주체는 유지하면서 내부 기능의 복잡도와 변경 범위를 줄입니다.

### 기능과 관계의 변화를 분리

기능 자체의 변화와 현재 게임에서 맺는 관계의 변화는 서로 다른 이유로 발생할 수 있습니다.

이 프로젝트에서는 대략 다음과 같이 서로 다른 변화의 원인을 분리합니다.

```text
실행 주체 내부의 기능 변화
→ Gameplay / Module / Presenter

현재 게임 구성에 대한 외부 접근
→ Context

특정 task에서의 관계와 플레이 흐름 변화
→ Flow
```

이를 통해 기능 자체의 확장과 게임 흐름의 변화를 서로 다른 문제로 다루는 것을 목표로 합니다.

### 재사용성과 게임 구체성에 따른 설계 판단

모든 역할에 같은 수준의 추상화와 재사용성을 요구하지 않습니다.

대략 다음과 같은 경향을 설계 판단의 기준으로 사용합니다.

```text
재사용 압력이 높음

Module
  ↓
Gameplay
  ↓
Actor / Director
  ↓
Flow / Context

현재 게임 구성에 더 구체적
```

이것은 Layer나 고정된 의존 방향을 의미하지 않습니다.

재사용 가능성이 높은 역할일수록 현재 게임의 구체적인 구성에 의존할 때 더 신중하게 판단하고, 필요하다면 좁은 Interface나 Context를 사용합니다.

반대로 Actor / Director는 자신이 책임지는 gameplay나 객체에 필요한 구체적인 기능을 구성할 수 있으며, Flow / Context는 현재 게임의 관계와 구성에 맞는 정보를 가질 수 있습니다.

이 기준 역시 Interface나 추상화를 미리 강제하기 위한 규칙은 아닙니다.
실제 재사용, 여러 구현, 테스트, 결합 문제 등의 필요가 생겼을 때 필요한 범위만 추상화합니다.

### 필요한 만큼만 구조화

모든 기능을 처음부터 Gameplay, Module, Presenter, Flow, Context, Interface로 분리하지 않습니다.

먼저 Director / Actor를 중심으로 필요한 기능을 구현하고, 다음과 같은 실제 필요가 생기는 시점에 구조를 분리합니다.

* 책임이 커져 가독성이 떨어질 때
* 독립적으로 변경될 가능성이 생길 때
* 다른 구성에서 재사용할 필요가 있을 때
* 서로 다른 gameplay 영역의 결합이 커질 때
* 협업이나 테스트를 위해 경계를 분리할 가치가 있을 때

구조적 선택이 개발 생산성을 해치지 않도록 **필요한 만큼만 분리하고 추상화하는 것**을 중요하게 봅니다.

### 독립적으로 개발하고 나중에 연결

가능한 경우 각 gameplay 영역과 객체는 자신의 책임 안에서 먼저 구현하고 검증합니다.

다른 영역의 구현이 아직 준비되지 않았더라도 필요한 Interface나 작은 계약을 통해 독립적으로 개발할 수 있도록 하고, 실제 플레이를 구성할 때 Flow / Context를 통해 관계를 연결합니다.

이를 통해 개별 기능의 개발과 전체 게임 구성 작업을 가능한 한 분리하고, 변경이나 협업에서 발생하는 영향을 줄이는 것을 목표로 합니다.

