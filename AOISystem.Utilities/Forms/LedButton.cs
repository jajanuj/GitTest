using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
    public class LedButton : Button
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

        public LedButton()
        {
            Size = new Size(65, 35);
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            SolidBrush myBrush;
            if (active == true)
            {
                myBrush = new SolidBrush(activeColor);
            }
            else
                myBrush = new SolidBrush(inActiveColor);
            pevent.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1), pevent.ClipRectangle.X + 6, (float)(pevent.ClipRectangle.Height * 0.72), (float)(pevent.ClipRectangle.Width - 12), (float)(pevent.ClipRectangle.Height * 0.11));
            pevent.Graphics.FillRectangle(myBrush, pevent.ClipRectangle.X + 6, (int)(pevent.ClipRectangle.Height * 0.73), (int)(pevent.ClipRectangle.Width - 12), (int)(pevent.ClipRectangle.Height * 0.1));
            //pevent.Graphics.DrawLine(new Pen(myBrush, 3), new Point((int)(pevent.ClipRectangle.X + 6), (int)(pevent.ClipRectangle.Height * 0.8)), new Point((int)(pevent.ClipRectangle.Width - 6), (int)(pevent.ClipRectangle.Height * 0.8)));
        }
    }
}
