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

namespace SimpleCalculator
{
   

    public partial class MainWindow : Window, IClearable
    {
        private Calculator _calculator;

        public MainWindow()
        {
            InitializeComponent();
            _calculator = new Calculator();
        }

       private void AddToNumber(TextBox _boxResult, char numberToAdd)
        {
            if (!_calculator.IsOperationJustExecuted || _calculator.OperationType != OperationType.None)
            {
                if (_boxResult.Text == "0" && char.IsDigit(numberToAdd))
                    _boxResult.Text = string.Empty;
                _boxResult.Text += numberToAdd;
            }
            else
            {
                ClearEverything();
                _boxResult.Text = numberToAdd.ToString();
                _calculator.IsOperationJustExecuted = false;
            }            
        }

        private void SetOperationReaction(string operationSign, OperationType operationTypeToSet)
        {
            if (!_calculator.FirstNumber.HasValue)
            {
                _calculator.FirstNumber = double.Parse(_boxResult.Text);
                _calculator.SecondNumber = null;
                _boxResult.Text = "0";
            }
            else if (!_calculator.IsOperationJustExecuted)
            {
                _calculator.SecondNumber = double.Parse(_boxResult.Text);
               // _calculator.ExecuteOperation();
                _calculator.IsOperationJustExecuted = false;
            }
            _calculator.OperationType = operationTypeToSet;
            _boxOperation.Text = _calculator.FirstNumber.Value.ToString() + " " + operationSign;            
        }

        public void ClearAfterDividingByZero()
        {
            const string infinity = "∞";
            _boxOperation.Text = string.Empty;
            _boxResult.Text = infinity;
        }

        public void ClearAfterExecutingOperation()
        {
            _boxResult.Text = _calculator.Result.ToString();
            _boxOperation.Text = string.Empty;
        }

        public void ClearEverything()
        {
            _boxResult.Text = "0";
            _boxOperation.Text = string.Empty;
            _calculator.ClearEverything();
        }

        private void AddDotToNumber()
        {
            const char dot = ',';
            if (!_boxResult.Text.Contains(dot))            
                _boxResult.Text += dot;
        }

        private void buttonNumber_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            char numberToAdd = char.Parse(button.Content.ToString());
            AddToNumber(_boxResult, numberToAdd);
        }

        private void buttonDot_Click(object sender, RoutedEventArgs e)
        {
            AddDotToNumber();
        }

        private void buttonDivide_Click(object sender, RoutedEventArgs e)
        {
            SetOperationReaction(((Button)sender).Content.ToString(), OperationType.Division);            
        }

        private void buttonMultiply_Click(object sender, RoutedEventArgs e)
        {
            SetOperationReaction(((Button)sender).Content.ToString(), OperationType.Multiplication);
        }

        private void buttonSubstract_Click(object sender, RoutedEventArgs e)
        {
            SetOperationReaction(((Button)sender).Content.ToString(), OperationType.Substraction);
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            SetOperationReaction(((Button)sender).Content.ToString(), OperationType.Addition);
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            ClearEverything();            
        }

        private void buttonExecute_Click(object sender, RoutedEventArgs e)
        {
            if (_calculator.FirstNumber.HasValue && !_calculator.SecondNumber.HasValue)
                _calculator.SecondNumber = double.Parse(_boxResult.Text);
            else if (!_calculator.FirstNumber.HasValue)
                _calculator.FirstNumber = double.Parse(_boxResult.Text);
            if (_calculator.ExecuteOperation())
                ClearAfterExecutingOperation();
            else
                ClearAfterDividingByZero();
        }
    }
}
