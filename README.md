# (C# 코딩) 그림판 (Simple Paint)
## 개요
- C# 프로그래밍학습
- 1줄소개: 사용자 마우스 입력을 통해 간단한 도형과 그림을 그리는 프로그램
- 사용한플랫폼: 
  -C#, .NET Windows Forms, Visual Studio, GitHub
- 사용한컨트롤: 
  - Label, Combobox, ComboBox.SelectedIndex, TrackBar, TrackBar.Value, PictureBox, PictureBox.Image, Button, Graphics, Pen, Bitmap 
- 사용한 기술과 구현한 기능:
  - VisualStudio를 이용하여 기본적인 UI 디자인
  - pictureBox 컨트롤과 비트맵을 이용하여 그림을 그릴 수 있는 캔버스 구현
  - 마우스 입력을 통해 도형 그리기 기능 구현
  - 마우스 입력 시작부분과 끝부분을 이용하여 선, 사각형, 원 그리기 기능 구현
  - 색상 선택 및 펜 크기 조절 기능 구현
  - 도형 그리기 기능 구현 (선, 사각형, 원)
  - 그려진 그림을 다양한 형식의 이미지 파일로 저장하는 기능 구현 (SaveFileDialog 사용)

## 실행 화면 (과제1)
- 코드의 실행 스크린샷과 구현 내용 설명
![실행화면](img/subject1.png)
- 구현한 내용 (위 그림 참조)
  - UI 구성 : Label(앱 이름 표시), Combobox(색상 선택), TrackBar(펜 크기 조절), PictureBox(그림 표시)

## 실행 화면 (과제2)
- 코드의 실행 스크린샷과 구현 내용 설명
![실행화면](img/subject2.png)
- 구현한 내용 (위 그림 참조)
  - 마우스 입력을 통해 선, 사각형, 원 그리기 기능 구현
  - 선택된 색상과 펜 크기에 따라 도형이 그려짐
  - 마우스 드래그로 도형의 크기 조절 가능
  - 콤보박스를 이용하여 색상 선택
  - 트랙바로 펜 크기 조절 가능

## 실행 화면 (과제3)
- 코드의 실행 스크린샷과 구현 내용 설명
![실행화면](img/subject3.png)
- 구현한 내용 (위 그림 참조)
  - 그려진 그림을 이미지 파일로 저장 가능
  - 기본 저장 이름은 "내 그림.png"로 설정
  - SaveFileDialog를 이용하여 파일 저장 위치와 이름 지정
  - .png, .jpg, .bmp 세 가지 형식으로 저장 가능

## 실행 화면 (과제4)
- 코드의 실행 스크린샷과 구현 내용 설명
![실행화면](img/subject4.png)

  
