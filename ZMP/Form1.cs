using MetroSet_UI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.WindowsForms;

namespace ZMP
{
    public partial class Form1 : MetroSetForm
    {
        #region Объвление переменных

        /// <summary>
        /// Фаза движения
        /// </summary>
        /// <param name="stance">Фаза</param>
        /// <remarks>0 - двухопорная</remarks>
        /// <remarks>1 - одноопорная (маховая спереди)</remarks>
        /// <remarks>2 - одноопорная (маховая сзади)</remarks>
        ///
        public int stance;
        
        /// <summary>
        /// Размеры в миллиметрах
        /// </summary>
        /// <param name="a">Стопа</param>
        /// <param name="L">Длина шага</param>
        /// <param name="b">Голень</param>
        /// <param name="c">Бедро</param>
        /// <param name="d">Центр масс от таза</param>
        public int a; 
        public int L; 
        public int b;
        public int c;
        public int d;

        /// <summary>
        /// Масса в граммах
        /// </summary>
        /// <param name="Ma">Лодыжка</param>
        /// <param name="Mk">Колено</param>
        /// <param name="Mp">Таз</param>
        /// <param name="Mu">Туловище</param>
        public int Ma;
        public int Mk;
        public int Mp;
        public int Mu;

        /// <summary>
        /// Скорость и ускорение робота в системе СИ
        /// </summary>
        /// /// <remarks>Это скорость таза</remarks>
        /// <param name="Vx">Скорость по оси x</param>
        /// <param name="Vz">Скорость по оси z</param>
        /// <param name="Wx">Ускорение по оси x</param>
        /// <param name="Wz">Ускорение по оси z</param>
        public int Vx;
        public int Vz;
        public int Wx;
        public int Wz;

        /// <summary>
        /// Углы в градусах
        /// </summary>
        /// <param name="alpha">Туловище</param>
        /// <param name="beta">Опорное бедро или переднее в двухопорной фазе</param>
        /// <param name="gamma">Опорная голень или передняя голень в двухопорной фазе</param>
        /// <param name="delta">Маховое бедро или заднее в двухопорной фазе</param>
        /// <param name="epsilon">Маховая голень или задняя в двухопорной фазе</param>
        public int alpha;
        public int beta;
        public int gamma;
        public int delta;
        public int epsilon;

        /// <summary>
        /// Координаты точек (по правой части рисунка. См. Описание)
        /// </summary>
        /// <param name="x2, z2">Передняя лодыжка</param>
        /// <param name="x5, z5">Задняя лодыжка</param>
        /// <param name="x7, z7">Переднее колено</param>
        /// <param name="x8, z8">Заднее колено</param>
        /// <param name="x9, z9">Таз</param>
        /// <param name="x10, z10">Центр масс туловища</param>
        /// <param name="xCoM, zCoM">Центр масс робота</param>
        /// <param name="xZMP">ZMP</param>
        public float x2, x5, x7, x8, x9, x10;
        public float z2, z5, z7, z8, z9, z10;

        #endregion Объявление переменных

