# Unity2DAdventureGame 위키

Unity 2023.1.16f1로 제작된 2D 사이드스크롤 플랫포머 게임의 개발 문서입니다.

## 목차

- [아키텍처 & 씬 흐름](Architecture.md) — 씬 구조, `GameController` 초기화 흐름
- [플레이어 시스템](Player.md) — 이동/점프/체력/무기
- [적 AI](Enemies.md) — 개구리 / 버섯 / 꽃
- [아이템 · 타일 · 발판](Items-Tiles-Platforms.md) — 수집 아이템, 상호작용 타일, 발판 기믹
- [레벨 & 저장 데이터](Level-System.md) — `StageData`, `PlayerPrefs` 저장 구조
- [UI 시스템](UI.md) — HUD와 팝업
- [스크립트 레퍼런스](Script-Reference.md) — 전체 스크립트 목록과 역할

## 빠른 요약

이 게임은 **Intro → SelectLevel → Game** 3개 씬으로 구성된 10스테이지 플랫포머입니다.
플레이어는 좌/우 이동, 달리기, 점프(높은 점프/낮은 점프), 원거리 공격을 사용할 수 있으며,
스테이지마다 코인·별 3개·아이템을 수집하고 `PropsGoal`에 도달하면 클리어됩니다.
진행 상황(레벨 잠금 해제, 별 획득, 코인)은 `PlayerPrefs`에 저장되어 재실행 시 유지됩니다.

## 기술 스택

- Unity 2023.1.16f1 (Built-in Render Pipeline, 2D)
- `com.unity.feature.2d` (Sprite, Tilemap, Physics2D, Animation)
- TextMeshPro (UI 텍스트)
- `PlayerPrefs` 기반 로컬 세이브 (별도 세이브 파일/서버 없음)
