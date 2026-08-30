# 스크립트 레퍼런스

전체 51개 C# 스크립트를 폴더별로 정리한 표입니다. 링크는 저장소 내 실제 경로입니다.

## Common (`Assets/Scripts/Common/`)

| 스크립트 | 역할 |
| --- | --- |
| [`Constants.cs`](../../2DAdventureGame/Assets/Scripts/Common/Constants.cs) | 최대 레벨 수, 별 개수, `PlayerPrefs` 키 상수, 레벨 로드/클리어 저장 헬퍼, `ItemType` enum |
| [`Utils.cs`](../../2DAdventureGame/Assets/Scripts/Common/Utils.cs) | 씬 전환 헬퍼(`LoadScene`), `SceneNames` enum |
| [`StageData.cs`](../../2DAdventureGame/Assets/Scripts/Common/StageData.cs) | 레벨별 카메라/플레이어 이동 한계, 낙사 기준선, 시작 좌표 (ScriptableObject) |
| [`FadeEffect.cs`](../../2DAdventureGame/Assets/Scripts/Common/FadeEffect.cs) | Image/SpriteRenderer/TMP/Tilemap 알파 페이드 코루틴 (static) |
| [`CameraFollowTarget.cs`](../../2DAdventureGame/Assets/Scripts/Common/CameraFollowTarget.cs) | 지정 축(x/y/z)만 타겟을 따라가는 카메라, 좌우 이동 한계 클램프 |
| [`MovementRigidbody2D.cs`](../../2DAdventureGame/Assets/Scripts/Common/MovementRigidbody2D.cs) | Rigidbody2D 기반 이동/가변 점프/코요테 타임/점프 버퍼링 (플레이어·개구리·투사체 공용) |
| [`MovementTransform2D.cs`](../../2DAdventureGame/Assets/Scripts/Common/MovementTransform2D.cs) | 물리 없이 방향×속도로 이동 |
| [`MovementEffects.cs`](../../2DAdventureGame/Assets/Scripts/Common/MovementEffects.cs) | 발소리/착지 파티클 이펙트 제어 |
| [`FollowPath.cs`](../../2DAdventureGame/Assets/Scripts/Common/FollowPath.cs) | 여러 waypoint를 순서대로 왕복 이동 |
| [`ParallaxBackground.cs`](../../2DAdventureGame/Assets/Scripts/Common/ParallaxBackground.cs) | 카메라 이동량 기반 배경 레이어 패럴랙스 스크롤 |
| [`RotateBetweenAToB.cs`](../../2DAdventureGame/Assets/Scripts/Common/RotateBetweenAToB.cs) | Sin 함수 기반 좌우(각도) 왕복 회전 |
| [`RotateToAxis.cs`](../../2DAdventureGame/Assets/Scripts/Common/RotateToAxis.cs) | 지정 축으로 일정 속도 계속 회전 |
| [`HiddenArea.cs`](../../2DAdventureGame/Assets/Scripts/Common/HiddenArea.cs) | 플레이어 진입 시 Tilemap을 페이드로 숨김/노출 |
| [`AutoDestroyer.cs`](../../2DAdventureGame/Assets/Scripts/Common/AutoDestroyer.cs) | 지정 시간 후 비활성화 또는 파괴 |
| [`ParticleAutoDestroyer.cs`](../../2DAdventureGame/Assets/Scripts/Common/ParticleAutoDestroyer.cs) | 파티클 재생 종료 시 오브젝트 파괴 |

## Intro (`Assets/Scripts/Intro/`)

| 스크립트 | 역할 |
| --- | --- |
| [`IntroController.cs`](../../2DAdventureGame/Assets/Scripts/Intro/IntroController.cs) | 타이틀 텍스트 점멸, 키 입력 시 페이드 후 `SelectLevel` 씬 전환 |

## SelectLevel (`Assets/Scripts/SelectLevel/`)

