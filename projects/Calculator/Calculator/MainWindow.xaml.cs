using System.Windows;
using System.Windows.Controls;

namespace SimpleCalculator
{
    public partial class MainWindow : Window, IClearable
    {
        private Calculator _calculator;

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
            if (currentProgress == OperationProgress.None || currentProgress == OperationProgress.OnOperationType)
            {
                if ((_boxResult.Text == MathSymbols.ZeroSign && char.IsDigit(numberToAdd)) || _boxResult.Text == MathSymbols.Infinity || _boxResult.Text == MathSymbols.ResultUndefined)
                    _boxResult.Text = string.Empty;
                _boxResult.Text += numberToAdd;
            }
            else if (currentProgress == OperationProgress.OnFirstNumber)
            {
                _boxResult.Text = numberToAdd.ToString();
                _calculator.ClearEverything();
            }
            else if (currentProgress == OperationProgress.OnSecondNumber)
            {
                // nothing else here in this condition
            }
            else if (currentProgress == OperationProgress.OnResult)
            {
                ClearEverything();
                _calculator.ClearEverything();
                _boxResult.Text = numberToAdd.ToString();
            }
        }

        // only update the operation type
        private void UpdateOperationType(string operationSign, OperationType operationTypeToSet)
        {
            OperationProgress operationProgress = _calculator.CheckOperationProgress();
            if (operationProgress != OperationProgress.None)
            {
                _calculator.OperationType = operationTypeToSet;
                _boxOperation.Text = _calculator.FirstNumber.ToString() + " " + operationSign;
                _boxResult.Text = MathSymbols.ZeroSign;
            }
        }

        // reaction for choosing one of the operation signs
        private void SetOperation(string operationSign, OperationType operationTypeToSet)
        {
            OperationProgress currentProgress = _calculator.CheckOperationProgress();
            if (currentProgress == OperationProgress.None)
            {
                _calculator.FirstNumber = double.Parse(_boxResult.Text);                
            }
            else if (currentProgress == OperationProgress.OnFirstNumber)
            {
                // nothing else here in this condition
            }
            else if (currentProgress == OperationProgress.OnOperationType)
            {
                _calculator.SecondNumber = double.Parse(_boxResult.Text);
                TryToExecuteOperation();
                _calculator.ClearSecondNumberAndResult();
            }
            else if (currentProgress == OperationProgress.OnResult || currentProgress == OperationProgress.OnSecondNumber)
            {
                _calculator.SecondNumber = null;
                _calculator.Result = null;
            }
            UpdateOperationType(operationSign, operationTypeToSet);
        }

        public void TryToExecuteOperation()
        {
            OperationProgress currentProgress = _calculator.CheckOperationProgress();
            if (currentProgress == OperationProgress.None)
            {
                _calculator.FirstNumber = double.Parse(_boxResult.Text);
            }
            else if (currentProgress == OperationProgress.OnFirstNumber)
            {
                // nothing else here in this condition
            }
            else if (currentProgress == OperationProgress.OnOperationType)
            {
                _calculator.SecondNumber = double.Parse(_boxResult.Text);
            }
            else if (currentProgress == OperationProgress.OnSecondNumber)
            {
                // nothing else here in this condition
            }
            else if (currentProgress == OperationProgress.OnResult)
            {
                // nothing else here in this condition
            }

            OperationResult operationResult = _calculator.ExecuteOperation();
            Clear(operationResult);
        }

        public void Clear(OperationResult operationResult)
        {           
            
            ClearEverything();
            switch (operationResult)
            {
                case OperationResult.None:
                    // nothing else here in this condition
                    break;
                case OperationResult.Good:
                    _boxResult.Text = _calculator.Result.ToString();
                    break;
                case OperationResult.Infinity:
                    _boxResult.Text = MathSymbols.Infinity;
                    break;
                case OperationResult.Undefined:
                    _boxResult.Text = MathSymbols.ResultUndefined;
                    break;
            }
            _calculator.Clear(operationResult);
        }

        public void ClearEverything()
        {
            _boxResult.Text = MathSymbols.ZeroSign;
            _boxOperation.Text = string.Empty;
        }

        private void AddDotToNumber()
        {
            OperationProgress operationProgress = _calculator.CheckOperationProgress();
            if (!_boxResult.Text.Contains(MathSymbols.Dot) && operationProgress != OperationProgress.OnResult && operationProgress != OperationProgress.OnFirstNumber)            
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
            SetOperation(((Button)sender).Content.ToString(), OperationType.Division);            
        }

        private void buttonMultiply_Click(object sender, RoutedEventArgs e)
        {
            SetOperation(((Button)sender).Content.ToString(), OperationType.Multiplication);
        }

        private void buttonSubstract_Click(object sender, RoutedEventArgs e)
        {
            SetOperation(((Button)sender).Content.ToString(), OperationType.Substraction);
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            SetOperation(((Button)sender).Content.ToString(), OperationType.Addition);
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            ClearEverything();
            _calculator.ClearEverything();        
        }

        private void buttonExecute_Click(object sender, RoutedEventArgs e)
        {
            TryToExecuteOperation();
        }
    }
}
