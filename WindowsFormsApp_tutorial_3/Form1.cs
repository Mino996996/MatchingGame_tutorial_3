using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_tutorial_3
{
    public partial class Form1 : Form
    {
        Label firstClicked = null;
        Label secondClicked = null;
        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k", "b", "b", "v", "v", "w", "w", "z", "z"
        };
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        /// <summary>
        /// 各マスにランダムでアイコンを埋める
        /// </summary>
        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {   
                    int randomNumber = random.Next(icons.Count); // 現在のリストの個数を取得して、その個数を上限としたランダムな値を取得
                    iconLabel.Text = icons[randomNumber]; // ランダムな数値順に該当するアイコンをセット
                    iconLabel.ForeColor = iconLabel.BackColor;　// フォントの色を同じにして非表示風にする
                    icons.RemoveAt(randomNumber); // 該当したアイコンをリストから削除
                }
            }
        }

        /// <summary>
        /// 全マス目対象のイベント:クリックしたマスのアイコンを黒くする。クリックされてたら以降は何もしない
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null) // 1回目のクリック時
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                // 2回目のクリック時
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // ゲームの達成判定:全部合わせられたか否か
                CheckForWinner();

                // 絵合わせ出来ていたらクリック状態の変数をNULLにする
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // If the player gets this far, the player 
                // clicked two different icons, so start the 
                // timer (which will wait three quarters of 
                // a second, and then hide the icons)
                timer1.Start();
            }
        }

        /// <summary>
        /// 絵合わせがミスマッチの時にインターバル分だけ表示を見せてから、非表示にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            // Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Reset firstClicked and secondClicked 
            // so the next time a label is
            // clicked, the program knows it's the first click
            firstClicked = null;
            secondClicked = null;
        }

        /// <summary>
        /// 全アイコンをマッチした際のメッセージ
        /// </summary>
        private void CheckForWinner()
        {
            // 全表示されてない場合は早期リターン
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("絵合わせ完了！", "おめでとうございます！");
            Close();
        }
    }
}
