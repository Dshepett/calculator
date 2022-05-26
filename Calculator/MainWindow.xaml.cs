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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        double saveNum;

        double newNum;

        string operSign;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            if ((Result.Text == "0") || (Result.Text == double.NaN.ToString()) || (Result.Text == double.PositiveInfinity.ToString()))
                Result.Text = (sender as Button).Content.ToString();
            else
                Result.Text += (sender as Button).Content;
        }

        private void ButDot_Click(object sender, RoutedEventArgs e)
        {
            if((Result.Text == double.NaN.ToString()) || (Result.Text == double.PositiveInfinity.ToString()))
            {
                Result.Text = "0,";
                return;
            }
            if (!Result.Text.Contains(","))
                Result.Text += ",";
        }

        private void ButAC_Click(object sender, RoutedEventArgs e)
        {
            Result.Text = "0";
            newNum = saveNum = 0;
        }

        private void ButPlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Result.Text, out newNum))
            {
                if (Result.Text != "0")
                {
                    newNum = newNum * (-1);
                    Result.Text = newNum.ToString();
                }
            }
        }

        private void ButInverse_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Result.Text, out newNum))
            {
                if (newNum == 0)
                    Result.Text = $"{double.PositiveInfinity}";
                else
                    Result.Text = $"{1 / newNum}";
            }
        }

        private void ButSqrt_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Result.Text, out newNum))
            {
                if (newNum < 0)
                    Result.Text = $"{double.NaN}";
                else
                    Result.Text = $"{Math.Sqrt(newNum)}";
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            if (Result.Text == ",")
                newNum = 0;
            else
                newNum = double.Parse(Result.Text);
            saveNum = newNum;
            Result.Text = "0";
            Button but = (Button)sender;
            operSign = but.Content.ToString();
        }

        private void ButEquel_Click(object sender, RoutedEventArgs e)
        {
            if (operSign == "")
                return;
            if (Result.Text == ",")
                newNum = 0;
            else
                newNum = double.Parse(Result.Text);
            switch (operSign)
            {
                case "/":
                    if (newNum == 0)
                    {
                        Result.Text = double.PositiveInfinity.ToString();
                        return;
                    }
                    saveNum /= newNum;
                    break;
                case "*":
                    saveNum *= newNum;
                    break;
                case "+":
                    saveNum += newNum;
                    break;
                case "-":
                    saveNum -= newNum;
                    break;
            }
            Result.Text = saveNum.ToString();
            operSign = "";
        }


        List<Key> keys = new List<Key> {Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3,
            Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9,
            Key.D0,Key.D1,Key.D2,Key.D3,Key.D4,Key.D5,Key.D6,Key.D7,Key.D8,Key.D9};

        private void Result_KeyDown(object sender, KeyEventArgs e)
        {
            bool flag = false;
            if ((Result.Text == "0") || (Result.Text == double.NaN.ToString()) || (Result.Text == double.PositiveInfinity.ToString()))
                flag = true;
            Key k = e.Key;            
            if (k == Key.OemComma)
            {
                if (Result.Text.Contains(","))
                    e.Handled = true;
                else
                {
                    if (flag)
                    {
                        Result.Text = "0,";
                        e.Handled = true;
                    }
                        
                }                    
                return;
            }
            if (!(keys.Contains(k)) || (k == Key.Space))
                e.Handled = true;
            else
            {
                if (flag)
                    Result.Text = "";
            }

        }
    }
}
