# 아이템 · 타일 · 발판 · 오브젝트

## 아이템 (`Assets/Scripts/Game/Item/`)

모든 아이템은 [`ItemBase`](../../2DAdventureGame/Assets/Scripts/Game/Item/ItemBase.cs)(추상 클래스)를 상속합니다.

- `Setup()` 호출 시 `SpawnItemProcess()` 코루틴 시작: `Rigidbody2D`를 동적으로 추가해 위로 튀어오르는 스폰 연출(`spawnForce`) 후, 착지하면 수집 가능(`allowCollect = true`) 상태가 됨
- 수집 가능 상태에서 `Player`와 충돌(Trigger/Collision 모두 대응)하면 `UpdateCollision(target)` 호출 후 자기 자신 파괴
- `aliveTimeAfterSpawn`(기본 5초) 동안 수집되지 않으면 자동 파괴

| 아이템 | 효과 |
| --- | --- |
| `ItemCoin` | `PlayerData.Coin ++` |
| `ItemStar` | `PlayerData.GetStar(starIndex)` — 스테이지당 0~2번 3개, `ItemType.Star`는 `GameController`가 이미 획득한 별을 비활성화하는 데 사용 |
| `ItemHPPotion` | `PlayerHP.IncreaseHP()` |
| `ItemProjectile` | `PlayerData.CurrentProjectile ++` (최대 10) |
| `ItemInvincibility` | `PlayerHP.OnInvincibility(time)` (기본 3초, 중첩 시 시간 누적) |

아이템 종류는 [`ItemType`](../../2DAdventureGame/Assets/Scripts/Common/Constants.cs) enum(`Random, Coin, Invincibility, HPPotion, Projectile, Star`)으로 구분됩니다.

## 타일 (`Assets/Scripts/Game/Tile/`)

[`TileBase`](../../2DAdventureGame/Assets/Scripts/Game/Tile/TileBase.cs)는 플레이어가 머리로 타일을 칠 때(`PlayerController.UpdateAboveCollision`) 호출되는 `UpdateCollision()`을 정의합니다. `canBounce`가 true면 타일이 위로 살짝 튀었다 돌아오는 바운스 애니메이션을 재생하고, 애니메이션 동안 `IsHit = true`로 재충돌을 막습니다.

| 타일 | 동작 |
| --- | --- |
| `TileItem` | 부딪힐 때마다 `itemPrefabs` 중 하나(고정 타입 또는 `Random`)를 생성. 코인 타입이면 `coinCount`를 소진할 때까지 반복 지급, 소진되면 `nonBrokeImage`로 스프라이트 교체 후 비활성 상태(`isEmpty`)가 됨 |
| `TileBroke` | 부딪히면 파괴 이펙트(`tileBrokeEffect`)를 생성하고 타일 오브젝트를 즉시 파괴 |

## 발판 (`Assets/Scripts/Game/Platform/`)

[`PlatformBase`](../../2DAdventureGame/Assets/Scripts/Game/Platform/PlatformBase.cs)는 플레이어가 발로 밟았을 때(`PlayerController.UpdateBelowCollision`) 호출되는 `UpdateCollision(GameObject other)`를 정의합니다.

| 발판 | 동작 |
| --- | --- |
| `PlatformDrop` | 밟으면 흔들리는 애니메이션(`OnShake`) 후 낙하(`Rigidbody2D.isKinematic = false`). `RespawnType.AfterTime`이면 `respawnTime` 후 원위치로 재생성, `PlayerDead`면 그대로 파괴 |
| `PlatformJump` | 밟으면 `onJump` 애니메이션 트리거 → 애니메이션 이벤트로 `JumpAction()` 호출, 플레이어에게 `jumpForce`(기본 22)만큼 즉시 상승 속도 부여 |
| `PlatformMoving` | 오브젝트가 충돌해 있는 동안 `target`(실제로 움직이는 발판)의 자식으로 부모 설정 → 발판과 함께 이동, 벗어나면 부모 해제 |
| `PlatformEffectorExtension` | `PlatformEffector2D`의 `rotationalOffset`을 반전시켜 ↓ 키로 원웨이 발판을 아래로 통과시킴 (`PlayerController`의 `UpdateBelowCollision`에서 호출) |

## 장애물 (`ObstacleBase`)

- `Player`와 충돌 시 `isInstantDeath`가 true면 즉사(`PlayerController.OnDie()`), false면 체력만 1 감소(`PlayerHP.DecreaseHP()`)

## 오브젝트/기믹 (`Assets/Scripts/Game/Props/`)

| 스크립트 | 동작 |
| --- | --- |
| `PropsChest` | 플레이어 접촉 시 열림 애니메이션(스프라이트 교체) 후 `itemCount`개의 아이템을 랜덤 간격으로 순차 생성, 완료 후 페이드아웃하며 파괴 |
| `PropsGoal` | 플레이어 접촉 시 `PlayerController.LevelComplete()` 호출 → 레벨 클리어 처리 |
| `PropsSign` | 플레이어가 범위에 들어오면 안내 오브젝트(`guideObject`) 표시, 벗어나면 숨김 |

## 기타 연출용 공용 컴포넌트

| 스크립트 | 용도 |
| --- | --- |
| `ParallaxBackground` | 카메라 x 이동량에 배경별 `speed` 배율을 곱해 텍스처 오프셋 이동 (레이어별 패럴랙스), 구름은 시간 기반으로 별도 스크롤 |
| `HiddenArea` | 플레이어가 Tilemap 영역에 들어오면 해당 타일맵을 알파 0으로 페이드(가려진 통로 등을 노출) |
| `MovementTransform2D` | 물리 없이 `moveDirection * moveSpeed`로 단순 이동 (발사체 등에서 활용) |
| `MovementEffects` | 이동 상태에 따라 발소리 파티클 방출량 조절, 착지 시 착지 이펙트 재생 |
| `FollowPath` | 지정한 waypoint들을 순서대로 왕복 이동 (버섯 적, 이동 발판 등에 재사용 가능) |
| `RotateBetweenAToB` / `RotateToAxis` | 특정 각도 내 왕복 회전 / 일정 속도로 계속 회전하는 기믹 오브젝트용 |
| `AutoDestroyer` / `ParticleAutoDestroyer` | 일정 시간 경과 또는 파티클 재생 종료 후 오브젝트 비활성화/파괴 |
