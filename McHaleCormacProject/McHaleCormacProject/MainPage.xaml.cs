using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace McHaleCormacProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>


    public sealed partial class MainPage : Page
    {
        //all the static variable that I need to complete logic
        #region - Global variables
        Ellipse makeMove = new Ellipse();
        Ellipse Player1 = new Ellipse();
        Ellipse Player2 = new Ellipse();
        Ellipse [,] searchForWinarray = new Ellipse [6,7];
        Boolean playerTurn = false;
        String comparePiece;
        int redWins = 0;
        int yellowWins = 0;
#endregion
        //for stacking in columns correctly
        #region - column counters
        int column1counter = 0;
        int column2counter = 0;
        int column3counter = 0;
        int column4counter = 0;
        int column5counter = 0;
        int column6counter = 0;
        int column7counter = 0;
        #endregion

        public MainPage()
        {
            this.InitializeComponent();          
        }
        
        //start game clicked event
        private void startGame_Click(object sender, RoutedEventArgs e)
        {
            //run correct function
            createPlayingBoard();
        }
        
        // all gui for the board and how it appears when app runs
        // creates all ellipes for board and stores them in searchforwinarray
        // also creates ellipses for player1 (red) and player2 (yellow)
        private void createPlayingBoard()
        {
            startGame.Visibility = Visibility.Collapsed;
            //changeBackRound to blue
            Grid changeRoot = FindName("rootGrid") as Grid;
            changeRoot.Background = new SolidColorBrush(Colors.Gray);
            //may not need this
            //changeRoot.HorizontalAlignment = HorizontalAlignment.Center;
            //button to refresh game
            Button clearGame = new Button();
            clearGame.Content = "Clear Game";
            clearGame.VerticalAlignment = VerticalAlignment.Top;
            clearGame.Click += cleared;
            changeRoot.Children.Add(clearGame);
            StackPanel alignGame = new StackPanel();
            alignGame.Orientation = Orientation.Horizontal;
            alignGame.HorizontalAlignment = HorizontalAlignment.Center;
            alignGame.Height = 500;
            //alignGame.Width = 700;
            changeRoot.Children.Add(alignGame);
            //create the actual object
            Grid playingBoard = new Grid();
            playingBoard.Name = "referenceBoard";
            playingBoard.Height = 400;
            playingBoard.Width = 400;
            playingBoard.Background = new SolidColorBrush(Colors.Blue);
            //add margin to leave space for canvas with buttons
            //use for loop to add rows and columns
            //rows
            for (int i = 0; i < 6; i++)
            {
                playingBoard.RowDefinitions.Add(new RowDefinition());
            }
            //columns
            for (int j = 0; j < 7; j++)
            {
                playingBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //add white ellipses to set up board visually and prepare for moves
                    Ellipse moveChoice = new Ellipse();
                    moveChoice.Name = "search"+i+j;
                    moveChoice.Tag = "tag" + i + j;
                    moveChoice.Height = 40;
                    moveChoice.Width = 40;
                    moveChoice.Fill = new SolidColorBrush(Colors.White);
                    moveChoice.SetValue(Grid.RowProperty, i);
                    moveChoice.SetValue(Grid.ColumnProperty, j);                   
                    //Ellipse addEllipse = FindName("search" + i + j) as Ellipse;
                    playingBoard.Children.Add(moveChoice);
                    moveChoice.Tapped+= MovePiece;
                    searchForWinarray[i,j]  = moveChoice;
                }
            }
            //text for players
            TextBlock playerOne = new TextBlock();
            playerOne.Text = "Player 1, Wins: "+redWins;
            playerOne.VerticalAlignment = VerticalAlignment.Center;
            playerOne.Margin = new Thickness(5, 5, 5, 5);
            TextBlock playerTwo = new TextBlock();
            playerTwo.VerticalAlignment = VerticalAlignment.Center;
            playerTwo.Text = "Player 2, Wins: "+yellowWins;
            playerTwo.Margin = new Thickness(5, 5, 5, 5);
            //add players to board in correct order
            Player1.Height = 60;
            Player1.Width = 60;
            Player1.Fill = new SolidColorBrush(Colors.Red);
            Player1.Margin = new Thickness(10, 10, 10, 10);
            Player1.Tapped += MoveChoice;
            Player1.Name = "redPiece";
            Player2.Height = 60;
            Player2.Width = 60;
            Player2.Fill = new SolidColorBrush(Colors.Yellow);
            Player2.Margin = new Thickness(10, 10, 10, 10);
            Player2.Tapped += MoveChoiceTwo;
            Player2.Name = "yellowPiece";
            //add the board                  
            alignGame.Children.Add(playingBoard);
            alignGame.Children.Add(playerOne);
            alignGame.Children.Add(Player1);

            alignGame.Children.Add(playerTwo);
            alignGame.Children.Add(Player2);
            //playerTwo.Visibility = Visibility.Collapsed;
            Player2.Visibility = Visibility.Collapsed;
        }
        //function to clear the game
        private void cleared(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Ellipse clearGame = FindName("search"+i+j) as Ellipse;
                    clearGame.Fill = new SolidColorBrush(Colors.White);
                    clearGame.Tag = "newTag"+i+j;
                    //reset the counters
                     column1counter = 0;
                     column2counter = 0;
                     column3counter = 0;
                     column4counter = 0;
                     column5counter = 0;
                     column6counter = 0;
                     column7counter = 0;
                }
            }
        }

        //the tapped event for all white ellipes on the board
        //also contains all the moves so that the pieces stack properly
        private void MovePiece(object sender, TappedRoutedEventArgs e)
        {
            Ellipse showPieceTwo = FindName("yellowPiece") as Ellipse;
            Ellipse showPieceOne = FindName("redPiece") as Ellipse;
            if (showPieceTwo.Visibility == Visibility.Collapsed)
            {
                showPieceTwo.Visibility = Visibility.Visible;
                showPieceOne.Visibility = Visibility.Collapsed;
            }
            else if (showPieceOne.Visibility == Visibility.Collapsed)
            {
                showPieceOne.Visibility = Visibility.Visible;
                showPieceTwo.Visibility = Visibility.Collapsed;
            }
            Ellipse arrayPosition = (Ellipse)sender;
            string name = arrayPosition.Name;
            string column = name.Substring(7, 1);
            string row = name.Substring(6, 1);
            int columnReference = Int32.Parse(column);
            int rowReference = Int32.Parse(row);
#region - everymove
            if (playerTurn == true  && column1counter == 0  && columnReference == 0)// && arrayPosition.Fill == new SolidColorBrush(Colors.Red))
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[5, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[5, columnReference].Tag = "red";
                }
                searchForWinarray[5,columnReference].Fill = makeMove.Fill;                
                column1counter++;
                playerTurn = false;

            }
            else if (playerTurn == true && column1counter == 1 && columnReference == 0)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[4, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[4, columnReference].Tag = "red";
                }
                searchForWinarray[4, columnReference].Fill = makeMove.Fill;
                column1counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column1counter == 2 && columnReference == 0)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[3, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[3, columnReference].Tag = "red";
                }
                searchForWinarray[3, columnReference].Fill = makeMove.Fill;
                column1counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column1counter == 3 && columnReference == 0)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[2, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[2, columnReference].Tag = "red";
                }
                searchForWinarray[2, columnReference].Fill = makeMove.Fill;
                column1counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column1counter == 4 && columnReference == 0)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[1, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[1, columnReference].Tag = "red";
                }
                searchForWinarray[1, columnReference].Fill = makeMove.Fill;
                column1counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column1counter == 5 && columnReference == 0)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[0, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[0, columnReference].Tag = "red";
                }
                searchForWinarray[0, columnReference].Fill = makeMove.Fill;
                column1counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column2counter == 0 && columnReference == 1)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[5, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[5, columnReference].Tag = "red";
                }
                searchForWinarray[5, columnReference].Fill = makeMove.Fill;
                column2counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column2counter == 1 && columnReference == 1)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[4, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[4, columnReference].Tag = "red";
                }
                searchForWinarray[4, columnReference].Fill = makeMove.Fill;
                column2counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column2counter == 2 && columnReference == 1)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[3, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[3, columnReference].Tag = "red";
                }
                searchForWinarray[3, columnReference].Fill = makeMove.Fill;
                column2counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column2counter == 3 && columnReference == 1)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[2, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[2, columnReference].Tag = "red";
                }
                searchForWinarray[2, columnReference].Fill = makeMove.Fill;
                column2counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column2counter == 4 && columnReference == 1)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[1, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[1, columnReference].Tag = "red";
                }
                searchForWinarray[1, columnReference].Fill = makeMove.Fill;
                column2counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column2counter == 5 && columnReference == 1)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[0, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[0, columnReference].Tag = "red";
                }
                searchForWinarray[0, columnReference].Fill = makeMove.Fill;
                column2counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column3counter == 0 && columnReference == 2)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[5, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[5, columnReference].Tag = "red";
                }
                searchForWinarray[5, columnReference].Fill = makeMove.Fill;
                column3counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column3counter == 1 && columnReference == 2)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[4, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[4, columnReference].Tag = "red";
                }
                searchForWinarray[4, columnReference].Fill = makeMove.Fill;
                column3counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column3counter == 2 && columnReference == 2)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[3, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[3, columnReference].Tag = "red";
                }
                searchForWinarray[3, columnReference].Fill = makeMove.Fill;
                column3counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column3counter == 3 && columnReference == 2)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[2, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[2, columnReference].Tag = "red";
                }
                searchForWinarray[2, columnReference].Fill = makeMove.Fill;
                column3counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column3counter == 4 && columnReference == 2)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[1, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[1, columnReference].Tag = "red";
                }
                searchForWinarray[1, columnReference].Fill = makeMove.Fill;
                column3counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column3counter == 5 && columnReference == 2)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[0, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[0, columnReference].Tag = "red";
                }
                searchForWinarray[0, columnReference].Fill = makeMove.Fill;
                column3counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column4counter == 0 && columnReference == 3)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[5, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[5, columnReference].Tag = "red";
                }
                searchForWinarray[5, columnReference].Fill = makeMove.Fill;
                column4counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column4counter == 1 && columnReference == 3)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[4, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[4, columnReference].Tag = "red";
                }
                searchForWinarray[4, columnReference].Fill = makeMove.Fill;
                column4counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column4counter == 2 && columnReference == 3)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[3, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[3, columnReference].Tag = "red";
                }
                searchForWinarray[3, columnReference].Fill = makeMove.Fill;
                column4counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column4counter == 3 && columnReference == 3)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[2, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[2, columnReference].Tag = "red";
                }
                searchForWinarray[2, columnReference].Fill = makeMove.Fill;
                column4counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column4counter == 4 && columnReference == 3)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[1, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[1, columnReference].Tag = "red";
                }
                searchForWinarray[1, columnReference].Fill = makeMove.Fill;
                column4counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column4counter == 5 && columnReference == 3)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[0, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[0, columnReference].Tag = "red";
                }
                searchForWinarray[0, columnReference].Fill = makeMove.Fill;
                column4counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column5counter == 0 && columnReference == 4)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[5, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[5, columnReference].Tag = "red";
                }
                searchForWinarray[5, columnReference].Fill = makeMove.Fill;
                column5counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column5counter == 1 && columnReference == 4)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[4, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[4, columnReference].Tag = "red";
                }
                searchForWinarray[4, columnReference].Fill = makeMove.Fill;
                column5counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column5counter == 2 && columnReference == 4)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[3, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[3, columnReference].Tag = "red";
                }
                searchForWinarray[3, columnReference].Fill = makeMove.Fill;
                column5counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column5counter == 3 && columnReference == 4)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[2, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[2, columnReference].Tag = "red";
                }
                searchForWinarray[2, columnReference].Fill = makeMove.Fill;
                column5counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column5counter == 4 && columnReference == 4)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[1, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[1, columnReference].Tag = "red";
                }
                searchForWinarray[1, columnReference].Fill = makeMove.Fill;
                column5counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column5counter == 5 && columnReference == 4)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[0, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[0, columnReference].Tag = "red";
                }
                searchForWinarray[0, columnReference].Fill = makeMove.Fill;
                column5counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column6counter == 0 && columnReference == 5)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[5, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[5, columnReference].Tag = "red";
                }
                searchForWinarray[5, columnReference].Fill = makeMove.Fill;
                column6counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column6counter == 1 && columnReference == 5)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[4, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[4, columnReference].Tag = "red";
                }
                searchForWinarray[4, columnReference].Fill = makeMove.Fill;
                column6counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column6counter == 2 && columnReference == 5)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[3, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[3, columnReference].Tag = "red";
                }
                searchForWinarray[3, columnReference].Fill = makeMove.Fill;
                column6counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column6counter == 3 && columnReference == 5)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[2, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[2, columnReference].Tag = "red";
                }
                searchForWinarray[2, columnReference].Fill = makeMove.Fill;
                column6counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column6counter == 4 && columnReference == 5)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[1, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[1, columnReference].Tag = "red";
                }
                searchForWinarray[1, columnReference].Fill = makeMove.Fill;
                column6counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column6counter == 5 && columnReference == 5)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[0, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[0, columnReference].Tag = "red";
                }
                searchForWinarray[0, columnReference].Fill = makeMove.Fill;
                column6counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column7counter == 0 && columnReference == 6)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[5, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[5, columnReference].Tag = "red";
                }
                searchForWinarray[5, columnReference].Fill = makeMove.Fill;
                column7counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column7counter == 1 && columnReference == 6)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[4, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[4, columnReference].Tag = "red";
                }
                searchForWinarray[4, columnReference].Fill = makeMove.Fill;
                column7counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column7counter == 2 && columnReference == 6)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[3, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[3, columnReference].Tag = "red";
                }
                searchForWinarray[3, columnReference].Fill = makeMove.Fill;
                column7counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column7counter == 3 && columnReference == 6)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[2, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[2, columnReference].Tag = "red";
                }
                searchForWinarray[2, columnReference].Fill = makeMove.Fill;
                column7counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column7counter == 4 && columnReference == 6)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[1, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[1, columnReference].Tag = "red";
                }
                searchForWinarray[1, columnReference].Fill = makeMove.Fill;
                column7counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column7counter == 5 && columnReference == 6)
            {
                if (comparePiece == "Yellow")
                {
                    searchForWinarray[0, columnReference].Tag = "yellow";
                }
                else if (comparePiece == "Red")
                {
                    searchForWinarray[0, columnReference].Tag = "red";
                }
                searchForWinarray[0, columnReference].Fill = makeMove.Fill;
                column7counter++;
                playerTurn = false;
            }
            #endregion
            searchForWin();
        }
        
        // Tapped event for player 1 (red)
        private void MoveChoice(object sender, TappedRoutedEventArgs e)
        {
                Ellipse moveThisPeice = (Ellipse)sender;
                playerTurn = true;
                makeMove.Fill = moveThisPeice.Fill;
                comparePiece = "Red";
                //searchForWin(); run this in move piece            
        }

        // Tapped event for player 2 (yellow)
        private void MoveChoiceTwo(object sender, TappedRoutedEventArgs e)
        {
                Ellipse moveThisPeice = (Ellipse)sender;
                playerTurn = true;
                makeMove.Fill = moveThisPeice.Fill;
                comparePiece = "Yellow";           
        }
       
        //logic complete bredren- all the logic for finding four in a row!! finally complete, tested and running.. praise haile selassie
        private void searchForWin()
        {
            //search horizontally
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    //will run this part so it is iterarting through array properly
                    //searchForWinarray[i, j].Fill = new SolidColorBrush(Colors.Black);
                    if (j < 4)
                    {
                        if (searchForWinarray[i, j].Tag == searchForWinarray[i, j + 1].Tag && searchForWinarray[i, j + 1].Tag == searchForWinarray[i, j + 2].Tag && searchForWinarray[i, j + 2].Tag == searchForWinarray[i, j + 3].Tag)
                        {
                            //winCounter++;
                            searchForWinarray[i, j].Fill = new SolidColorBrush(Colors.Black);
                            searchForWinarray[i, j + 1].Fill = new SolidColorBrush(Colors.Black);
                            searchForWinarray[i, j + 2].Fill = new SolidColorBrush(Colors.Black);
                            searchForWinarray[i, j + 3].Fill = new SolidColorBrush(Colors.Black);

                        }
                    }                
                }
            }//end of horizontal search

            //search vertically
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //will run this part so it is iterarting through array properly
                    //searchForWinarray[i, j].Fill = new SolidColorBrush(Colors.Black);
                    if (i < 3)
                    {
                        if (searchForWinarray[i, j].Tag == searchForWinarray[i+1, j].Tag && searchForWinarray[i + 1, j].Tag == searchForWinarray[i+2, j].Tag && searchForWinarray[i+2, j].Tag == searchForWinarray[i+3, j].Tag)
                        {
                            //winCounter++;
                            searchForWinarray[i, j].Fill = new SolidColorBrush(Colors.Black);
                            searchForWinarray[i+1, j].Fill = new SolidColorBrush(Colors.Black);
                            searchForWinarray[i+2, j].Fill = new SolidColorBrush(Colors.Black);
                            searchForWinarray[i+3, j].Fill = new SolidColorBrush(Colors.Black);

                        }
                    }
                }
            }//end of vertical search

            //search diagonal from left to right
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    //will run this part so it is iterarting through array properly
                    //searchForWinarray[i, j].Fill = new SolidColorBrush(Colors.Black);
                    if (i < 3 && j < 4)
                    {
                        if (searchForWinarray[i, j].Tag == searchForWinarray[i + 1, j+1].Tag && searchForWinarray[i + 1, j+1].Tag == searchForWinarray[i + 2, j+2].Tag && searchForWinarray[i + 2, j+2].Tag == searchForWinarray[i + 3, j+3].Tag)
                        {
                            //winCounter++;
                            searchForWinarray[i, j].Fill = new SolidColorBrush(Colors.Black);
                            searchForWinarray[i + 1, j+1].Fill = new SolidColorBrush(Colors.Black);
                            searchForWinarray[i + 2, j+2].Fill = new SolidColorBrush(Colors.Black);
                            searchForWinarray[i + 3, j+3].Fill = new SolidColorBrush(Colors.Black);

                        }
                    }
                }
            }//end of diagonal from left to right search

            //search diagonal from right to left
            for (int i = 5; i > 0; i--)
            {
                for (int j = 0; j <6; j++)
                {
                    //will run this part so it is iterarting through array properly
                    //searchForWinarray[i, j].Fill = new SolidColorBrush(Colors.Black);
                    if(i > 2 && j < 4 )
                        if (searchForWinarray[i, j].Tag == searchForWinarray[i - 1, j + 1].Tag && searchForWinarray[i - 1, j + 1].Tag == searchForWinarray[i - 2, j + 2].Tag  && searchForWinarray[i - 2, j + 2].Tag == searchForWinarray[i - 3, j + 3].Tag)
                        {
                            //winCounter++;
                            searchForWinarray[i, j].Fill = new SolidColorBrush(Colors.Black);
                            searchForWinarray[i - 1, j +1].Fill = new SolidColorBrush(Colors.Black);
                            searchForWinarray[i - 2, j + 2].Fill = new SolidColorBrush(Colors.Black);
                            searchForWinarray[i - 3, j + 3].Fill = new SolidColorBrush(Colors.Black);
                            // using for reference searchForWinarray[0, 0].Fill = new SolidColorBrush(Colors.Black);
                        }
                    
                }
            }//end of diagonal from right to left search
        }


    }
}
