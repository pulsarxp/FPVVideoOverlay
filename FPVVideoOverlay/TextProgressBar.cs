namespace FPVVideoOverlay
{
    public class TextProgressBar : ProgressBar
    {
        public TextProgressBar()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = ClientRectangle;

            using SolidBrush backgroundBrush =
                new SolidBrush(SystemColors.Control);

            e.Graphics.FillRectangle(
                backgroundBrush,
                rect);

            double range = Maximum - Minimum;

            double percentage =
                range > 0
                    ? (double)(Value - Minimum) / range
                    : 0;

            int progressWidth =
                (int)(rect.Width * percentage);

            if (progressWidth > 0)
            {
                Rectangle progressRect =
                    new Rectangle(
                        0,
                        0,
                        progressWidth,
                        rect.Height);

                using SolidBrush progressBrush =
                    new SolidBrush(SystemColors.Highlight);

                e.Graphics.FillRectangle(
                    progressBrush,
                    progressRect);
            }

            using Pen borderPen =
                new Pen(SystemColors.ControlDark);

            e.Graphics.DrawRectangle(
                borderPen,
                0,
                0,
                Width - 1,
                Height - 1);

            string text = $"{Value}%";

            TextRenderer.DrawText(
                e.Graphics,
                text,
                Font,
                rect,
                SystemColors.ControlText,
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.SingleLine);
        }
    }
}