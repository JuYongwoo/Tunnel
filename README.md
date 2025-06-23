# Unity 프로젝트 UI 구조 설계 요약

## 1. UI 흐름 다이어그램

```
[TitleCanvas]
   ├── LoadButton      → PlayerPrefs 기반 씬 로딩 (Chapter1 / Chapter2)
   ├── StartButton     → Chapter1 씬으로 이동
   ├── SettingsButton  → SettingsCanvas 활성화
   └── GameExitButton  → Application.Quit()

[SettingsCanvas]
   ├── ResolutionApplyButton → (현재 미구현) 해상도 설정
   ├── ResetButton           → 설정 초기화 로직 자리
   └── BackButton            → TitleCanvas로 복귀
```

※ Canvas 전환은 `GameObject` 이름 기반으로 자동 탐색하여 `SetActive(true/false)`로 처리됩니다.

---

## 2. 설계 핵심 요약

이 프로젝트에서는 UI 관련 처리 로직을 다음과 같은 구조로 설계하였습니다:

- **UIButtonBinder<TEnum> 추상 클래스 도입**  
  → `enum` 기반으로 버튼 이름 자동 매핑 및 `onClick` 자동 연결  
  → 유지보수 시 `enum`만 수정하면 UI 동작 확장 가능

- **Hover 이벤트 처리 자동화**  
  → `EventTrigger`를 코드에서 자동 삽입  
  → 별도 스크립트 없이 Hover 효과 적용 (드래그 없음, 일관 구조 유지)

- **TitleCanvas ↔ SettingsCanvas 구조상 위치 탐색으로 처리**  
  → UI 간 전환 시에도 드래그나 하드코딩 없이 유연하게 연결됨

---

## ⏱️ 추천 사용 기술 태그 (포트폴리오용)

- UnityEngine.UI
- EventTrigger
- Enum-driven UI Mapping
- Dynamic Component Binding
- Canvas-based UI Navigation
- Runtime Scene Management
