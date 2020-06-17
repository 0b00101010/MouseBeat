# MouseBeat

## Developer
Develop : 전세훈([@Komorio](https://github.com/Komorio)) <br>
Design : 정용우([@Reuse1020](https://github.com/Reuse1020)) <br>

## Editor
UnityVersion : 2020.1.0b12

# Designer Values

## Function
> Hierarchy/InGameManager 오브젝트 NodeGeneartor 스크립트에 Button Tap에서 사용 가능


* Normal Node Generate

```
랜덤한 위치에 Normal Node 하나를 생성함
```

* Long Node Generate

```
랜덤한 위치에 Long Node 하나를 생성함
```

## Node 

Normal Node

* Normal Node / 기본 노드
>03.Prefabs/Nodes/NormalNode 아래 NormalNode Script의 Values 탭에서 수정 가능

```
Score : 처리 성공시 획득할 점수

Redubed HP : 처리 실패시 감소할 체력

Default Speed : 노트가 이동하는 속도 
ex) Default Speed가 1일 경우 1초에 걸쳐 이동

Ease Type : 노트의 이동 방식을 설정

-- Judge --

노트 진행도 
Start Value : 0 
Arrive Value : 1

Judge Perfect : Perfect 판정 값 
Perfect 값과 노트의 진행도가 같을 경우 Perfect 처리

Judge Great : Greate 판정 값
노트의 진행도가 Perfect 값과 Great 값 사이일 경우 Great 처리

Judge Good : Good 판정 값
노트의 진행도가 Great와 Good 값 사이일 경우 Good 처리
노트의 진행도가 Good보다 낮을 경우 Bad 처리

노트를 아예 처리 하지 않을 경우에는 Miss로 처리

-- Failed Inturaction Effect --

Fade Duration : Fade out이 진행 될 시간
Fall Down Duration : 노트가 아래로 떨어지는 시간 
Fall Down Ease : 노트 낙하 방식
```

Long Node 

* Long Node / 롱 노드
>03.Prefabs/Nodes/LongNode 아래 LongNode Script의 Values 탭에서 수정 가능

```
Score : 처리 성공시 Tail 이 끝날 때 까지 프레임 당 획득할 점수

Redubed HP : 처리 실패시 감소할 체력

Default Speed : 노트가 이동하는 속도 
ex) Default Speed가 1일 경우 1초에 걸쳐 이동
ex) Head도 1초, Tail도 1초 

Ease Type : 노트의 이동 방식을 설정

-- Judge --

노트 진행도 처음으로 상효 작용한 Head의 위치 한정으로만 계산함

Start Value : 0 
Arrive Value : 1

Judge Perfect : Perfect 판정 값 
Perfect 값과 노트의 진행도가 같을 경우 Perfect 처리

Judge Great : Greate 판정 값
노트의 진행도가 Perfect 값과 Great 값 사이일 경우 Great 처리

Judge Good : Good 판정 값
노트의 진행도가 Great와 Good 값 사이일 경우 Good 처리
노트의 진행도가 Good보다 낮을 경우 Bad 처리

노트를 아예 처리 하지 않을 경우에는 Miss로 처리
```

---

## Effect

* Judge Effect / 판정시 화면 중앙에 출력되는 판정 이미지
> Hierarchy/InGameManager 오브젝트의 ScoreManager 스크립트에 있는 Judge Effect 탭에서 수정 가능 
```
Size Up Value : Judge Image가 얼마나 커질지 설정하는 값 
ex) Size Up Value가 1.5 일 경우 1.5배 만큼 커짐

Duration : 크기 변경에 소요 될 시간
ex) Duration 값이 1.5 일 경우 1.5초에 걸쳐 커짐

Ease Type : 크기 변경에 방식을 설정
```

* Node Effect / Perfect로 상호작용 할 경우 실행 될 이펙트
>03.Prefabs/Effect/Effect 아래 NodeEffect 스크립트의 Values 탭에서 수정 가능
```
Left Turn Angle : 왼쪽으로 회전하는 오브젝트 회전 값
Right Turn Angle : 오른쪽으로 회전하는 오브젝트 회전 값
Size Up Value : 증가할 크기
```