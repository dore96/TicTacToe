using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GameLogic;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        const int User = 1;
        const int Computer = -1;
        private Board _game = new Board();
        private Button[] _buttons = new Button[9];

        public Form1()
        {
            InitializeComponent();
            _game = new Board();

            _buttons[0] = button1;
            _buttons[1] = button2;
            _buttons[2] = button3;
            _buttons[3] = button4;
            _buttons[4] = button5;
            _buttons[5] = button6;
            _buttons[6] = button7;
            _buttons[7] = button8;
            _buttons[8] = button9;

            for (var i = 0; i < 9; i++)
            {
                _buttons[i].Click += handelButtonClick;
                _buttons[i].Tag = i;
            }
        }

        private void handelButtonClick(object sender, EventArgs e)
        {
            var clickedButton = (Button)sender;
            int gameSquerNumber = (int)clickedButton.Tag;

            _game.board[gameSquerNumber] = User;
            updateBoard();

            if (_game.IsWinner(_game.board, User))
            {
                MessageBox.Show("Player human won!");
                disableAllButtons();
            }
            else if (!_game.board.Contains(0))
            {
                MessageBox.Show("The board is full");
                disableAllButtons();
            }
            else
            {
                _game.PickSquare(_game.board, Computer);
                updateBoard();
                if (_game.IsWinner(_game.board, Computer))
                {
                    MessageBox.Show("Computer won! hahaha");
                    disableAllButtons();
                }
            }

        }

        private void disableAllButtons()
        {
            foreach (var itemButton in _buttons)
                itemButton.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateBoard();
        }

        private void updateBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                if (_game.board[i] == 0)
                {
                    _buttons[i].Text = "";
                    _buttons[i].Enabled = true;
                }               
                else if (_game.board[i] == 1)
                {
                    _buttons[i].Text = "X";
                    _buttons[i].Enabled = false;
                }
                else
                {
                    _buttons[i].Text = "O";
                    _buttons[i].Enabled = false;
                }
                _buttons[i].ForeColor = Color.Black;
            }
        }

        private void enableAll()
        {
            foreach (var button in _buttons)
                button.Enabled = true;
            updateBoard();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created By Dor Edelman ©");
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _game = new Board();
            enableAll();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
