# 레벨 & 저장 데이터

## `StageData` (ScriptableObject)

[`StageData.cs`](../../2DAdventureGame/Assets/Scripts/Common/StageData.cs)는 레벨마다 하나씩(`Level01.asset` ~ `Level10.asset`) 존재하는 데이터 에셋입니다.

| 필드 | 용도 |
| --- | --- |
| `cameraLimitMinX / MaxX` | 카메라가 좌우로 이동 가능한 x좌표 범위 |
| `playerLimitMinX / MaxX` | 플레이어가 이동 가능한 x좌표 범위 |
| `mapLimitMinY` | 이 y좌표보다 아래로 떨어지면 낙사 처리 |
| `playerPosition` | 레벨 시작 시 플레이어 스폰 좌표 |
| `cameraPosition` | 레벨 시작 시 카메라 초기 좌표 |

`GameController`가 `allStageData[currentLevel-1]`을 `PlayerController.Setup()` / `CameraFollowTarget.Setup()`에 전달해 각 레벨의 경계값을 적용합니다.

## 레벨 프리팹

`Assets/LevelData/Prefab/LevelNN.prefab`은 해당 레벨의 실제 맵(타일맵, 적, 아이템 타일, 발판, 오브젝트, `PropsGoal` 등)을 담은 프리팹입니다. `GameController.levelPrefabs` 배열의 인덱스와 `allStageData` 배열의 인덱스가 1:1로 대응해야 하므로, **새 레벨을 추가할 때는 두 배열에 같은 순서로 등록**해야 합니다.

## 저장 데이터 (`PlayerPrefs`)

[`Constants.cs`](../../2DAdventureGame/Assets/Scripts/Common/Constants.cs)가 정의하는 키:

| 키 | 의미 |
| --- | --- |
| `CURRENT_LEVEL` | 현재(마지막으로 진입한) 레벨 번호 |
| `LEVEL_UNLOCK{N}` | N번 레벨 잠금 해제 여부 (1 = 해제) |
| `LEVEL_STAR_{N}{i}` | N번 레벨의 i번째(0~2) 별 획득 여부 |
| `COINCOUNT` | 누적 보유 코인 |

### 흐름

1. `SelectLevelController.Awake()`가 1번 레벨을 강제로 잠금 해제(`LEVEL_UNLOCK1 = 1`)하고, 레벨 1~10에 대해 `Constants.LoadLevelData(i)`로 잠금/별 상태를 읽어 `UILevel`에 표시
2. 플레이어가 레벨 아이콘 클릭 → `CURRENT_LEVEL` 저장 후 `Game` 씬 로드
3. `GameController.Awake()`가 `CURRENT_LEVEL`, `COINCOUNT`를 읽어 레벨/코인 상태 복원, 이미 획득한 별은 맵에서 제거
4. 레벨 클리어 시 `Constants.LevelComplete(level, stars, coinCount)` 호출
   - 코인 저장
   - `level+1`이 `MaxLevel`(10) 이하이면 다음 레벨 잠금 해제
   - 이번 레벨에서 획득한 별 3개 상태 저장
5. `UIGamePopup.NextLevel()`에서 현재 레벨이 최대 레벨이면 레벨 선택 화면으로, 아니면 `CURRENT_LEVEL + 1`로 갱신 후 `Game` 씬 재로드

### 데이터 초기화

`SelectLevelController`에 `[ContextMenu("ResetData")] ResetData()`가 있어, Unity 에디터에서 해당 컴포넌트의 우클릭 메뉴로 `PlayerPrefs.DeleteAll()`을 실행해 전체 진행 상황을 초기화할 수 있습니다.

## 새 레벨 추가 체크리스트

1. `Assets/LevelData/StageData/LevelNN.asset` 생성 및 경계값/시작 좌표 설정
2. `Assets/LevelData/Prefab/LevelNN.prefab`으로 맵 구성 (타일/적/아이템/발판/`PropsGoal` 배치, 별 3개 배치 및 `starIndex` 0~2 설정)
3. `GameController`의 `levelPrefabs`, `allStageData` 배열에 같은 순서로 등록
4. `Constants.MaxLevel` 값을 총 레벨 수에 맞게 갱신
