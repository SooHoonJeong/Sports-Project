# Sports-Project

Unity 기반 스포츠 프로젝트. `tennisV1` 브랜치에서 테니스 관련 기능을 개발 중.

## 요구 사항

| 항목 | 버전 |
|------|------|
| Unity Editor | **6000.3.17f1** (Unity Hub에서 정확히 이 버전 설치) |
| Render Pipeline | URP (Universal Render Pipeline) |
| Git | 최신 버전 |
| **Git LFS** | 필수 (아래 참고) |

## 처음 세팅하는 방법

### 1. Git LFS 설치 (최초 1회, 컴퓨터당)

이 프로젝트는 이미지·모델·오디오 등 대용량 에셋을 [Git LFS](https://git-lfs.com/)로 관리합니다. **설치 안 하면 해당 파일들이 정상적으로 받아지지 않고 `git status`에서 이상한 변경사항이 뜨는 등 문제가 생깁니다.**

```bash
brew install git-lfs
git lfs install
```

### 2. 클론

```bash
git clone https://github.com/SooHoonJeong/Sports-Project.git
cd Sports-Project
git checkout tennisV1   # 실제 개발 브랜치
```

이미 클론했는데 Git LFS를 나중에 설치했다면:

```bash
git lfs install
git checkout -- .
```

### 3. Unity Hub에서 열기

Unity Hub → Open → 클론한 폴더 선택. Editor 버전이 없다면 Hub가 `6000.3.17f1` 설치를 유도합니다. 다른 버전으로 열면 에셋 재직렬화 등으로 불필요한 diff가 발생할 수 있으니 반드시 동일 버전을 사용하세요.

## 씬 구조

씬 파일이 하나로 뭉쳐 있으면 `git lfs lock` 특성상 한 번에 한 사람만 작업할 수 있어 병목이 생깁니다. 그래서 영역별로 씬을 나누고, 실행 시 `Bootstrap` 씬이 나머지를 Additive로 로드합니다.

| 씬 | 역할 |
|---|---|
| `Bootstrap.unity` | 진입 씬. Build Settings의 첫 씬. `SceneBootstrapper`가 아래 씬들을 Additive로 로드 |
| `Court.unity` | 코트/환경, Main Camera |
| `Lighting.unity` | 조명(Directional Light), 포스트프로세싱(Global Volume) |
| `UI.unity` | HUD/메뉴 (현재 빈 씬, 콘텐츠 추가 예정) |

Unity Editor에서 개별 씬을 열어 작업해도 되고, `Bootstrap.unity`를 Play 하면 전부 합쳐진 상태로 실행됩니다. 플레이어/공/네트 등 동적 오브젝트는 씬이 아니라 `Assets/Prefabs/` 하위 프리팹으로 관리합니다 (씬 잠금 없이 여러 명이 각자 다른 프리팹을 작업할 수 있도록).

## 협업 규칙: 씬/프리팹 동시 편집 충돌 방지

`.unity`(씬), `.prefab` 파일은 Git이 자동 병합할 수 없는 포맷입니다. 두 사람이 같은 파일을 동시에 수정하면 한쪽 작업이 통째로 날아갈 수 있으므로, **편집 전 잠금 → 편집 후 해제** 규칙을 지킵니다.

```bash
# 편집 시작 전
git lfs lock Assets/Scenes/Court.unity

# 현재 잠긴 파일 확인
git lfs locks

# 편집 끝나고 커밋/푸시 후
git lfs unlock Assets/Scenes/Court.unity
```

다른 사람이 잠근 파일은 `git lfs locks`로 미리 확인하고 편집을 피하세요.

## 브랜치

- `main` — 기본/안정 브랜치
- `tennisV1` — 테니스 기능 개발 브랜치 (실제 작업은 여기서 진행)

## 주의 사항

- Unity 프로젝트 생성 시 기본으로 딸려오는 **Unity Version Control(Plastic SCM)** 은 이 프로젝트에서 사용하지 않습니다. Unity Editor의 `Project Settings > Version Control`에서 실수로 활성화하지 마세요 — Git과 충돌합니다.
- `raw` 상태의 대용량 바이너리 파일(`.png`, `.jpg`, `.fbx`, `.wav`, `.mp4` 등)은 자동으로 LFS 추적됩니다 (`.gitattributes` 참고). 새 확장자를 추가해야 하면 `git lfs track "*.확장자"` 사용.
