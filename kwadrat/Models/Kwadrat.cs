using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace kwadrat.Models
{
    internal class Kwadrat
    {
        protected System.Windows.Shapes.Polygon poly;
        protected System.Windows.Point topleft;
        protected System.Windows.Point topright;
        protected System.Windows.Point bottomleft;
        protected System.Windows.Point bottomright;
        protected SolidColorBrush color;
        protected double opacity;
        protected double opacityOldValue;
        protected double bok;
        protected int rotation;
        protected double cornerOffsetPercent;
        protected int cornerStepDirection;
        protected int cornerRotation;
        protected TextBox txtPole;
        protected TextBox txtObwod;
        protected System.Windows.Controls.Label lblTest;
        protected Slider opacitySlider;
        protected double topWidth;
        protected double bottomWidth;

        public Kwadrat(
                System.Windows.Shapes.Polygon poly,
                double bok,
                SolidColorBrush color,
                double opacity,
                TextBox pole,
                TextBox obwod,
                System.Windows.Controls.Label lblTest,
                Slider slider

               )
        {
            this.poly = poly;
            this.bok = bok;
            this.color = color;
            this.opacity = opacity;
            this.rotation = 0;
            this.cornerOffsetPercent = 0;
            this.cornerStepDirection = 1;
            this.cornerRotation = 1;
            this.txtObwod = obwod;
            this.txtPole = pole;
            this.lblTest = lblTest;
            this.opacitySlider = slider;
        }

        public void draw()
        {
            this.poly.Points.Clear();
            this.poly.Stroke = color;
            this.poly.Fill = color;
            this.poly.Opacity = opacity;
            this.poly.Width = bok;
            this.poly.Height = bok;
            this.drawSize(this.bok);
        }

        public void setSize(double bok)
        {
            this.bok = bok;
            this.topWidth = bok;
            this.bottomWidth = bok;

            this.poly.Width = bok;
            this.poly.Height = bok;
        }

        public void setOpacity(double opacity)
        {
            this.opacity = opacity;
            this.opacitySlider.Value = opacity * 100;
        }

        public void setColor(SolidColorBrush color)
        {
            this.color = color;
        }

        public void show(bool show)
        {
            if(!show)
            {
                this.opacityOldValue = this.opacity;
            }
            this.poly.Opacity = show ? this.opacityOldValue : 0;
        }

        public void rotateRight(int rotation)
        {
            this.rotation += rotation;
            if(this.rotation >= 360)
            {
                this.rotation = 0;
            }

            lblTest.Content = this.rotation.ToString();
            RotateTransform rt = new RotateTransform(this.rotation);
            poly.RenderTransform = rt;
        }

        public void drawSize(double bok)
        {
            double cornerOffset = this.bok * (this.cornerOffsetPercent / 100) / 2;

            if (this.cornerRotation > 0)
            {
                this.topleft = new System.Windows.Point(cornerOffset, 0);
                this.topright = new System.Windows.Point(bok - cornerOffset, 0);
                this.bottomleft = new System.Windows.Point(bok, bok);
                this.bottomright = new System.Windows.Point(0, bok);

                this.topWidth = bok - (2 * cornerOffset);
                this.bottomWidth = bok;
            }
            else if (this.cornerRotation < 0)
            {
                this.topright = new System.Windows.Point(0, 0);
                this.topleft = new System.Windows.Point(bok, 0);
                this.bottomright = new System.Windows.Point(bok - cornerOffset, bok);
                this.bottomleft = new System.Windows.Point(cornerOffset, bok);

                this.topWidth = bok;
                this.bottomWidth = bok - (2 * cornerOffset);
            }


            PointCollection polyPoints = new PointCollection();
            polyPoints.Add(this.topleft);
            polyPoints.Add(this.topright);
            polyPoints.Add(this.bottomleft);
            polyPoints.Add(this.bottomright);

            this.poly.Points = polyPoints;
        }

        public void cornerTransform(int percent)
        {
            this.poly.Points.Clear();

            this.cornerOffsetPercent += percent * this.cornerStepDirection;

            if(this.cornerOffsetPercent >= 100)
            {
                this.cornerStepDirection = -1;
                this.cornerOffsetPercent = 100;
            }

            if(this.cornerOffsetPercent <= 0)
            {
                this.cornerStepDirection = 1;
                this.cornerRotation = -this.cornerRotation;
                this.cornerOffsetPercent = 0;
            }

            lblTest.Content = this.cornerOffsetPercent.ToString();

            this.drawSize(this.bok);
        }

        public void showPole()
        {
            this.txtPole.Text = this.getPole().ToString();
        }

        public double getPole()
        {
            double pole = ((this.topWidth + this.bottomWidth) * this.bok) / 2;
            return pole;
        }

        public void showObwod()
        {
            txtObwod.Text = this.getObwod().ToString();
        }

        public double getObwod()
        {
            double przyporostokatna = this.bok;
            double podstawaTrojkata = Math.Abs(this.topWidth - this.bottomWidth);
            double ramie = Math.Sqrt(Math.Pow(podstawaTrojkata, 2) + Math.Pow(przyporostokatna, 2));
            double obwod = this.topWidth + this.bottomWidth + ramie * 2;

            return obwod;
        }

        public void showParams()
        {
            this.showObwod();
            this.showPole();
        }
    }
}
