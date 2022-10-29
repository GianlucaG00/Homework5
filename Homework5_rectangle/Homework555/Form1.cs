namespace Homework555
{
    public partial class Form1 : Form
    {
        Bitmap b;
        Graphics g;
        Rectangle r;

        int x_mouse, y_mouse;
        int x_down, y_down;

        int r_width, r_height;

        float magnFactor = 100.0F;

        bool drag = false;
        bool resizing = false;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!r.Contains(e.X, e.Y)) return;

            x_mouse = e.X;
            y_mouse = e.Y;

            x_down = r.X;
            y_down = r.Y;

            r_width = r.Width;
            r_height = r.Height;

            if (e.Button == MouseButtons.Left)
            {
                drag = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                resizing = true;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
            resizing = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (b == null) return;

            int delta_x = e.X - x_mouse;
            int delta_y = e.Y - y_mouse;

            if (drag)
            {
                r.X = x_down + delta_x;
                r.Y = y_down + delta_y;
            }
            else if (resizing)
            {
                r.Width = r_width + delta_x;
                r.Height = r_height + delta_y;
            }
            redraw();
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!(ModifierKeys == Keys.Control)) return;
            if (pictureBox1_MouseWheel_SR) return;

            pictureBox1_MouseWheel_SR = true;

            float stepZoom;
            if (ModifierKeys == (Keys.Shift | Keys.Control))
            {
                stepZoom = 0.01F;
            }
            else
            {
                stepZoom = 0.1F;
            }

            r.Inflate((int)(e.Delta * stepZoom), (int)(e.Delta * stepZoom));

            pictureBox1_MouseWheel_SR = false;
            redraw();

        }

        bool pictureBox1_MouseWheel_SR;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
            r = new Rectangle(20, 20, 500, 300);

            g.DrawRectangle(Pens.Red, r);
            g.FillRectangle(Brushes.Blue, r);

            pictureBox1.Image = b;
        }

        private void redraw()
        {
            g.Clear(BackColor);
            g.DrawRectangle(Pens.Blue, r);
            g.FillRectangle(Brushes.Blue, r);
            pictureBox1.Image = b;
        }
    }
}