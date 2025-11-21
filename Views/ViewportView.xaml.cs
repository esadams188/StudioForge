using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StudioForge.Views
{
    public partial class ViewportView : UserControl
    {
        public ViewportView()
        {
            InitializeComponent();
        }
    }

    public class ViewportCanvas : Canvas
    {
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            var skyGradient = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1)
            };
            skyGradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#87CEEB"), 0));
            skyGradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#C8E6F5"), 1));
            dc.DrawRectangle(skyGradient, null, new Rect(0, 0, ActualWidth, ActualHeight));

            DrawGrid(dc);
            DrawPlatform(dc);
        }

        private void DrawGrid(DrawingContext dc)
        {
            double width = ActualWidth;
            double height = ActualHeight;
            double horizon = height * 0.45;
            var gridBrush = (Brush)Application.Current.Resources["Brush.GridLine"];

            for (int i = -10; i <= 10; i++)
            {
                double x = width / 2 + i * 40;
                var start = new Point(x, horizon);
                var end = new Point(x + i * 6, height);
                dc.DrawLine(new Pen(gridBrush, 1), start, end);
            }

            for (int j = 0; j < 12; j++)
            {
                double y = horizon + j * 20;
                dc.DrawLine(new Pen(gridBrush, 1), new Point(0, y), new Point(width, y));
            }
        }

        private void DrawPlatform(DrawingContext dc)
        {
            double width = ActualWidth;
            double height = ActualHeight;
            Point center = new(width / 2, height * 0.6);

            Size topSize = new Size(240, 120);
            Size sideSize = new Size(topSize.Width, 30);

            Point topLeft = new(center.X - topSize.Width / 2, center.Y - topSize.Height / 2);
            Rect topRect = new(topLeft, topSize);
            dc.DrawRoundedRectangle(Brushes.White, new Pen(Brushes.LightGray, 1), topRect, 4, 4);

            Point shadowOffset = new(center.X - topSize.Width / 2 + 15, center.Y - topSize.Height / 2 + sideSize.Height + 5);
            Rect shadowRect = new(shadowOffset, new Size(topSize.Width, sideSize.Height));
            dc.PushOpacity(0.4);
            dc.DrawRectangle(new SolidColorBrush(Color.FromRgb(20, 20, 20)), null, shadowRect);
            dc.Pop();

            Point sideTop = new(center.X - topSize.Width / 2, center.Y + topSize.Height / 2 - 1);
            Rect sideRect = new(sideTop, sideSize);
            dc.DrawRectangle(new SolidColorBrush(Color.FromRgb(210, 210, 210)), null, sideRect);

            DrawDecal(dc, topRect);
        }

        private void DrawDecal(DrawingContext dc, Rect rect)
        {
            var center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            var pen = new Pen(Brushes.Black, 3)
            {
                StartLineCap = PenLineCap.Round,
                EndLineCap = PenLineCap.Round
            };

            double radius = Math.Min(rect.Width, rect.Height) / 3;
            for (int i = 0; i < 8; i++)
            {
                double angle = i * Math.PI / 4;
                Point end = new(center.X + Math.Cos(angle) * radius, center.Y + Math.Sin(angle) * radius);
                dc.DrawLine(pen, center, end);
            }

            dc.DrawEllipse(Brushes.Black, null, center, 18, 18);
            dc.DrawEllipse(Brushes.White, null, center, 10, 10);
        }
    }
}