        public Form1()
        {
            InitializeComponent();
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //НАЧАЛЬНЫЕ УСТАНОВКИ

            #region Данные TabPanel
            //Фаза движения - по умолчанию двухопорная
            stance = 0;

            //Размеры (мм)
            // a - стопа, L - шаг, b - голень, с - бедро, d - центр масс
               a = 100;   L = 50;  b = 150;    c = 150;   d = 100;
            // в TextBox
            aTextBox.Text = a.ToString();
            LTextBox.Text = L.ToString();
            bTextBox.Text = b.ToString();
            cTextBox.Text = c.ToString();
            dTextBox.Text = d.ToString();
            // в TrackBar Max
            aTrackBar.Maximum = 2 * a;
            LTrackBar.Maximum = 2 * L;
            bTrackBar.Maximum = 2 * b;
            cTrackBar.Maximum = 2 * c;
            dTrackBar.Maximum = 2 * d;
            // в TrackBar Value
            aTrackBar.Value = a;
            LTrackBar.Value = L;
            bTrackBar.Value = b;
            cTrackBar.Value = c;
            dTrackBar.Value = d;

            //Массы (г)
            // Ma - лодыжка, Mk - колено, Mp - бедро, Mu - туловище
               Ma = 100;     Mk = 100;    Mp = 100;   Mu = 100;
            // в TextBox
            MaTextBox.Text = Ma.ToString();
            MkTextBox.Text = Mk.ToString();
            MpTextBox.Text = Mp.ToString();
            MuTextBox.Text = Mu.ToString();
            // в TrackBar Max
            MaTrackBar.Maximum = 2 * Ma;
            MkTrackBar.Maximum = 2 * Mk;
            MpTrackBar.Maximum = 2 * Mp;
            MuTrackBar.Maximum = 2 * Mu;
            // в TrackBar Value
            MaTrackBar.Value = Ma;
            MkTrackBar.Value = Mk;
            MpTrackBar.Value = Mp;
            MuTrackBar.Value = Mu;

            //Скорости и ускорения в проекциях на оси x, z (СИ)
            //Ось x слева направо, ось z снизу вверх. 
            //Начало осей в лодыжке опорной ноги или передней в двухопорной фазе
            Vx = 1; Vz = 1; Wx = 1; Wz = 1;
            //в TextBox
            VxTextBox.Text = Vx.ToString();
            VzTextBox.Text = Vz.ToString();
            WxTextBox.Text = Wx.ToString();
            WzTextBox.Text = Wz.ToString();
            // в TrackBar Max
            VxTrackBar.Maximum = 2 * Vx;
            VzTrackBar.Maximum = 2 * Vz;
            WxTrackBar.Maximum = 2 * Wx;
            WzTrackBar.Maximum = 2 * Wz;
            // в TrackBar Value
            VxTrackBar.Value = Vx;
            VzTrackBar.Value = Vz;
            WxTrackBar.Value = Wx;
            WzTrackBar.Value = Wz;

            //Углы
            alpha = 0; beta = 0; gamma = 0; delta = 0; epsilon = 0;
            //в TextBox
            alphaTextBox.Text = alpha.ToString();
            betaTextBox.Text = beta.ToString();
            gammaTextBox.Text = gamma.ToString();
            deltaTextBox.Text = delta.ToString();
            epsilonTextBox.Text = epsilon.ToString();
            // в TrackBar Max
            alphaTrackBar.Maximum = 90;
            betaTrackBar.Maximum = 90;
            gammaTrackBar.Maximum = 90;
            deltaTrackBar.Maximum = 90;
            epsilonTrackBar.Maximum = 90;
            // в TrackBar Value
            alphaTrackBar.Value = alpha;
            betaTrackBar.Value = beta;
            gammaTrackBar.Value = gamma;
            deltaTrackBar.Value = delta;
            epsilonTrackBar.Value = epsilon;

            #endregion Данные TabPanel

            #region Координаты точек для графика
            
            if (stance == 0)
            {
               
            }
            else if (stance == 1)
            {
                
            }
            else
            {
               
            }

            #endregion Координаты точек для графика

            #region Графика OxyPlot

            //  Графика в OxyPlot
            // Стопа. Опорная или передняя в двухопорной фазе
            var опорнаяСтопа = new OxyPlot.Series.LineSeries()
            {
                Title = "Опорная стопа",
                Color = OxyPlot.OxyColors.Red,
                StrokeThickness = 1,
                MarkerSize = 2,
                MarkerType = OxyPlot.MarkerType.Square
            };

            var опорнаяНога = new OxyPlot.Series.LineSeries()
            {
                Title = "Опорная нога",
                Color = OxyPlot.OxyColors.Blue,
                StrokeThickness = 1,
                MarkerSize = 2,
                MarkerType = OxyPlot.MarkerType.Square
            };

            var маховаяНога = new OxyPlot.Series.LineSeries()
            {
                Title = "Маховая нога",
                Color = OxyPlot.OxyColors.Green,
                StrokeThickness = 1,
                MarkerSize = 2,
                MarkerType = OxyPlot.MarkerType.Square
            };

            var маховаяСтопа = new OxyPlot.Series.LineSeries()
            {
                Title = "Маховая стопа",
                Color = OxyPlot.OxyColors.Orange,
                StrokeThickness = 1,
                MarkerSize = 2,
                MarkerType = OxyPlot.MarkerType.Square
            };

            var туловище = new OxyPlot.Series.LineSeries()
            {
                Title = "Туловище",
                Color = OxyPlot.OxyColors.Indigo,
                StrokeThickness = 1,
                MarkerSize = 2,
                MarkerType = OxyPlot.MarkerType.Square
            };

            var центрМасс = new OxyPlot.Series.LineSeries()
            {
                Title = "Центр масс",
                Color = OxyPlot.OxyColors.Brown,
                StrokeThickness = 1,
                MarkerSize = 4,
                MarkerType = OxyPlot.MarkerType.Square
            };

            var точкаZMP = new OxyPlot.Series.LineSeries()
            {
                Title = "ZMP",
                Color = OxyPlot.OxyColors.Black,
                StrokeThickness = 1,
                MarkerSize = 4,
                MarkerType = OxyPlot.MarkerType.Diamond
            };

            опорнаяСтопа.Points.Add(new OxyPlot.DataPoint(0, 0));
            опорнаяСтопа.Points.Add(new OxyPlot.DataPoint(a/2, 0));
            опорнаяСтопа.Points.Add(new OxyPlot.DataPoint(-a/2, 0));

            опорнаяНога.Points.Add(new OxyPlot.DataPoint(0, 0));
            опорнаяНога.Points.Add(new OxyPlot.DataPoint(-10, 50));
            опорнаяНога.Points.Add(new OxyPlot.DataPoint(-30, 100));

            маховаяНога.Points.Add(new OxyPlot.DataPoint(-30, 100));
            маховаяНога.Points.Add(new OxyPlot.DataPoint(-40, 50));
            маховаяНога.Points.Add(new OxyPlot.DataPoint(-80, 0));

            маховаяСтопа.Points.Add(new OxyPlot.DataPoint(-80, 0));
            маховаяСтопа.Points.Add(new OxyPlot.DataPoint(-80 + a / 2, 0));
            маховаяСтопа.Points.Add(new OxyPlot.DataPoint(-80 - a / 2, 0));

            туловище.Points.Add(new OxyPlot.DataPoint(-30, 100));
            туловище.Points.Add(new OxyPlot.DataPoint(-20, 200));

            центрМасс.Points.Add(new OxyPlot.DataPoint(-10, 170));

            точкаZMP.Points.Add(new OxyPlot.DataPoint(3, 0));


            var model = new OxyPlot.PlotModel
            {
                Title = $"Робот"
            };
            model.Series.Add(опорнаяСтопа);
            model.Series.Add(опорнаяНога);
            model.Series.Add(маховаяНога);
            model.Series.Add(маховаяСтопа);
            model.Series.Add(туловище);
            model.Series.Add(центрМасс);
            model.Series.Add(точкаZMP);

            pv.Model = model;

            #endregion Графика OxyPlot
        }

