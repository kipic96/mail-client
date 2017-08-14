using System.Windows;
using System.Windows.Controls;
using SimpleCalculator.Enums;

namespace SimpleCalculator
{
    public partial class MainWindow : Window
    {
        private ICalculator _calculator;

        /// <summary>
        /// Initialize window and the fields
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();            
            InitializeFields();
        }
        
        /// <summary>
        /// Initialize all the fields
        /// </summary>
        private void InitializeFields()
        {
            _calculator = new Calculator();            
        }

        /// <summary>
        /// Gets the string from the TextBox boxResult and converts it to double
        /// </summary>
        /// <returns> Returns number from TextBox boxrResult
        /// If it contains comments that operation is infinity or undefined it returns null </returns>
        private double? GetNumberFromBoxResult()
        {
            var currentProgress = _calculator.CheckOperationProgress();
            if (currentProgress == OperationProgress.OnOperationType && boxResult.Text == string.Empty)
                return null;
            if (!boxResult.Text.Contains(MathSymbols.Infinity) 
                && !boxResult.Text.Contains(MathSymbols.ResultUndefined))
                return double.Parse(boxResult.Text);
            else 
                return null;
        }

        /// <summary>
        /// Adds number to the TextBox boxResult
        /// </summary>
        /// <param name="numberToAdd"></param>
        private void AddToNumber(char numberToAdd)
        {
            var currentProgress = _calculator.CheckOperationProgress();
            switch (currentProgress)
            {
                case OperationProgress.None:
                    goto case OperationProgress.OnOperationType;
                case OperationProgress.OnOperationType:
                    if ((boxResult.Text == MathSymbols.ZeroSign 
                        && char.IsDigit(numberToAdd))
                        || boxResult.Text == MathSymbols.Infinity
                        || boxResult.Text == MathSymbols.ResultUndefined)
                    {
                        boxResult.Text = string.Empty;
                    }
                    boxResult.Text += numberToAdd;
                    break;
                case OperationProgress.OnFirstNumber:
                    boxResult.Text = numberToAdd.ToString();
                    _calculator.ClearEverything();
                    break;
                case OperationProgress.OnResult:
                    ClearEverything();
                    _calculator.ClearEverything();
                    boxResult.Text = numberToAdd.ToString();
                    break;
            }
        }

        /// <summary>
        /// Only update the operation type and prepare for writing next number
        /// </summary>
        /// <param name="operationSign"></param>
        /// <param name="operationTypeToSet"></param>
        private void UpdateOperationType(string operationSign, OperationType operationTypeToSet)
        {
            var operationProgress = _calculator.CheckOperationProgress();
            if (operationProgress != OperationProgress.None)
            {
                _calculator.OperationType = operationTypeToSet;
                boxOperation.Text = _calculator.FirstNumber.ToString() + " " + operationSign;
                boxResult.Text = string.Empty;
            }
        }

        /// <summary>
        /// Setting the chosen operation type
        /// </summary>
        /// <param name="operationSign"></param>
        /// <param name="operationTypeToSet"></param>
        private void SetOperation(string operationSign, OperationType operationTypeToSet)
        {
            var currentProgress = _calculator.CheckOperationProgress();
            switch (currentProgress)
            {
                case OperationProgress.None:
                    _calculator.FirstNumber = GetNumberFromBoxResult();
                    break;
                case OperationProgress.OnOperationType:
                    _calculator.SecondNumber = GetNumberFromBoxResult();
                    TryToExecuteOperation();
                    _calculator.ClearSecondNumberAndResult();
                    break;
                case OperationProgress.OnSecondNumber:
                    goto case OperationProgress.OnResult;
                case OperationProgress.OnResult:
                    _calculator.ClearSecondNumberAndResult();
                    break;
            }
            UpdateOperationType(operationSign, operationTypeToSet);
        }

        /// <summary>
        /// Checks the operation progress, gets the number from TexBox boxResult if nessesery
        /// </summary>
        private void TryToExecuteOperation()
        {
            var currentProgress = _calculator.CheckOperationProgress();
            switch (currentProgress)
            {
                case OperationProgress.None:
                    _calculator.FirstNumber = GetNumberFromBoxResult();
                    break;
                case OperationProgress.OnOperationType:
                    _calculator.SecondNumber = GetNumberFromBoxResult();
                    break;
            }
            OperationResult operationResult = _calculator.ExecuteOperation();
            Clear(operationResult);
        }

        /// <summary>
        /// Clears TextBoxes on different operation results cases
        /// </summary>
        /// <param name="operationResult"></param>
        private void Clear(OperationResult operationResult)
        {        
            ClearEverything();
            switch (operationResult)
            {                
                case OperationResult.Good:
                    boxResult.Text = _calculator.Result.ToString();
                    break;
                case OperationResult.Infinity:
                    boxResult.Text = MathSymbols.Infinity;
                    break;
                case OperationResult.Undefined:
                    boxResult.Text = MathSymbols.ResultUndefined;
                    break;
            }
            _calculator.Clear(operationResult);
        }

        /// <summary>
        /// Clears boxOperation and puts in boxResult sign zero
        /// </summary>
        private void ClearEverything()
        {
            boxResult.Text = MathSymbols.ZeroSign;
            boxOperation.Text = string.Empty;
        }

        /// <summary>
        /// Adds dot to a number in boxResult if no dot is there and it is number, not a comment of operation result
        /// </summary>
        private void AddDotToNumber()
        {
            var operationProgress = _calculator.CheckOperationProgress();
            if (boxResult.Text == string.Empty)
                boxResult.Text = MathSymbols.ZeroSign + MathSymbols.Dot;
            else if (!boxResult.Text.Contains(MathSymbols.Dot)
                && operationProgress != OperationProgress.OnResult
                && operationProgress != OperationProgress.OnFirstNumber
                && boxResult.Text != MathSymbols.Infinity
                && boxResult.Text != MathSymbols.ResultUndefined)
            {
                boxResult.Text += MathSymbols.Dot;
            }
        }

        /// <summary>
        /// Do when number is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNumber_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var numberToAdd = char.Parse(button.Content.ToString());
            AddToNumber(numberToAdd);
        }

        /// <summary>
        /// Do when dot sign is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDot_Click(object sender, RoutedEventArgs e)
        {
            AddDotToNumber();
        }

        /// <summary>
        /// Do when divide sign is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDivide_Click(object sender, RoutedEventArgs e)
        {
            SetOperation(((Button)sender).Content.ToString(), OperationType.Division);            
        }

        /// <summary>
        /// Do when multiplication sign is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMultiply_Click(object sender, RoutedEventArgs e)
        {
            SetOperation(((Button)sender).Content.ToString(), OperationType.Multiplication);
        }

        /// <summary>
        /// Do when minus sign is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSubstract_Click(object sender, RoutedEventArgs e)
        {
            SetOperation(((Button)sender).Content.ToString(), OperationType.Substraction);
        }

        /// <summary>
        /// Do when plus sign is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            SetOperation(((Button)sender).Content.ToString(), OperationType.Addition);
        }

        /// <summary>
        /// Do when clear button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            ClearEverything();
            _calculator.ClearEverything();        
        }

        /// <summary>
        /// Do when execute button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExecute_Click(object sender, RoutedEventArgs e)
        {
            TryToExecuteOperation();
        }
    }
}
