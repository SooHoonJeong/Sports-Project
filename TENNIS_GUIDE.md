# 테니스 개발 가이드 (tennisV1 전용)

이 문서는 `tennisV1` 브랜치에만 존재합니다. "어떤 작업을 하려면 어떤 파일을 열어야 하는지" 헷갈릴 때 참고하세요. 공통 세팅(Git LFS, 씬 잠금 규칙 등)은 [README.md](README.md)를 먼저 보세요.

## 무슨 작업을 하려면 어떤 파일을 열어야 할까

| 하고 싶은 작업 | 열어야 할 파일 | 비고 |
|---|---|---|
| 코트 크기, 라인, 바닥 재질을 바꾸고 싶다 | `Assets/Scenes/Court.unity` | 코트/환경 담당. 씬 편집 전 `git lfs lock Assets/Scenes/Court.unity` |
| 카메라 각도, 시야(FOV)를 조정하고 싶다 | `Assets/Scenes/Court.unity` | Main Camera가 이 씬 소속 |
| 그림자, 시간대(낮/밤) 느낌, 톤매핑/블룸 같은 화면 보정 | `Assets/Scenes/Lighting.unity` | Directional Light + Global Volume(포스트프로세싱) |
| 스코어보드, 메뉴, 버튼 등 화면에 보이는 UI | `Assets/Scenes/UI.unity` | 현재 빈 씬. Canvas부터 새로 추가하면 됨 |
| 게임 시작 시 씬이 로드되는 순서/방식을 바꾸고 싶다 | `Assets/Scenes/Bootstrap.unity` + `Assets/Scripts/SceneBootstrapper.cs` | 평소엔 건드릴 일 거의 없음 |
| 플레이어 캐릭터, 공, 라켓, 네트처럼 게임 중 생성/이동하는 오브젝트 | `Assets/Prefabs/` 안의 프리팹 | 씬이 아니라 프리팹으로 관리 (동시 작업 시 씬 잠금 없이 각자 다른 프리팹 작업 가능) |
| 캐릭터 이동, 타격 판정, 점수 계산 같은 게임 로직 코드 | `Assets/Scripts/` | 씬과 무관, 일반 C# 스크립트 |

## 실행해서 확인하는 방법

1. `Bootstrap.unity`를 연다.
2. Play 버튼을 누른다 → `SceneBootstrapper`가 `Court`, `Lighting`, `UI`를 자동으로 Additive 로드해서 합쳐진 상태로 실행됨.
3. 개별 씬(`Court.unity` 등)을 직접 열어서 Play 해도 되지만, 그 경우 다른 씬 내용(조명 등)이 없어서 화면이 이상하게 보일 수 있음 — 최종 확인은 항상 `Bootstrap.unity`에서.

## 씬 편집 전 체크리스트

1. `git lfs locks`로 다른 사람이 지금 만지는 중인 씬/프리팹이 있는지 확인
2. 편집할 파일을 `git lfs lock`으로 잠금
3. 작업 → 커밋 → 푸시
4. `git lfs unlock`으로 잠금 해제 (까먹으면 다른 팀원이 그 파일을 못 건드림)

## 새 프리팹을 추가할 때 이름 규칙

`Assets/Prefabs/` 아래에 만들 때는 역할이 드러나는 이름을 사용하세요. 예: `Player.prefab`, `Ball.prefab`, `Racket.prefab`, `Net.prefab`. 세부 변형이 필요하면 `Player_Red.prefab`처럼 접미사를 붙이는 식으로 통일하는 걸 권장합니다.
