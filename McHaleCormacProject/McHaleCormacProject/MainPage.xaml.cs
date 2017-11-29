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
        Ellipse makeMove = new Ellipse();
        Ellipse Player1 = new Ellipse();
        Ellipse Player2 = new Ellipse();
        Ellipse [,] searchForWinarray = new Ellipse [6,7];
        Boolean playerTurn = false;
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
        //start game
        private void startGame_Click(object sender, RoutedEventArgs e)
        {
            //run correct function
            createPlayingBoard();
        }

        private void createPlayingBoard()
        {

            //changeBackRound to blue
            Grid changeRoot = FindName("rootGrid") as Grid;
            changeRoot.Background = new SolidColorBrush(Colors.Gray);
            //may not need this
            //changeRoot.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel alignGame = new StackPanel();
            alignGame.Orientation = Orientation.Horizontal;
            alignGame.HorizontalAlignment = HorizontalAlignment.Center;
            alignGame.Height = 500;
            alignGame.Width = 700;
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
            
            //add players to board in correct order
            Player1.Height = 60;
            Player1.Width = 60;
            Player1.Fill = new SolidColorBrush(Colors.Red);
            Player1.Margin = new Thickness(10, 10, 10, 10);
            Player1.Tapped += MoveChoice;
            Player2.Height = 60;
            Player2.Width = 60;
            Player2.Fill = new SolidColorBrush(Colors.Yellow);
            Player2.Margin = new Thickness(10, 10, 10, 10);
            Player2.Tapped += MoveChoice;
            //add the board
           
            changeRoot.Children.Add(playingBoard);
            alignGame.Children.Add(Player1);
            alignGame.Children.Add(Player2);

            
        }


        private void MovePiece(object sender, TappedRoutedEventArgs e)
        {          
            Ellipse arrayPosition = (Ellipse)sender;
            string name = arrayPosition.Name;
            string column = name.Substring(7, 1);
            string row = name.Substring(6, 1);
            int columnReference = Int32.Parse(column);
            int rowReference = Int32.Parse(row);
#region - everymove
            if (playerTurn == true && columnReference == 0 && column1counter == 0)// && arrayPosition.Fill == new SolidColorBrush(Colors.Red))
            {
                searchForWinarray[5,columnReference].Fill = makeMove.Fill;
                column1counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column1counter == 1 && columnReference == 0)
            {
                searchForWinarray[4, columnReference].Fill = makeMove.Fill;
                column1counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column1counter == 2 && columnReference == 0)
            {
                searchForWinarray[3, columnReference].Fill = makeMove.Fill;
                column1counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column1counter == 3 && columnReference == 0)
            {
                searchForWinarray[2, columnReference].Fill = makeMove.Fill;
                column1counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column1counter == 4 && columnReference == 0)
            {
                searchForWinarray[1, columnReference].Fill = makeMove.Fill;
                column1counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column1counter == 5 && columnReference == 0)
            {
                searchForWinarray[0, columnReference].Fill = makeMove.Fill;
                column1counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column2counter == 0 && columnReference == 1)
            {
                searchForWinarray[5, columnReference].Fill = makeMove.Fill;
                column2counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column2counter == 1 && columnReference == 1)
            {
                searchForWinarray[4, columnReference].Fill = makeMove.Fill;
                column2counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column2counter == 2 && columnReference == 1)
            {
                searchForWinarray[3, columnReference].Fill = makeMove.Fill;
                column2counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column2counter == 3 && columnReference == 1)
            {
                searchForWinarray[2, columnReference].Fill = makeMove.Fill;
                column2counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column2counter == 4 && columnReference == 1)
            {
                searchForWinarray[1, columnReference].Fill = makeMove.Fill;
                column2counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column2counter == 5 && columnReference == 1)
            {
                searchForWinarray[0, columnReference].Fill = makeMove.Fill;
                column2counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column3counter == 0 && columnReference == 2)
            {
                searchForWinarray[5, columnReference].Fill = makeMove.Fill;
                column3counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column3counter == 1 && columnReference == 2)
            {
                searchForWinarray[4, columnReference].Fill = makeMove.Fill;
                column3counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column3counter == 2 && columnReference == 2)
            {
                searchForWinarray[3, columnReference].Fill = makeMove.Fill;
                column3counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column3counter == 3 && columnReference == 2)
            {
                searchForWinarray[2, columnReference].Fill = makeMove.Fill;
                column3counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column3counter == 4 && columnReference == 2)
            {
                searchForWinarray[1, columnReference].Fill = makeMove.Fill;
                column3counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column3counter == 5 && columnReference == 2)
            {
                searchForWinarray[0, columnReference].Fill = makeMove.Fill;
                column3counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column4counter == 0 && columnReference == 3)
            {
                searchForWinarray[5, columnReference].Fill = makeMove.Fill;
                column4counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column4counter == 1 && columnReference == 3)
            {
                searchForWinarray[4, columnReference].Fill = makeMove.Fill;
                column4counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column4counter == 2 && columnReference == 3)
            {
                searchForWinarray[3, columnReference].Fill = makeMove.Fill;
                column4counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column4counter == 3 && columnReference == 3)
            {
                searchForWinarray[2, columnReference].Fill = makeMove.Fill;
                column4counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column4counter == 4 && columnReference == 3)
            {
                searchForWinarray[1, columnReference].Fill = makeMove.Fill;
                column4counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column4counter == 5 && columnReference == 3)
            {
                searchForWinarray[0, columnReference].Fill = makeMove.Fill;
                column4counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column5counter == 0 && columnReference == 4)
            {
                searchForWinarray[5, columnReference].Fill = makeMove.Fill;
                column5counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column5counter == 1 && columnReference == 4)
            {
                searchForWinarray[4, columnReference].Fill = makeMove.Fill;
                column5counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column5counter == 2 && columnReference == 4)
            {
                searchForWinarray[3, columnReference].Fill = makeMove.Fill;
                column5counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column5counter == 3 && columnReference == 4)
            {
                searchForWinarray[2, columnReference].Fill = makeMove.Fill;
                column5counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column5counter == 4 && columnReference == 4)
            {
                searchForWinarray[1, columnReference].Fill = makeMove.Fill;
                column5counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column5counter == 5 && columnReference == 4)
            {
                searchForWinarray[0, columnReference].Fill = makeMove.Fill;
                column5counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column6counter == 0 && columnReference == 5)
            {
                searchForWinarray[5, columnReference].Fill = makeMove.Fill;
                column6counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column6counter == 1 && columnReference == 5)
            {
                searchForWinarray[4, columnReference].Fill = makeMove.Fill;
                column6counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column6counter == 2 && columnReference == 5)
            {
                searchForWinarray[3, columnReference].Fill = makeMove.Fill;
                column6counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column6counter == 3 && columnReference == 5)
            {
                searchForWinarray[2, columnReference].Fill = makeMove.Fill;
                column6counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column6counter == 4 && columnReference == 5)
            {
                searchForWinarray[1, columnReference].Fill = makeMove.Fill;
                column6counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column6counter == 5 && columnReference == 5)
            {
                searchForWinarray[0, columnReference].Fill = makeMove.Fill;
                column6counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column7counter == 0 && columnReference == 6)
            {
                searchForWinarray[5, columnReference].Fill = makeMove.Fill;
                column7counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column7counter == 1 && columnReference == 6)
            {
                searchForWinarray[4, columnReference].Fill = makeMove.Fill;
                column7counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column7counter == 2 && columnReference == 6)
            {
                searchForWinarray[3, columnReference].Fill = makeMove.Fill;
                column7counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column7counter == 3 && columnReference == 6)
            {
                searchForWinarray[2, columnReference].Fill = makeMove.Fill;
                column7counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column7counter == 4 && columnReference == 6)
            {
                searchForWinarray[1, columnReference].Fill = makeMove.Fill;
                column7counter++;
                playerTurn = false;
            }
            else if (playerTurn == true && column7counter == 5 && columnReference == 6)
            {
                searchForWinarray[0, columnReference].Fill = makeMove.Fill;
                column7counter++;
                playerTurn = false;
            }
            #endregion

            // searchForWin();
        }

        private void MoveChoice(object sender, TappedRoutedEventArgs e)
        {
            Ellipse moveThisPeice = (Ellipse)sender;
            playerTurn = true;
            makeMove.Fill = moveThisPeice.Fill;
        }

        private void searchForWin()
        {
           
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {

                    if (searchForWinarray[i,j].Fill == searchForWinarray[i+1,j+1].Fill)
                    {
                        searchForWinarray[i,j].Fill = new SolidColorBrush(Colors.Black);
                        
                    }                   
                }
            }           
        }
    }
}
