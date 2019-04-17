using System;
using System.Drawing;
using System.Windows.Forms;

namespace Life.Client.Forms
{
    public partial class Form1 : Form
    {
        private DateTime prevUpdate = DateTime.Now;
        private DateTime prevDraw = DateTime.Now;
        private int fieldStartLine = 25;
        private int cellSize = 20;
        private Timer drawTimer;
        private Timer updateTime;
        private Game game;
        private GameManager gameManager;
        public Form1()
        {
            InitializeComponent();

            CreateDrawTimer();
            CreateUpdateTimer();

            InitCore();
        }

        private void InitCore()
        {
            var fieldManager = new FieldManager();
            var gameBuilder = new GameBuilder(fieldManager);
            game = gameBuilder.Build();

            gameManager = new GameManager(fieldManager);
        }

        private void CreateDrawTimer()
        {
            drawTimer = new Timer();
            drawTimer.Interval = 100;
            drawTimer.Tick += DrawTimer;
            drawTimer.Start();
        }

        private void CreateUpdateTimer()
        {
            updateTime = new Timer();
            updateTime.Interval = 100;
            updateTime.Tick += UpdateTimer;
            updateTime.Start();
        }

        private void UpdateTimer(object sender, EventArgs eventArgs)
        {
            ShowCmd();
            gameManager.Update(game);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var graphics = e.Graphics;
            Clear(graphics);
            DrawGrid(graphics);
            DrawMap(graphics);
        }

        private void DrawTimer(object sender, EventArgs eventArgs)
        {
            ShowFps();
            ShowSize();
            Refresh();
        }

        private void ShowFps()
        {
            var now = DateTime.Now;
            var fps = 1000_0000f / (now.Ticks - prevDraw.Ticks);
            toolStripStatusLabel2.Text = $"FPS: {fps:0.00}";
            prevDraw = now;
        }

        private void ShowCmd()
        {
            var now = DateTime.Now;
            var fps = 1000_0000f / (now.Ticks - prevUpdate.Ticks);
            toolStripStatusLabel3.Text = $"CMD: {fps:0.00}";
            prevUpdate = now;
        }

        private void ShowSize()
        {
            toolStripStatusLabel1.Text = $"{game.Field.Height}x{game.Field.Width}";
        }

        private void DrawMap(Graphics graphics)
        {
            using (var brush = new SolidBrush(Color.Black))
            {
                for (int i = 0; i < game.Field.Height; i++)
                {
                    for (int j = 0; j < game.Field.Width; j++)
                    {
                        if (game.Field.Map[i, j])
                        {
                            graphics.FillRectangle(brush, j * cellSize, fieldStartLine + i * cellSize, cellSize, cellSize);
                        }
                    }
                }
            }
        }

        private void Clear(Graphics graphics)
        {
            graphics.Clear(BackColor);
        }

        private void DrawGrid(Graphics graphics)
        {
            using (var pen = new Pen(Color.Black))
            {
                for (int i = 0; i <= game.Field.Height; i++)
                {
                    graphics.DrawLine(pen, 0, fieldStartLine + i * cellSize, game.Field.Width * cellSize, fieldStartLine + i * cellSize);
                }

                for (int j = 0; j <= game.Field.Width; j++)
                {
                    graphics.DrawLine(pen, j * cellSize, fieldStartLine, j * cellSize, fieldStartLine + game.Field.Height * cellSize);
                }
            }
        }
    }
}