        #region Обработка контролов TabPanel

        private void stanceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            stance = stanceComboBox.SelectedIndex;
            if (stance == 0)
            {
                epsilonAttentionLabel.Text = "*В двухопорной фазе зависит от остальных углов и длины шага";
                epsilonLabel.Text = "Эпсилон*";
            }
            else
            {
                epsilonAttentionLabel.Text = "";
                epsilonLabel.Text = "Эпсилон";
            }
        }

        private void aTrackBar_Scroll(object sender)
        {
            aTextBox.Text = aTrackBar.Value.ToString();
        }

        private void LTrackBar_Scroll(object sender)
        {
            LTextBox.Text = LTrackBar.Value.ToString();
        }

        private void bTrackBar_Scroll(object sender)
        {
            bTextBox.Text = bTrackBar.Value.ToString();
        }

        private void cTrackBar_Scroll(object sender)
        {
            cTextBox.Text = cTrackBar.Value.ToString();
        }

        private void dTrackBar_Scroll(object sender)
        {
            dTextBox.Text = dTrackBar.Value.ToString();
        }

        private void MaTrackBar_Scroll(object sender)
        {
            MaTextBox.Text = MaTrackBar.Value.ToString();
        }

        private void MkTrackBar_Scroll(object sender)
        {
            MkTextBox.Text = MkTrackBar.Value.ToString();
        }

        private void MpTrackBar_Scroll(object sender)
        {
            MpTextBox.Text = MpTrackBar.Value.ToString();
        }

        private void MuTrackBar_Scroll(object sender)
        {
            MuTextBox.Text = MuTrackBar.Value.ToString();
        }

        private void VxTrackBar_Scroll(object sender)
        {
            VxTextBox.Text = VxTrackBar.Value.ToString();
        }

        private void VzTrackBar_Scroll(object sender)
        {
            VzTextBox.Text = VzTrackBar.Value.ToString();
        }

        private void WxTrackBar_Scroll(object sender)
        {
            WxTextBox.Text = WxTrackBar.Value.ToString();
        }

        private void WzTrackBar_Scroll(object sender)
        {
            WzTextBox.Text = WzTrackBar.Value.ToString();
        }

        private void alphaTrackBar_Scroll(object sender)
        {
            alphaTextBox.Text = alphaTrackBar.Value.ToString();
        }

        private void betaTrackBar_Scroll(object sender)
        {
            betaTextBox.Text = betaTrackBar.Value.ToString();
        }

        private void gammaTrackBar_Scroll(object sender)
        {
            gammaTextBox.Text = gammaTrackBar.Value.ToString();
        }

        private void deltaTrackBar_Scroll(object sender)
        {
            deltaTextBox.Text = deltaTrackBar.Value.ToString();
        }

        private void epsilonTrackBar_Scroll(object sender)
        {
            epsilonTextBox.Text = epsilonTrackBar.Value.ToString();
        }

        private void metroSetSetTabPage1_MouseMove(object sender, MouseEventArgs e)
        {
            labelAbove.Text = "Размеры";
            
            pv.Refresh();
        }

        private void metroSetSetTabPage2_MouseMove(object sender, MouseEventArgs e)
        {
            labelAbove.Text = "Массы";
            pv.Refresh();
        }

        private void metroSetSetTabPage3_MouseMove(object sender, MouseEventArgs e)
        {
            labelAbove.Text = "Скорость и ускорение";
            pv.Refresh();
        }

        private void metroSetSetTabPage4_MouseMove(object sender, MouseEventArgs e)
        {
            labelAbove.Text = "Углы";
            pv.Refresh();
        }

        private void metroSetSetTabPage5_MouseMove(object sender, MouseEventArgs e)
        {
            labelAbove.Text = "ZMP";
            pv.Refresh();
            
        }

        #endregion Обработка контролов TabPanel
    }
}
