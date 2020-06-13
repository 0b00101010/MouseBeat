# MouseBeat

## Developer
Develop : 전세훈([@Komorio](https://github.com/Komorio)) <br>
Design : 정용우

## Editor
UnityVersion : 2020.1.0b12

## Designer Values

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
```

* Judge Effect / 판정시 화면 중앙에 출력되는 판정 이미지
> InGameManager 오브젝트의 ScoreManager 스크립트에 있는 Judge Effect 탭에서 수정 가능 
```
Size Up Value : Judge Image가 얼마나 커질지 설정하는 값 
ex) Size Up Value가 1.5 일 경우 1.5배 만큼 커짐

Duration : 크기 변경에 소요 될 시간
ex) Duration 값이 1.5 일 경우 1.5초에 걸쳐 커짐

Ease Type : 크기 변경에 방식을 설정
```