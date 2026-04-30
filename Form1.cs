using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
    

namespace SimplePaint
{
    public partial class Form1 : Form
    {
        enum ToolType { Line, Rectangle, Circle }
        private double zoomRatio = 1.0;
        private Bitmap canvasBitmap;
        private Graphics canvasGraphics;
        private bool isDrawing = false;
        private Point startPoint;
        private Point endPoint;
        private ToolType currentTool = ToolType.Line;
        private Color currentColor = Color.Black;
        private int currentLineWidth = 2;
        public Form1()
        {
            InitializeComponent();

            //캔버스 초기화
            canvasBitmap = new Bitmap(picCanvas.Width, picCanvas.Height);
            canvasGraphics = Graphics.FromImage(canvasBitmap);
            canvasGraphics.Clear(Color.White);
            picCanvas.Image = canvasBitmap;
            picCanvas.MouseDown += picCanvas_MouseDown;
            picCanvas.MouseMove += picCanvas_MouseMove;
            picCanvas.MouseUp += picCanvas_MouseUp;
            picCanvas.Paint += picCanvas_Paint;
            btnLine.Click += btnLine_Click;
            btnRectangle.Click += btnRectangle_Click;
            btnCircle.Click += btnCircle_Click;
            cmbColor.SelectedIndexChanged += cmbColor_SelectedIndexChanged;
            cmbColor.SelectedIndex = 0; //기본 색상 검정

            trbLineWidth.Minimum = 1;
            trbLineWidth.Maximum = 10;
            trbLineWidth.Value = 2;
            trbLineWidth.ValueChanged += trbLineWidth_ValueChanged;



        }
        private void btnLine_Click(object sender, EventArgs e)
        {
            currentTool = ToolType.Line;
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            currentTool = ToolType.Rectangle;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            currentTool = ToolType.Circle;
        }

        private void cmbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbColor.SelectedIndex)
            {
                case 0:
                    currentColor = Color.Black; break;
                case 1:
                    currentColor = Color.Red; break;
                case 2:
                    currentColor = Color.Blue; break;
                case 3:
                    currentColor = Color.Green; break;
                default:
                    currentColor = Color.Black; break;
            }
        }

        private void trbLineWidth_ValueChanged(object sender, EventArgs e)
        {
            currentLineWidth = trbLineWidth.Value;
        }

        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            startPoint = e.Location;
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;
            isDrawing = false;
            endPoint = e.Location;
            using (Pen pen = new Pen(currentColor, currentLineWidth))
            {
                DrawShape(canvasGraphics, pen, startPoint, endPoint);
            }
            picCanvas.Invalidate();
        }

        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;
            endPoint = e.Location;
            picCanvas.Invalidate();
        }

        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (!isDrawing) return;
            using (Pen previewPen = new Pen(currentColor, currentLineWidth))
            {
                previewPen.DashStyle = DashStyle.Dash;
                DrawShape(e.Graphics, previewPen, startPoint, endPoint);
            }
        }
        private void DrawShape(Graphics g, Pen pen, Point p1, Point p2)
        {
            switch (currentTool)
            {
                case ToolType.Line:
                    g.DrawLine(pen, p1, p2); break;
                case ToolType.Rectangle:
                    g.DrawRectangle(pen, GetRectangle(p1, p2)); break;
                case ToolType.Circle:
                    g.DrawEllipse(pen, GetRectangle(p1, p2)); break;
            }
        }
        private Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            // 1. SaveFileDialog 생성 및 설정
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // PDF 요구사항: png, jpg, bmp 3가지 포맷 설정
            saveFileDialog.Filter = "PNG Image|*.png|JPeg Image|*.jpg|Bitmap Image|*.bmp";
            saveFileDialog.Title = "이미지 파일로 저장";
            saveFileDialog.FileName = "내그림"; // 초기 파일명 설정

            // 2. 사용자가 '저장' 버튼을 눌렀을 때만 실행
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 선택한 파일 경로 및 확장자 확인
                    string filePath = saveFileDialog.FileName;
                    string extension = System.IO.Path.GetExtension(filePath).ToLower();

                    // 3. 확장자에 따른 이미지 포맷 결정
                    System.Drawing.Imaging.ImageFormat format;
                    switch (extension)
                    {
                        case ".jpg":
                        case ".jpeg":
                            format = System.Drawing.Imaging.ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = System.Drawing.Imaging.ImageFormat.Bmp;
                            break;
                        case ".png":
                        default:
                            format = System.Drawing.Imaging.ImageFormat.Png;
                            break;
                    }

                    // 4. 비트맵(canvasBitmap)을 파일로 저장
                    // 화면의 PictureBox가 아닌, 데이터가 담긴 Bitmap 객체를 직접 저장합니다.
                    canvasBitmap.Save(filePath, format);

                    MessageBox.Show("이미지가 성공적으로 저장되었습니다!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("저장 중 오류가 발생했습니다: " + ex.Message);
                }
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); openFileDialog.Filter =
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 1. 원본 이미지 로드
                Image originalImage = Image.FromFile(openFileDialog.FileName);

                // 2. 이미지 크기에 맞춰 PictureBox 및 비트맵 크기 조정
                picCanvas.Width = originalImage.Width;
                picCanvas.Height = originalImage.Height;

                // 3. 새 비트맵 생성 및 Graphics 초기화
                canvasBitmap = new Bitmap(originalImage.Width, originalImage.Height);
                canvasGraphics = Graphics.FromImage(canvasBitmap);

                // 4. 불러온 이미지를 비트맵에 그리기
                canvasGraphics.DrawImage(originalImage, 0, 0, originalImage.Width, originalImage.Height);

                picCanvas.Image = canvasBitmap;
                originalImage.Dispose();

                // Panel의 AutoScroll 덕분에 이미지가 크면 자동으로 스크롤바가 생깁니다.
            }
        }
        private void ApplyZoom()
        {
            // 비트맵의 원본 크기에 비율을 곱해 PictureBox 크기 변경
            picCanvas.Width = (int)(canvasBitmap.Width * zoomRatio);
            picCanvas.Height = (int)(canvasBitmap.Height * zoomRatio);

            // PictureBox의 SizeMode가 StretchImage여야 이미지가 같이 커집니다.
            picCanvas.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            zoomRatio += 0.1; // 10% 확대
            ApplyZoom();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            if (zoomRatio > 0.2) // 최소 크기 제한
            {
                zoomRatio -= 0.1; // 10% 축소
                ApplyZoom();
            }
        }
    }
}
