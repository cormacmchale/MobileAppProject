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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TillingAppProject
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



        private void addTilingGrid()
        {
            // remove the old grid
            try
            {
                rootGrid.Children.Remove(FindName("tileGrid") as Grid);
            }
            catch { }
            int sizeofgrid = Convert.ToInt32(SizeOfGrid.Text.ToString());
            int sizeofTile = Convert.ToInt32(SizeOfTiles.Text.ToString());
            //create grid object
            Grid tileTemplate = new Grid();
            //name, size, allignment,, bacrkound colour
            tileTemplate.Name = "tileGrid";
            tileTemplate.HorizontalAlignment = HorizontalAlignment.Center;
            tileTemplate.VerticalAlignment = VerticalAlignment.Center;
            //tileTemplate.Height = _rowHeight; //100 * _rows;
            //tileTemplate.Width = +_rowHeight;// 100 * _rows;
            tileTemplate.Background = new SolidColorBrush(Colors.Gray);
            tileTemplate.Margin = new Thickness(5);
            tileTemplate.SetValue(Grid.RowProperty, 1);
            //tileTemplate.Background = {ApplicationTheme};
            tileTemplate.Height = sizeofgrid; //100 * _rows;
            tileTemplate.Width = +sizeofgrid;// 100 * _rows;
            tileTemplate.Background = new SolidColorBrush(Colors.Gray);
            tileTemplate.Margin = new Thickness(5);
            tileTemplate.SetValue(Grid.RowProperty, 1);

            //add row and column definitions
            for (int i = 0; i < sizeofgrid; i++)
            {
                tileTemplate.RowDefinitions.Add(new RowDefinition());
                tileTemplate.ColumnDefinitions.Add(new ColumnDefinition());
            }
            // add chessboard to root grid
            rootGrid.Children.Add(tileTemplate);
        }

        private void generateGrid_Click_1(object sender, RoutedEventArgs e)
        {
             addTilingGrid();
        }
    }
}
