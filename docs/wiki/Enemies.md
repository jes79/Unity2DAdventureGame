# 적 AI

모든 적은 `Assets/Scripts/Game/Enemy/`에 있으며, 이동형 적은 [`EnemyBase`](../../2DAdventureGame/Assets/Scripts/Game/Enemy/EnemyBase.cs)(`IsDie`, `OnDie()` 추상 메서드)를 상속합니다. 공격 판정(플레이어 피격 / 투사체 처치)은 별도 자식 오브젝트의 [`EnemyCollider`](../../2DAdventureGame/Assets/Scripts/Game/Enemy/EnemyCollider.cs)가 담당합니다.

## 공통: `EnemyCollider`

- `Player` 태그와 충돌하면 `PlayerHP.DecreaseHP()` 호출 (적이 이미 죽은 상태면 무시)
- `PlayerProjectile` 태그와 충돌하면 `enemyBase.OnDie()` 호출 후 투사체 파괴

## 개구리 (`EnemyFrog`)

주기적으로 점프해서 이동하는 패턴.

1. `Idle` 코루틴: 2초 대기
2. `Jump` 코루틴: 점프 후 공중에서 `direction` 방향으로 이동, 착지 시 `Idle`로 복귀
3. `UpdateDirection()`: 진행 방향 앞쪽 바닥(`groundLayer`)이 없으면(낭떠러지) 방향 반전
4. 사망 시 스프라이트 페이드 아웃 후 2초 뒤 파괴

## 버섯 (`EnemyMushroom`)

[`FollowPath`](../../2DAdventureGame/Assets/Scripts/Common/FollowPath.cs)에 등록된 waypoint들을 왕복 이동.

- `FollowPath.Direction`(진행 방향)에 따라 스프라이트 좌우 반전
- `FollowPath.State`(Idle/Move)를 애니메이터 `moveSpeed` 파라미터에 반영
- 사망 시 `FollowPath.Stop()`으로 이동 정지 + `onDie` 트리거 → 애니메이션 이벤트로 [`EnemyMushroomAnimator.OnDieEvent()`](../../2DAdventureGame/Assets/Scripts/Game/Enemy/EnemyMushroomAnimator.cs)가 호출되어 실제 오브젝트 파괴

## 꽃 (`EnemyFlower`)

이동하지 않고 제자리에서 주기적으로 투사체를 발사.

- `attackRate`(기본 2초)마다 `onFire` 애니메이션 트리거 실행
- 애니메이션 이벤트로 [`EnemyFlowerAnimator.OnFireEvent()`](../../2DAdventureGame/Assets/Scripts/Game/Enemy/EnemyFlowerAnimator.cs)가 호출되어 `projectilePrefab`을 생성
- `EnemyBase`를 상속하지 않음 (근접 공격을 받지 않는 고정형 적)

## 적의 투사체 (`EnemyProjectile`)

- 아무 대상과 충돌해도 즉시 파괴됨
- 충돌 대상이 `Player`면 `PlayerHP.DecreaseHP()` 호출