| 스크립트 | 역할 |
| --- | --- |
| [`SelectLevelController.cs`](../../2DAdventureGame/Assets/Scripts/SelectLevel/SelectLevelController.cs) | 10개 레벨 아이콘 생성, 진입 페이드, `ResetData` 컨텍스트 메뉴 |
| [`UILevel.cs`](../../2DAdventureGame/Assets/Scripts/SelectLevel/UILevel.cs) | 개별 레벨 아이콘: 잠금/별 표시, 클릭 시 `Game` 씬 이동 |

## Game / Player (`Assets/Scripts/Game/Player/`)

| 스크립트 | 역할 |
| --- | --- |
| [`PlayerController.cs`](../../2DAdventureGame/Assets/Scripts/Game/Player/PlayerController.cs) | 입력 총괄, 타일/발판 충돌 분기, 낙사 판정, 사망/클리어 콜백 |
| [`PlayerAnimator.cs`](../../2DAdventureGame/Assets/Scripts/Game/Player/PlayerAnimator.cs) | 이동 상태 기반 애니메이터 파라미터 갱신, 스프라이트 좌우 반전(Scale) |
| [`PlayerHP.cs`](../../2DAdventureGame/Assets/Scripts/Game/Player/PlayerHP.cs) | 체력 증감, 피격 무적시간, 점멸 연출 |
| [`PlayerWeapon.cs`](../../2DAdventureGame/Assets/Scripts/Game/Player/PlayerWeapon.cs) | 투사체 생성 및 발사 방향 전달 |
| [`PlayerProjectile.cs`](../../2DAdventureGame/Assets/Scripts/Game/Player/PlayerProjectile.cs) | 투사체 이동, 바닥에 닿으면 튕김, 속도 감소 시 파괴 |
| [`PlayerData.cs`](../../2DAdventureGame/Assets/Scripts/Game/Player/PlayerData.cs) | 코인/투사체 보유량/별 획득 상태 관리, HUD 갱신 트리거 |

## Game / Enemy (`Assets/Scripts/Game/Enemy/`)

| 스크립트 | 역할 |
| --- | --- |
| [`EnemyBase.cs`](../../2DAdventureGame/Assets/Scripts/Game/Enemy/EnemyBase.cs) | 사망 상태(`IsDie`)와 `OnDie()` 추상 정의 |
| [`EnemyCollider.cs`](../../2DAdventureGame/Assets/Scripts/Game/Enemy/EnemyCollider.cs) | 플레이어 피격/투사체 처치 판정 공용 컴포넌트 |
| [`EnemyFrog.cs`](../../2DAdventureGame/Assets/Scripts/Game/Enemy/EnemyFrog.cs) | 대기 → 점프 이동 반복, 낭떠러지에서 방향 전환 |
| [`EnemyMushroom.cs`](../../2DAdventureGame/Assets/Scripts/Game/Enemy/EnemyMushroom.cs) | `FollowPath` 기반 왕복 이동 적 |
| [`EnemyMushroomAnimator.cs`](../../2DAdventureGame/Assets/Scripts/Game/Enemy/EnemyMushroomAnimator.cs) | 사망 애니메이션 이벤트로 오브젝트 파괴 |
| [`EnemyFlower.cs`](../../2DAdventureGame/Assets/Scripts/Game/Enemy/EnemyFlower.cs) | 고정형 적, 주기적으로 발사 애니메이션 트리거 |
| [`EnemyFlowerAnimator.cs`](../../2DAdventureGame/Assets/Scripts/Game/Enemy/EnemyFlowerAnimator.cs) | 발사 애니메이션 이벤트로 투사체 생성 |
| [`EnemyProjectile.cs`](../../2DAdventureGame/Assets/Scripts/Game/Enemy/EnemyProjectile.cs) | 적 투사체: 플레이어 피격 또는 충돌 시 파괴 |

## Game / Item (`Assets/Scripts/Game/Item/`)

