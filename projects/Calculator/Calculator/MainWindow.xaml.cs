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

       // private MathSymbols _symbols;

        public MainWindow()
        {
            InitializeComponent();            
            InitializeFields();
        }
        
        private void InitializeFields()
        {
            _calculator = new Calculator();            
        }       

        // adds number to the box where we write numbers 
        private void AddToNumber(char numberToAdd)
        {
            OperationProgress currentProgress = _calculator.CheckOperationProgress();
            if (currentProgress == OperationProgress.None || currentProgress == OperationProgress.OnOperationType /*|| currentProgress == OperationProgress.OnFirstNumber*/)
            {
                if (_boxResult.Text == MathSymbols.ZeroSign && char.IsDigit(numberToAdd))
                    _boxResult.Text = string.Empty;
                _boxResult.Text += numberToAdd;
            }
            else if (currentProgress == OperationProgress.OnResult)
            {
                ClearEverything();
                _boxResult.Text = numberToAdd.ToString();
               // _calculator.IsOperationJustExecuted = false;
            }
        }

        // only update the operation type
        private void UpdateOperationType(string operationSign, OperationType operationTypeToSet)
        {
            _calculator.OperationType = operationTypeToSet;
            _boxOperation.Text = _calculator.FirstNumber.Value.ToString() + " " + operationSign;
            _boxResult.Text = MathSymbols.ZeroSign;
        }

        // reaction for choosing one of the operation signs
        private void SetOperationReaction(string operationSign, OperationType operationTypeToSet)
        {
            OperationProgress currentProgress = _calculator.CheckOperationProgress();
            if (currentProgress == OperationProgress.None)
            {
                _calculator.FirstNumber = double.Parse(_boxResult.Text);                
            }
            else if (currentProgress == OperationProgress.OnFirstNumber)
            {
                // just update the operation type like below, nothing else here in this condition
            }
            else if (currentProgress == OperationProgress.OnOperationType)
            {
                _calculator.SecondNumber = double.Parse(_boxResult.Text);
                //TryToExecuteOperation();
                // TODO: how to do the closing the second number and continous operations?
            }
            else if (currentProgress == OperationProgress.OnResult)
            {
                _calculator.SecondNumber = null;
                _calculator.Result = null;
            }
            UpdateOperationType(operationSign, operationTypeToSet);



            /*if (!_calculator.FirstNumber.HasValue)
            {
                _calculator.FirstNumber = double.Parse(_boxResult.Text);
                _calculator.SecondNumber = null;
                _boxResult.Text = MathSymbols.ZeroSign;
            }
            else if (!_calculator.IsOperationJustExecuted)
            {
                _calculator.SecondNumber = double.Parse(_boxResult.Text);
               // _calculator.ExecuteOperation();
                _calculator.IsOperationJustExecuted = false;
            }
            _calculator.OperationType = operationTypeToSet;
            _boxOperation.Text = _calculator.FirstNumber.Value.ToString() + " " + operationSign; */
        }

        public void TryToExecuteOperation()
        {
            OperationResult operationResult = _calculator.ExecuteOperation();
            Clear(operationResult);
        }

        /*public void ClearAfterDividingByZero()
        {            
            _boxOperation.Text = string.Empty;
            _boxResult.Text = MathSymbols.Infinity;
            //_calculator.ClearAfterDividingByZero();
        }

        public void ClearAfterExecutingOperation()
        {
            _boxResult.Text = _calculator.Result.ToString();
            _boxOperation.Text = string.Empty;
            //_calculator.ClearAfterExecutingOperation();
        }*/

        public void Clear(OperationResult operationResult)
        {
            _calculator.Clear(operationResult);
        }

        public void ClearEverything()
        {
            _boxResult.Text = MathSymbols.ZeroSign;
            _boxOperation.Text = string.Empty;
            _calculator.ClearEverything();
        }

        private void AddDotToNumber()
        {            
            if (!_boxResult.Text.Contains(MathSymbols.Dot))            
                _boxResult.Text += MathSymbols.Dot;
        }

        private void buttonNumber_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            char numberToAdd = char.Parse(button.Content.ToString());
            AddToNumber(numberToAdd);
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
            /*TODO: przerobic tak jak wyzej z uzyciem OperationProgress
             * 
             * if (_calculator.FirstNumber.HasValue && !_calculator.SecondNumber.HasValue)
                _calculator.SecondNumber = double.Parse(_boxResult.Text);
            else if (!_calculator.FirstNumber.HasValue)
                _calculator.FirstNumber = double.Parse(_boxResult.Text);
            */
        }
    }
}
