using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;

namespace AOISystem.Utilities.Forms
{
    public class LedLight : Panel
    {
        private bool active = false;
        public bool Active
        {
            get { return active; }
            set
            {
                this.Invalidate();
                active = value;
            }
        }
        public Color ActiveColor
        {
            get { return activeColor; }
            set { activeColor = value; }
        }
        private Color activeColor = Color.Lime;
        public Color InActiveColor
        {
            get { return inActiveColor; }
            set { inActiveColor = value; }
        }
        private Color inActiveColor = Color.Red;
        private ShapeContainer shapeContainer;
        private OvalShape ovalShape;
        public LedLight()
        {
            this.Size = new Size(27, 27);
            shapeContainer = new ShapeContainer();
            ovalShape = new OvalShape();

            shapeContainer.Location = new System.Drawing.Point(0, 0);
            shapeContainer.Margin = new Padding(0);
            shapeContainer.Size = this.Size;
            shapeContainer.TabIndex = 0;
            shapeContainer.TabStop = false;

            ovalShape.BorderStyle = DashStyle.Solid;
            ovalShape.BorderColor = this.BackColor;
            ovalShape.FillGradientStyle = FillGradientStyle.Central;
            ovalShape.FillStyle = FillStyle.Solid;
            ovalShape.Location = new Point(0, 0);
            ovalShape.Name = "ovalShape1";
            ovalShape.Size = new Size(this.Size.Width - 2, this.Size.Height - 2);

            this.Controls.Add(shapeContainer);
            shapeContainer.Shapes.Add(ovalShape);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ovalShape.BorderColor = this.BackColor;
            shapeContainer.Size = this.Size;
            ovalShape.Size = new Size(this.Size.Width - 2, this.Size.Height - 2);
            if (active == true)
            {
                ovalShape.FillColor = activeColor;
            }
            else
            {
                ovalShape.FillColor = inActiveColor;
            }
        }
    }
}
