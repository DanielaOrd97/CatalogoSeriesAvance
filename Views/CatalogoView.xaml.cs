﻿using System;
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
using System.Windows.Shapes;

namespace CatalogoSeries.Views
{
    /// <summary>
    /// Interaction logic for CatalogoView.xaml
    /// </summary>
    public partial class CatalogoView : Window
    {
        public CatalogoView()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Entrada_KeyDown(object sender, KeyEventArgs e)
        {
            //if (Char.IsDigit((char)e.Key))
            //{
            //    e.Handled = false;
            //}

        }
    }
}