| 스크립트 | 역할 |
| --- | --- |
| [`ItemBase.cs`](../../2DAdventureGame/Assets/Scripts/Game/Item/ItemBase.cs) | 스폰 연출, 수집 판정, 자동 소멸 공통 로직 |
| [`ItemCoin.cs`](../../2DAdventureGame/Assets/Scripts/Game/Item/ItemCoin.cs) | 코인 +1 |
| [`ItemStar.cs`](../../2DAdventureGame/Assets/Scripts/Game/Item/ItemStar.cs) | 별 획득 (0~2 인덱스) |
| [`ItemHPPotion.cs`](../../2DAdventureGame/Assets/Scripts/Game/Item/ItemHPPotion.cs) | 체력 회복 |
| [`ItemProjectile.cs`](../../2DAdventureGame/Assets/Scripts/Game/Item/ItemProjectile.cs) | 투사체 보유량 +1 |
| [`ItemInvincibility.cs`](../../2DAdventureGame/Assets/Scripts/Game/Item/ItemInvincibility.cs) | 일정 시간 무적 부여 |

## Game / Obstacle, Platform, Tile, Props, UI

| 스크립트 | 역할 |
| --- | --- |
| [`ObstacleBase.cs`](../../2DAdventureGame/Assets/Scripts/Game/Obstacle/ObstacleBase.cs) | 즉사 또는 체력 감소형 장애물 |
| [`PlatformBase.cs`](../../2DAdventureGame/Assets/Scripts/Game/Platform/PlatformBase.cs) | 발판 충돌 처리 추상 정의 |
| [`PlatformDrop.cs`](../../2DAdventureGame/Assets/Scripts/Game/Platform/PlatformDrop.cs) | 밟으면 흔들리다 낙하, 시간/사망 기준 재생성 |
| [`PlatformJump.cs`](../../2DAdventureGame/Assets/Scripts/Game/Platform/PlatformJump.cs) | 밟으면 플레이어를 강제로 튕겨 올림 |
| [`PlatformMoving.cs`](../../2DAdventureGame/Assets/Scripts/Game/Platform/PlatformMoving.cs) | 충돌 중인 오브젝트를 이동 발판의 자식으로 붙여 함께 이동 |
| [`PlatformEffectorExtension.cs`](../../2DAdventureGame/Assets/Scripts/Game/Platform/PlatformEffectorExtension.cs) | ↓ 키 입력 시 원웨이 발판을 일시적으로 통과 가능하게 전환 |
| [`TileBase.cs`](../../2DAdventureGame/Assets/Scripts/Game/Tile/TileBase.cs) | 머리 충돌 시 바운스 애니메이션 공통 로직 |
| [`TileItem.cs`](../../2DAdventureGame/Assets/Scripts/Game/Tile/TileItem.cs) | 충돌 시 아이템(또는 코인 N회) 생성, 소진 시 빈 타일로 전환 |
| [`TileBroke.cs`](../../2DAdventureGame/Assets/Scripts/Game/Tile/TileBroke.cs) | 충돌 시 파괴 이펙트 후 즉시 파괴 |
| [`PropsChest.cs`](../../2DAdventureGame/Assets/Scripts/Game/Props/PropsChest.cs) | 접촉 시 열려서 아이템 N개를 순차 생성 |
| [`PropsGoal.cs`](../../2DAdventureGame/Assets/Scripts/Game/Props/PropsGoal.cs) | 접촉 시 레벨 클리어 트리거 |
| [`PropsSign.cs`](../../2DAdventureGame/Assets/Scripts/Game/Props/PropsSign.cs) | 접촉 범위에서 안내 오브젝트 표시/숨김 |
| [`UIGamePopup.cs`](../../2DAdventureGame/Assets/Scripts/Game/UI/UIGamePopup.cs) | 일시정지/실패/클리어 팝업과 `Time.timeScale` 제어 |
| [`UIPlayerData.cs`](../../2DAdventureGame/Assets/Scripts/Game/UI/UIPlayerData.cs) | HP/코인/투사체/별 HUD 표시 갱신 |

## Game (루트)

| 스크립트 | 역할 |
| --- | --- |
| [`GameController.cs`](../../2DAdventureGame/Assets/Scripts/Game/GameController.cs) | 레벨 초기화, 별 상태 복원, 클리어/실패 처리, 저장 데이터 갱신 트리거 |
