## 설계 핵심 요약

이 프로젝트에서는 UI 관련 처리 로직을 다음과 같은 구조로 설계하였습니다:

- **UIButtonBinder<TEnum> 추상 클래스 도입**  
  → `enum` 기반으로 버튼 이름 자동 매핑 및 `onClick` 자동 연결  
  → 유지보수 시 `enum`만 수정하면 UI 동작 확장 가능

- **Hover 이벤트 처리 자동화**  
  → `EventTrigger`를 코드에서 자동 삽입  
  → 별도 스크립트 없이 Hover 효과 적용

- **TitleCanvas ↔ SettingsCanvas 구조상 위치 탐색으로 처리**  
  → UI 간 전환 시에도 드래그나 하드코딩 없이 유연하게 연결

## 테스트 시 주의사항

Unity 엔진에서 직접 여실 경우, Asset/Scenes/ 내의 Title 씬으로 실행해 주세요.

게임을 시작하고 철창 앞에서 적을 피해 지나가려면 철창 왼쪽 벽을 밀어 지나갈 수 있습니다.
