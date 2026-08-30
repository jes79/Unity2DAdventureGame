# Unity2DAdventureGame

고박사 2DAdventureGame (Platformer)

Unity로 제작한 2D 사이드스크롤 플랫포머 게임입니다. 10개의 스테이지를 클리어하며 코인·별을 수집하고, 적을 피하거나 원거리 무기로 처치하는 것이 목표입니다.

## 게임 개요

| 항목 | 내용 |
| --- | --- |
| 장르 | 2D 사이드스크롤 플랫포머 |
| 엔진 | Unity 2023.1.16f1 |
| 렌더링 파이프라인 | Built-in RP (2D) |
| 스테이지 수 | 10 (`Constants.MaxLevel`) |
| 저장 방식 | `PlayerPrefs` (로컬 저장) |

## 조작법

| 입력 | 동작 |
| --- | --- |
| ← / → 방향키 | 이동 |
| Shift(Sprint) | 달리기 (이동 속도 2배) |
| C | 점프 (길게 누르면 높이 점프) |
| Z | 원거리 공격 (보유한 투사체 소모) |
| ↓ (아래 방향키) | 원웨이 발판 통과 |

키 코드는 [`PlayerController`](2DAdventureGame/Assets/Scripts/Game/Player/PlayerController.cs)의 `jumpKeyCode`, `fireKeyCode` 필드에서 변경할 수 있습니다.

## 핵심 시스템

- **씬 흐름**: `Intro` → `SelectLevel` → `Game` (씬 전환 시 페이드 효과 적용)
- **스테이지 데이터**: 레벨별 `StageData` ScriptableObject로 카메라/플레이어 이동 한계, 시작 위치, 낙사 기준선을 정의
- **레벨 진행 저장**: 클리어한 레벨의 잠금 해제, 별 3개 획득 여부, 보유 코인을 `PlayerPrefs`에 저장해 다음 실행 시 이어서 진행
- **플레이어**: 걷기/달리기/점프(가변 점프 높이, 코요테 타임, 점프 버퍼링), 체력 3칸, 피격 후 무적 시간, 원거리 투사체 공격
- **적**: 개구리(주기적 점프 이동), 버섯(경로를 따라 이동, `FollowPath` 기반), 꽃(주기적 투사체 발사)
- **아이템**: 코인, 별(스테이지당 3개), 체력 포션, 투사체, 무적
- **기믹**: 부서지는 타일, 아이템이 나오는 타일, 사라졌다 재생성되는 낙하 발판, 움직이는 발판, 원웨이 발판
- **UI**: HP/코인/투사체/별 HUD, 일시정지·레벨 실패·레벨 클리어 팝업

자세한 아키텍처 및 스크립트별 설명은 [docs/wiki](docs/wiki/Home.md)를 참고하세요.

## 프로젝트 구조

```
Unity2DAdventureGame/
└─ 2DAdventureGame/              # Unity 프로젝트 루트
   ├─ Assets/
   │  ├─ Scenes/                 # Intro, SelectLevel, Game
   │  ├─ Scripts/
   │  │  ├─ Common/              # 씬 공통(카메라, 이동, 페이드, 상수 등)
   │  │  ├─ Intro/                # 인트로 씬 로직
   │  │  ├─ SelectLevel/          # 레벨 선택 씬 로직
   │  │  └─ Game/
   │  │     ├─ Player/            # 플레이어 이동/체력/무기/데이터
   │  │     ├─ Enemy/             # 적 AI (개구리/버섯/꽃)
   │  │     ├─ Item/              # 코인/별/포션/투사체/무적 아이템
   │  │     ├─ Tile/              # 상호작용 가능한 타일
   │  │     ├─ Platform/          # 발판(낙하/이동/원웨이/점프)
   │  │     ├─ Props/             # 상자/골(도착지점)/안내판
   │  │     ├─ Obstacle/          # 즉사 또는 데미지 장애물
   │  │     └─ UI/                # HUD, 팝업
   │  ├─ LevelData/
   │  │  ├─ StageData/            # 레벨별 StageData 에셋 (Level01~10)
   │  │  └─ Prefab/               # 레벨별 맵 프리팹 (Level01~10)
   │  ├─ Animations/, Materials/, Textures/, Fonts/, TextMesh Pro/, Etc/
   │  └─ Prefabs/
   ├─ Packages/                   # UPM 패키지 매니페스트
   └─ ProjectSettings/
└─ docs/wiki/                     # 프로젝트 위키 문서
```

## 시작하기

1. **Unity Hub**에서 Unity `2023.1.16f1` (또는 호환 버전) 설치
2. Unity Hub → `Open` → 이 저장소의 `2DAdventureGame` 폴더 선택
3. `Assets/Scenes/Intro.unity` 씬을 열고 실행

```bash
git clone https://github.com/jes79/Unity2DAdventureGame.git
```

## 주요 패키지

- `com.unity.feature.2d` — 2D 기능 세트 (Sprite, Tilemap, Physics2D 등)
- `com.unity.textmeshpro` — UI 텍스트
- `com.unity.timeline`, `com.unity.visualscripting`, `com.unity.ide.rider` / `com.unity.ide.visualstudio`

전체 목록은 [`Packages/manifest.json`](2DAdventureGame/Packages/manifest.json) 참고.

## 문서

- [위키 홈](docs/wiki/Home.md)
- [아키텍처 & 씬 흐름](docs/wiki/Architecture.md)
- [플레이어 시스템](docs/wiki/Player.md)
- [적 AI](docs/wiki/Enemies.md)
- [아이템 · 타일 · 발판](docs/wiki/Items-Tiles-Platforms.md)
- [레벨 & 저장 데이터](docs/wiki/Level-System.md)
- [UI 시스템](docs/wiki/UI.md)
- [스크립트 레퍼런스](docs/wiki/Script-Reference.md)
