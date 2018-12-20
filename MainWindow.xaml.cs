using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace WhackMole
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            scoreBox.Visibility = Visibility.Hidden;
            levelBox.Text = "Select Difficulty Level: \nBottom - easy, Middle - medium, Top - hard";
            

        }

        WhackMole2 currentGame;

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int counter;
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            S1.Play();

            counter = 200;

            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(0.1);
            dispatcherTimer.Start();
           
            if (currentGame == null)
            {
                Button[] buttons = new Button[25];
                for (int i = 0; i < 25; i++)
                {
                    buttons[i] = wrapPanel.Children[i] as Button;
                    //buttons[i].Background = Brushes.Gray;
                }
                currentGame = new WhackMole2(buttons, (int)slider.Value);
            }
            startButton.Visibility = Visibility.Hidden;
            slider.Visibility = Visibility.Hidden;
            levelBox.Visibility = Visibility.Hidden;

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            counter--;
            textBox.Text = counter / 10 + " seconds left";
            currentGame.Renewal(counter);
            if (counter == 0)
            {
                textBox.Text = "Game Over";
                dispatcherTimer.Stop();
                scoreBox.Visibility = Visibility.Visible;
                for (int i = 0; i < 25; i++)
                    ((Button)wrapPanel.Children[i]).Background = Brushes.Gray;
                scoreBox.Text = "You scored " + Math.Round((double)currentGame.Score/currentGame.Total*100, 2) + "% out of 100%";
            }

            //print the result  
        }

        private void StopTimerButton_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Stop();
        }       
    }

    public class WhackMole2
    {
        private Button[] buttons;
        private int score = 0;
        private int total = 0;
        private int[] moleState;
        private int diff;

        public int Score { get { return score; } }
        public int Total { get { return total; } }
        

        public WhackMole2(Button[] buttons, int diff)
        {
            this.buttons = buttons;
            switch (diff)
            {
                case 1:
                    this.diff = 15;
                    break;
                case 2:
                    this.diff = 10;
                    break;
                case 3:
                    this.diff = 5;
                    break;
            }
            for (int i = 0; i < 25; i++)
            {
                buttons[i].Click += new RoutedEventHandler(OnMove);  
                buttons[i].Background = Brushes.Gray;
            }

            moleState = new int[buttons.Length];
            GenerateMole();
            Renewal(0);

        }

        public void OnMove(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            if (button.Background == Brushes.Red)
            {
                button.Background = Brushes.Gray;
                score = score + 1;
            }
            else
            {
                // cannot be modified once marked: nothing
            }
        }

        public void Renewal(int counter)
        {           
            if (counter % diff == 0)
                GenerateMole();
        }

        public void GenerateMole()
        {
            for (int i = 0; i < moleState.Length; i++)
                buttons[i].Background = Brushes.Gray;
            Random rand = new Random();
            int index = rand.Next(0, 25);
            buttons[index].Background = Brushes.Red;
            total++;     
        }
    }

}