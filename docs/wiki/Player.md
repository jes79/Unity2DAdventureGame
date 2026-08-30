# 플레이어 시스템

플레이어 관련 컴포넌트는 `Assets/Scripts/Game/Player/`에 있으며, 하나의 플레이어 GameObject에 여러 컴포넌트가 조합되어 동작합니다.

| 컴포넌트 | 역할 |
| --- | --- |
| [`PlayerController`](../../2DAdventureGame/Assets/Scripts/Game/Player/PlayerController.cs) | 입력 처리 총괄 (이동/점프/공격/충돌 결과 처리), 사망·클리어 시 `GameController` 호출 |
| [`MovementRigidbody2D`](../../2DAdventureGame/Assets/Scripts/Common/MovementRigidbody2D.cs) | Rigidbody2D 기반 실제 물리 이동/점프 |
| [`PlayerAnimator`](../../2DAdventureGame/Assets/Scripts/Game/Player/PlayerAnimator.cs) | 애니메이터 파라미터 갱신 |
| [`PlayerHP`](../../2DAdventureGame/Assets/Scripts/Game/Player/PlayerHP.cs) | 체력, 피격 무적시간, 피격 시 색상 깜빡임 |
| [`PlayerWeapon`](../../2DAdventureGame/Assets/Scripts/Game/Player/PlayerWeapon.cs) | 투사체 발사 |
| [`PlayerProjectile`](../../2DAdventureGame/Assets/Scripts/Game/Player/PlayerProjectile.cs) | 발사된 투사체의 이동/충돌 |
| [`PlayerData`](../../2DAdventureGame/Assets/Scripts/Game/Player/PlayerData.cs) | 코인/투사체 보유 수/별 획득 상태 + HUD 갱신 트리거 |

## 이동 (`PlayerController.Update` → `MovementRigidbody2D`)

- `Input.GetAxisRaw("Horizontal")`로 좌우 입력을 받고, `Sprint` 축이 눌리면 속도 배율이 두 배(걷기 0.5 / 달리기 1.0)가 됩니다.
- `MovementRigidbody2D.MoveTo(x)`가 실제 `rigid2D.velocity.x`를 설정하며, `walkSpeed`(5) / `runSpeed`(8) 값으로 전환됩니다.
- `StageData.PlayerLimitMinX/MaxX` 범위로 x좌표가 `Mathf.Clamp` 됩니다.

## 점프 (`MovementRigidbody2D`)

가변 높이 점프와 조작감 보정 로직이 포함되어 있습니다.

- **가변 점프 높이**: 점프 키를 길게 누르는 동안(`IsLongJump == true`)은 `lowGravityScale`(2), 아니면 `highGravityScale`(3.5)을 적용해 점프 높이를 조절합니다.
- **코요테 타임(hang time)**: 바닥에서 벗어난 직후 `hangTime`(0.2초) 동안은 여전히 점프가 가능합니다.
- **점프 버퍼링**: 착지 직전 `jumpBufferTime`(0.1초) 이내에 점프 키를 누르면 착지 즉시 점프가 발동합니다.
- 머리/발 판정은 `Physics2D.OverlapBox`로 매 프레임 계산되며 `HitAboveObject` / `HitBelowObject` / `IsGrounded`로 노출됩니다.

## 체력 & 무적 (`PlayerHP`)

- 최대 체력 3칸, `DecreaseHP()` 호출 시 무적이 아니면 체력 1 감소 후 1초 무적(`OnInvincibility(1)`).
- 체력이 0이 되면 `PlayerController.OnDie()` → `GameController.LevelFailed()`.
- 무적 중에는 스프라이트 알파값을 `PingPong`으로 점멸시켜 시각적으로 표현합니다.
- `ItemInvincibility` 아이템 획득 시 별도 지속시간(기본 3초) 무적이 추가로 부여됩니다(중첩 시 시간 합산).

## 원거리 공격 (`PlayerWeapon`)

- `fireKeyCode`(기본 Z) 입력 + 보유 투사체(`PlayerData.CurrentProjectile`) > 0일 때 발사.
- 발사 방향은 마지막 이동 방향(`lastDirectionX`)을 따릅니다.
- 투사체는 `ItemProjectile` 아이템으로 최대 10개(`PlayerData.MaxProjectile`)까지 보충 가능합니다.

## 낙사 판정

`PlayerController.IsUnderGround()`가 매 프레임 `transform.position.y < StageData.MapLimitMinY`를 검사해 낙사 시 `OnDie()`를 호출합니다.

## 타일/발판과의 상호작용

- **머리 충돌**: 위로 이동 중(`velocity.y >= 0`) 머리에 `TileBase`가 감지되면 `tile.UpdateCollision()` 호출 → 바운스, 아이템 스폰, 파괴 등 타일 종류별 처리로 이어짐.
- **발 충돌**: 아래쪽 오브젝트가 `PlatformBase`면 `platform.UpdateCollision(gameObject)` 호출. `PlatformEffectorExtension`이 붙어있고 ↓ 키를 누르면 원웨이 플랫폼을 통과.
