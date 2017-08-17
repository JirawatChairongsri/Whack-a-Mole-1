IERG 3080 Information and Software Engineering Practice
README file for a “Whack a Mole” Project

Student Name: Eshbolotov Kairat
Student ID: 1155060862
Submission date: 25 November 2016


Contents:
•	Motivation and Synopsis
•	Code Examples
•	Installation
•	Tests
•	Contributors














Motivation and Synopsis

This project has been submitted for the course IERG 3080 – Information and Software Engineering Practice. There have in total been 4 projects, and I have been assigned the first one, which requires to build a “Whack a Mole” game. In this game, one needs to catch/press/shoot/click moles when they appear on the screen as quickly and as many of them as possible.
I replace the moles by the red buttons. There are 25 possible spots where the moles could appear randomly. The color of the spot/button is changed from gray to red when the mole is there. There are three levels of difficulty in the implemented game. The moles appear and disappear every 1.5sec, 1.0sec and 0.5sec in Level 1, Level 2 and Level 3, respectively.   The game duration is 20 seconds, after which the score is displayed in terms of the percentage of successful catches of the moles.  
Code Examples
The WhackMole2 class has been designed. 
The method GenerateMole() generates a random mole on a 5x5 WrapPanel. 
public void GenerateMole()
        {
            for (int i = 0; i < moleState.Length; i++)
                buttons[i].Background = Brushes.Gray;
            Random rand = new Random();
            int index = rand.Next(0, 25);
            buttons[index].Background = Brushes.Red;
            total++;     
        }
RoutedEventArgs e is a parameter called e that contains the event data. Object sender is a parameter called sender that contains a reference to the control/object that raised the event.

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
When Button is clicked, the OnMove event handler will be fired. The "object sender" portion will be a reference to the button which was clicked. 


Installation

The executable file WhackMole.exe contained inside WhackMole.zip can be run directly. The project has been created with the aid of Windows Presentation Foundation (WPF) on the Visual Studio 2013. The source codes are contained in MainWindow.xaml.cs and App.xaml.cs. 


Tests

The following code defines the duration of the game. The value set by me is 200. The duration is therefore 200/10 = 20 seconds. The value could be easily modified. 
…
private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            counter = 200;

            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(0.1);
…
private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            counter--;
            textBox.Text = counter / 10 + "s";
            currentGame.Renewal(counter);
…


Contributors

The project has been built with the guidance of the wonderful tutors of the IERG 3080 course (2016 Fall semester) of CUHK Information Engineering Department.

