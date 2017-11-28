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
        Ellipse checkWinRed = new Ellipse();

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
            //create the actual objcet
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
            alignGame.Children.Add(Player1);
            alignGame.Children.Add(playingBoard);
            alignGame.Children.Add(Player2);

            
        }

        private void MovePiece(object sender, TappedRoutedEventArgs e)
        {
            //Grid search = FindName("playingBoard") as Grid;
            Ellipse movedPiece = (Ellipse)sender;
            movedPiece.Fill = makeMove.Fill;
            searchForWin();
        }

        private void MoveChoice(object sender, TappedRoutedEventArgs e)
        {
            Ellipse moveThisPeice = (Ellipse)sender;
            makeMove.Fill = moveThisPeice.Fill;
        }

        private void searchForWin()
        {
            String checkWhere = "hello";
            checkWinRed.Fill = new SolidColorBrush(Colors.Red);
            //checking to see if i can find grid
            //Grid gameFound = FindName("rootGrid") as Grid;
            //gridFound.Background = new SolidColorBrush(Colors.Black);
            //search and find all ellipses
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Ellipse movedPiece = searchForWinarray[i, j];

                    movedPiece.Name = "foundPiece" + i + j;
                    //checkWin.Fill = checkWinRed.Fill;

                    movedPiece.Fill = new SolidColorBrush(Colors.Red);
                    //checkWin.SetValue(Grid.RowProperty, i);
                    //checkWin.SetValue(Grid.ColumnProperty, j);
                    //checkWin.Tapped += MovePiece;
                    //Console.WriteLine(checkWhere);                   
                    //gameFound.Children.Add(checkWin);
                    
                }
            }

            

        }
    }
}
