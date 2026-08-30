# UI 시스템

## 인게임 HUD — `UIPlayerData`

[`UIPlayerData.cs`](../../2DAdventureGame/Assets/Scripts/Game/UI/UIPlayerData.cs)는 `PlayerData` / `PlayerHP`가 값이 바뀔 때마다 직접 호출하는 순수 표시 담당 클래스입니다.

| 메서드 | 표시 내용 |
| --- | --- |
| `SetHP(index, isActive)` | 체력 아이콘(`hpImages[index]`) 색상을 흰색/검은색으로 토글 |
| `SetCoin(coinCount)` | `"x {coinCount}"` 텍스트 |
| `SetProjectile(current, max)` | `"{current}/{max}"` 텍스트, 30% 이하이면 빨간색으로 경고 표시 |
| `SetStar(index)` | 해당 인덱스의 별 아이콘 오브젝트 활성화 |

## 팝업 — `UIGamePopup`

[`UIGamePopup.cs`](../../2DAdventureGame/Assets/Scripts/Game/UI/UIGamePopup.cs)는 `Time.timeScale`을 직접 제어해 일시정지/결과창 동안 게임 로직(물리, `Update`)을 멈춥니다.

| 메서드 | 상황 | 동작 |
| --- | --- | --- |
| `Pause()` | 일시정지 버튼 | `timeScale = 0`, 일시정지 팝업 표시 |
| `Resume()` | 일시정지 해제 | `timeScale = 1`, 팝업 숨김 |
| `LevelFailed()` | `GameController.LevelFailed()` | `timeScale = 0`, 실패 팝업 표시 |
| `LevelComplete(stars)` | `GameController.LevelComplete()` | `timeScale = 0`, 클리어 팝업 + 획득한 별 아이콘 표시 |
| `SelectLevel()` | 팝업 내 버튼 | `timeScale = 1` 복원 후 `SelectLevel` 씬 이동 |
| `Restart()` | 팝업 내 버튼 | `timeScale = 1` 복원 후 현재 씬 재시작 |
| `NextLevel()` | 클리어 팝업 버튼 | 마지막 레벨이면 `SelectLevel`로, 아니면 `CURRENT_LEVEL + 1` 저장 후 재시작 |

## 레벨 선택 UI — `UILevel`

[`UILevel.cs`](../../2DAdventureGame/Assets/Scripts/SelectLevel/UILevel.cs)는 `IPointerClickHandler`를 구현해 클릭 이벤트를 직접 처리합니다.

- 잠긴 레벨: 잠금 아이콘(`spriteLevelLock`)으로 교체, 레벨 번호 텍스트/별 배경 숨김, 클릭 무시
- 해제된 레벨: 레벨 번호 표시, 획득한 별 개수만큼 별 아이콘 활성화, 클릭 시 페이드 후 `Game` 씬으로 이동하며 `CURRENT_LEVEL` 저장

## 씬 전환 페이드 — `FadeEffect`

[`FadeEffect.cs`](../../2DAdventureGame/Assets/Scripts/Common/FadeEffect.cs)는 `Image`, `SpriteRenderer`, `TextMeshProUGUI`, `Tilemap` 네 가지 타입에 대해 동일한 시그니처의 알파 페이드 코루틴을 오버로드로 제공합니다. `Intro`의 텍스트 점멸/화면 전환, `SelectLevel`의 씬 진입/이탈, `HiddenArea`의 타일맵 노출 등에서 공통으로 재사용됩니다.
