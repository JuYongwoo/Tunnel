# 프로젝트 구조 및 핵심 설계 요약

---

## 스크립트 구조

```
├── Chapter                    # 챕터별 고유 이벤트 관리
│   ├── Chapter1
│   ├── Chapter2
│   ├── Chapter3
│   └── GameOver
│
├── Enemy                      # 적 행동 스크립트
│   ├── Following
│   ├── Patrolling
│   └── Zombie
│
├── Items                      # 아이템 관련 (Item 상속 구조)
│   ├── Door
│   ├── Item
│   ├── Key
│   └── MovingWall
│
├── Manager                    # 툴 및 시스템 관리 매니저
│   ├── ButtonSystem
│   │   └── UIButtonBinder
│   ├── DoorKeySystem
│   │   ├── DoorKeyData
│   │   └── DoorKeyManager
│   ├── TriggerSystem
│   │   ├── TriggerEvent
│   │   └── TriggerEventManager
│   ├── ManagerObject
│   ├── ResourceManager
│   ├── ScenManagerJ
│   └── SoundManager
│
├── Player                     # 플레이어 관련 스크립트
│   ├── Camera
│   ├── PlayerInteractor
│   └── PlayerMove
│
└── UI                         # UI 캔버스 및 컨트롤러
    ├── InGameUI
    ├── SettingsUI
    └── TitleUI
```

---

## 사용 기술 및 설계 원칙

### **Trigger - Event 시스템 도입**
- **ScriptableObject(SO)** 기반 이벤트 트리거 시스템
- 인스펙터 창에서 직관적으로 이벤트 정의 가능
- 유지보수 용이 & 디자이너와 협업 최적화
- 문-열쇠 데이터 쌍도 SO를 활용하여 구성

---

### **UI 버튼 이벤트 처리 자동화**
- `ButtonBinder`로 **enum ↔ Button** 매핑
- GameObject 이름 참조 X → **enum 기반 참조로 안정성 강화**
- 오타나 참조 오류 발생률 감소

---

### **event / Action 기반 클래스 역할 분리**
- **직접 참조 없이** Action으로 통신
- 높은 응집도, 낮은 결합도 유지
- **스파게티 코드 방지**

---

### **Manager Object 기반 싱글톤 매니저 활용**
- 공통 매니저들(`SoundManager`, `SceneManager` 등)을 **ManagerObject** 기준으로 통합 관리
- 직접 변수 참조가 아닌, **이벤트 및 Action** 기반으로 연결
- 유지보수성과 구조적 확장성 확보
