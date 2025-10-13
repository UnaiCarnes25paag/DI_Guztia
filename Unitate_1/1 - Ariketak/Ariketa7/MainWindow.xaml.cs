using System;
using System.Windows;
using System.Windows.Controls;

namespace Ariketa7
{
    public partial class MainWindow : Window
    {
        private double _firstNumber = 0;
        private string _operator = "";
        private bool _isOperatorClicked = false;
        private bool _isResultShown = false;

        public MainWindow()
        {
            InitializeComponent();
            Display.Text = "0";
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string number = button.Content.ToString();

            if (Display.Text == "0" || _isOperatorClicked || _isResultShown)
            {
                Display.Text = number;
                _isOperatorClicked = false;
                _isResultShown = false;
            }
            else
            {
                Display.Text += number;
            }
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (_isOperatorClicked || _isResultShown)
            {
                Display.Text = "0.";
                _isOperatorClicked = false;
                _isResultShown = false;
            }
            else if (!Display.Text.Contains("."))
            {
                Display.Text += ".";
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            _firstNumber = double.TryParse(Display.Text, out var num) ? num : 0;
            _operator = button.Content.ToString();
            _isOperatorClicked = true;
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            double secondNumber = double.TryParse(Display.Text, out var num) ? num : 0;
            double result = 0;

            switch (_operator)
            {
                case "+":
                    result = _firstNumber + secondNumber;
                    break;
                case "-":
                    result = _firstNumber - secondNumber;
                    break;
                case "*":
                    result = _firstNumber * secondNumber;
                    break;
                case "/":
                    if (secondNumber == 0)
                    {
                        Display.Text = "Error";
                        _isResultShown = true;
                        return;
                    }
                    result = _firstNumber / secondNumber;
                    break;
                default:
                    result = secondNumber;
                    break;
            }

            Display.Text = result.ToString();
            _isResultShown = true;
        }

        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Display.Text, out var num))
            {
                Display.Text = (num / 100).ToString();
                _isResultShown = true;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.Content.ToString() == "C")
            {
                Display.Text = "0";
                _firstNumber = 0;
                _operator = "";
                _isOperatorClicked = false;
                _isResultShown = false;
            }
            else if (button.Content.ToString() == "CE")
            {
                Display.Text = "0";
            }
        }
    }
}