# 아키텍처 & 씬 흐름

## 씬 구성

`SceneNames` enum ([`Utils.cs`](../../2DAdventureGame/Assets/Scripts/Common/Utils.cs)) 기준으로 3개 씬이 존재합니다.

```
Intro  →  SelectLevel  →  Game
 ▲                            │
 └────── (레벨 클리어/실패 후 복귀) ──┘
```

| 씬 | 컨트롤러 | 역할 |
| --- | --- | --- |
| `Intro.unity` | [`IntroController`](../../2DAdventureGame/Assets/Scripts/Intro/IntroController.cs) | 타이틀 화면, "Press Any Key" 깜빡임, 키 입력 시 페이드 후 `SelectLevel`로 이동 |
| `SelectLevel.unity` | [`SelectLevelController`](../../2DAdventureGame/Assets/Scripts/SelectLevel/SelectLevelController.cs) | 10개 레벨 아이콘 생성, 잠금/별 상태 표시, 레벨 클릭 시 `Game` 씬으로 이동 |
| `Game.unity` | [`GameController`](../../2DAdventureGame/Assets/Scripts/Game/GameController.cs) | 선택된 레벨 프리팹 인스턴스화, 플레이어/카메라 설정, 클리어/실패 처리 |

씬 전환은 항상 [`FadeEffect`](../../2DAdventureGame/Assets/Scripts/Common/FadeEffect.cs) 코루틴으로 검은 화면을 페이드 인/아웃한 뒤 `Utils.LoadScene(...)`을 호출하는 패턴을 따릅니다.

## GameController 초기화 흐름

[`GameController.Awake()`](../../2DAdventureGame/Assets/Scripts/Game/GameController.cs)에서 일어나는 일:

1. `PlayerPrefs`에서 `CURRENT_LEVEL`, `COINCOUNT` 값을 읽어온다.
2. `levelPrefabs[currentLevel-1]`을 인스턴스화해 맵을 생성한다.
3. 생성된 맵의 `ItemStar` 컴포넌트를 모두 찾아, 이미 획득한 별(`StageData` 저장 값)이면 오브젝트를 비활성화한다.
4. `PlayerController.Setup(stageData)`, `CameraFollowTarget.Setup(stageData)`를 호출해 시작 위치/이동 한계를 적용한다.

레벨 클리어(`PropsGoal` 접촉) 또는 실패(낙사 등)는 각각 `GameController.LevelComplete()` / `LevelFailed()`를 통해 [`UIGamePopup`](../../2DAdventureGame/Assets/Scripts/Game/UI/UIGamePopup.cs)을 띄우고, 클리어 시 [`Constants.LevelComplete()`](../../2DAdventureGame/Assets/Scripts/Common/Constants.cs)로 저장 데이터를 갱신합니다.

## 레벨 데이터 구조 (`levelPrefabs` ↔ `allStageData`)

각 레벨은 두 개의 에셋 쌍으로 정의됩니다 (인덱스가 서로 매칭되어야 함).

- `Assets/LevelData/Prefab/LevelNN.prefab` — 실제 맵(타일, 적, 아이템, 발판 등)
- `Assets/LevelData/StageData/LevelNN.asset` — 카메라/플레이어 이동 범위, 시작 좌표 (`StageData` ScriptableObject)

## 공통(Common) 모듈

| 스크립트 | 역할 |
| --- | --- |
| `Constants.cs` | 최대 레벨 수, 별 개수, `PlayerPrefs` 키, 레벨 로드/저장 헬퍼 |
| `Utils.cs` | 씬 전환 헬퍼, `SceneNames` enum |
| `FadeEffect.cs` | Graphic(Image/Text) 알파값 코루틴 페이드 |
| `CameraFollowTarget.cs` | `StageData`의 카메라 이동 한계 내에서 타겟(플레이어) 추적 |
| `MovementRigidbody2D.cs` | Rigidbody2D 기반 이동/점프 공통 로직 (플레이어·적 공용) |
| `MovementTransform2D.cs` | Transform 기반 이동 (물리 없이 이동하는 오브젝트용) |
| `MovementEffects.cs` | 이동 관련 부가 효과 |
| `FollowPath.cs` | 지정된 경로를 따라 왕복 이동 (버섯 적 등에서 사용) |
| `ParallaxBackground.cs` | 배경 패럴랙스 스크롤 |
| `RotateBetweenAToB.cs` / `RotateToAxis.cs` | 회전 기믹 |
| `HiddenArea.cs` | 특정 영역 진입 시 가림/노출 처리 |
| `AutoDestroyer.cs` / `ParticleAutoDestroyer.cs` | 일정 시간/파티클 종료 후 오브젝트 자동 파괴 |
| `StageData.cs` | 레벨별 카메라/플레이어 이동 한계, 시작 위치 (ScriptableObject) |

## 폴더 ↔ 네임스페이스 매핑

프로젝트는 별도 C# 네임스페이스 없이, **폴더 구조로 기능 영역을 구분**합니다.

```
Scripts/
├─ Common/     # 씬에 무관한 공용 유틸리티
├─ Intro/      # Intro 씬 전용
├─ SelectLevel/# SelectLevel 씬 전용
└─ Game/       # Game 씬 전용
   ├─ Player/  ├─ Enemy/  ├─ Item/  ├─ Tile/
   ├─ Platform/├─ Props/  ├─ Obstacle/  └─ UI/
```
