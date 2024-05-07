>## 스파르타 던전 배틀 (Text 게임) 만들기 


### 💻프로젝트 소개
---
- C#과 Visual Studio를 활용한 텍스트 콘솔 RPG 게임 입니다.

- 이미지 넣는곳

---
### ⏱️개발 기간
- 2024.04.29 ~ 2024.05.07

### 👯‍♂️참여 인원
- 총 4명

---

### 💯상세 사항

##### *주요기능*


<details><summary> 싱글톤 게임 매니저
</summary>

외부 클래스에서 타클래스에 접근하기 쉽게 구현되었습니다.

<u>ex)Data, UI, Scene, Quest, Player가  GameManager에서 전역 변수로 선언되었습니다.
</u>

[게임 매니저 스크립트 링크](https://github.com/Denba32/Sparta-Dungeon/blob/main/GameManager.cs)   

```
    public sealed class GameManager
    {
        private static GameManager instance = null;
        private static readonly object padLock = new object();

        private bool isLocked = false;
        GameManager()
        {

        }

        public static GameManager Instance
        {
            get
            {
                lock (padLock)
                {
                    if(instance == null)
                    {
                        instance = new GameManager();
                    }
                }
                return instance;
            }
        }
```

</details>


<details><summary> UIManager
</summary>

* UI의 Title 부터 입출력 부분까지를 블록화하여 간편하게 구현하는 클래스 입니다.

* Console.Clear()와 Console.SetPosition, 그리고 For문을 활용하여 다양한 디자인의 UI와 컬러를 출력합니다.

[UIManager 스크립트 링크](https://github.com/Denba32/Sparta-Dungeon/blob/main/UIManager.cs)   

</details>


<details><summary> SceneManager
</summary>



* 콘솔창 화면에 출력되어야 할 모든 내용을 씬 별로 구분하고 이를 각각의 메서드로 구성하고 있는 스크립트입니다.
* UIManager를 적극 활용하여 보다 간편한 Scene을 구성할 수 있습니다.

[씬 매니저 스크립트 링크](https://github.com/Denba32/Sparta-Dungeon/blob/main/SceneManager.cs)   
</details>


<details><summary> 플레이어
</summary>

* 플레이어의 스테이터스 정보를 가지는 [PlayerData]
* 장착중인 무기, 방어구 정보 [Equipment]
* 플레이어의 스킬을 다루는 [SkillController]
* 플레이어가 소지하고 있는  장비를 관리하는 [Inventory]

[플레이어 스크립트 링크](https://github.com/Denba32/Sparta-Dungeon/blob/main/Player.cs)   

</details>



<details><summary> 던전 클래스
</summary>

* 플레이어가 던전에 입장 시, 플레이어의 레벨에 맞게 적을 소환합니다.

* 플레이어와 적 사이 공격에 대한 처리를 도와줍니다.

[던전 스크립트 링크](https://github.com/Denba32/Sparta-Dungeon/blob/main/Dungeon.cs)   

</details>
</details>
</details>

---


### >🧷데모 영상 링크
[데모 영상 유튜브 링크](https://www.youtube.com/watch?v=e-TAc7DStoo)   

---


>### ♻️ 화면 구성

- ##### 게임 시작 화면
![시작화면](https://github.com/Denba32/Sparta-Dungeon/blob/main/Images/%EC%8B%9C%EC%9E%91%ED%99%94%EB%A9%B4.png?raw=true)   

---

- ##### 캐릭터 이름 생성
![캐릭터이름생성](https://github.com/Denba32/Sparta-Dungeon/blob/main/Images/%EC%8B%9C%EC%9E%91%ED%99%94%EB%A9%B4_%EC%9D%B4%EB%A6%84%EC%84%A4%EC%A0%95.png?raw=true)   

---

- ##### 메인 화면
![메인화](https://github.com/Denba32/Sparta-Dungeon/blob/main/Images/%EB%A9%94%EC%9D%B8%EC%94%AC.png?raw=true)   

---

- ##### 스테이터스 화면
![title](https://github.com/Denba32/Sparta-Dungeon/blob/main/Images/%EC%83%81%ED%83%9C%EC%B0%BD.png?raw=true)   

---

 - ##### 인벤토리 화면

![title](https://github.com/Denba32/Sparta-Dungeon/blob/main/Images/%EC%9D%B8%EB%B2%A4%ED%86%A0%EB%A6%AC%EC%B0%BD.png?raw=true)   


---

 - ##### 상점 화면

![title](https://github.com/Denba32/Sparta-Dungeon/blob/main/Images/%EC%83%81%EC%A0%90.png?raw=true)   


---

 - ##### 던전 입장 화면
![title](https://github.com/Denba32/Sparta-Dungeon/blob/main/Images/%EB%8D%98%EC%A0%84%EC%A0%84%ED%88%AC%EC%94%AC.png?raw=true)   


---

 - ##### 스킬 사용 화면
![title](https://github.com/Denba32/Sparta-Dungeon/blob/main/Images/%EC%A0%84%ED%88%AC%EC%8B%9C_%EC%8A%A4%ED%82%AC%EC%82%AC%EC%9A%A9.png?raw=true)   


---

 - ##### 던전 승리 화면
![title](https://github.com/Denba32/Sparta-Dungeon/blob/main/Images/%EC%8A%B9%EB%A6%AC%EC%94%AC.png?raw=true)   


---

 - ##### 던전 사망 화면
![title](https://github.com/Denba32/Sparta-Dungeon/blob/main/Images/%ED%8C%A8%EB%B0%B0%EC%94%AC.png?raw=true)   


---

 - ##### 회복 화면
![title](https://github.com/Denba32/Sparta-Dungeon/blob/main/Images/%ED%9A%8C%EB%B3%B5%EC%94%AC.png?raw=true)   


---


 - ##### 퀘스트 화면
![title](https://github.com/Denba32/Sparta-Dungeon/blob/main/Images/%ED%80%98%EC%8A%A4%ED%8A%B8%EC%94%AC.png?raw=true)   


---






