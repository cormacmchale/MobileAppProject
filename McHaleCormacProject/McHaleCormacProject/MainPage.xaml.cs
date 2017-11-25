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
            changeRoot.Background = new SolidColorBrush(Colors.Blue);
            //create the actual objcet
            Grid playingBoard = new Grid();
            playingBoard.Name = "referenceBoard";
            playingBoard.Height = 400;
            playingBoard.Width = 400;
            playingBoard.Background = new SolidColorBrush(Colors.Gray);
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
                    Ellipse moveChoice = new Ellipse();
                    moveChoice.Height = 40;
                    moveChoice.Width = 40;
                    moveChoice.Fill = new SolidColorBrush(Colors.White);
                    moveChoice.SetValue(Grid.RowProperty, i);
                    moveChoice.SetValue(Grid.ColumnProperty, j);
                    playingBoard.Children.Add(moveChoice);
                }
            }

            changeRoot.Children.Add(playingBoard);            
            //create canvas to choose piece to play
            Canvas chooseMove = new Canvas();
            changeRoot.Children.Add(chooseMove);
        }

    }

}
