using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Life.Client.Network.SignalR;
using Newtonsoft.Json;

namespace Life.Client.Forms
{
    public partial class Form1 : Form
    {
        private DateTime prevUpdate = DateTime.Now;
        private DateTime prevDraw = DateTime.Now;
        private int fieldStartLine = 25;
        private int cellSize = 20;
        private Timer drawTimer;
        private Game game;
        private ServerConnection serverConnection;
        public Form1()
        {
            InitializeComponent();

            //CreateDrawTimer();

            InitCore();
            Task.Run(Connect);
        }

        private void InitCore()
        {
            var height = 20;
            var width = 30;
            game = new Game
            {
                Field = new Field { Height = height, Width = width, Map = new bool[height, width] }
            };
        }

        private void CreateDrawTimer()
        {
            drawTimer = new Timer();
            drawTimer.Interval = 1000;
            drawTimer.Tick += DrawTimer;
            drawTimer.Start();
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
            //Refresh();
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

        private async void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Connect();
        }

        private async Task Connect()
        {
            if (serverConnection == null)
            {
                serverConnection = new ServerConnection("http://localhost:8088");
                await serverConnection.OpenAsync();
                serverConnection.OnReceive<string>("SendField", message =>
                {
                    //var field = JsonConvert.DeserializeObject<Field>(message);
                    //game.Field = field;
                    //ShowCmd();
                });
            }

            await serverConnection.Send("Connect");
            toolStripStatusLabel4.Text = "Connected";
        }

        private async void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await serverConnection.Send("Disconnect");
            toolStripStatusLabel4.Text = "Disconnected";
        }

        protected override async void OnClosed(EventArgs e)
        {
            await serverConnection.Send("Disconnect");
            serverConnection.Close();
            base.OnClosed(e);
        }
    }
}
