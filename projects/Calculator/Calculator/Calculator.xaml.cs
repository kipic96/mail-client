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
    public enum OperationType
    {
        none = 0,
        adding,
        substracting,
        multiplying,
        dividing
    }

    public partial class Calculator : Window
    {
        private double firstNumber = 0;
        private double secondNumber = 0;
        private double result = 0;
        private OperationType operationType = OperationType.none;
        private bool isFirstNumberEntered = false;
        private bool isOperationJustExecuted = false;

        public Calculator()
        {
            InitializeComponent();
        }

        private void addToNumber(TextBox boxResult, char numberToAdd)
        {
            if (!isOperationJustExecuted)
            {
                if (boxResult.Text == "0" && char.IsDigit(numberToAdd))
                    boxResult.Text = string.Empty;
                boxResult.Text += numberToAdd;
            }
            else
            {
                //clearEverything();
                boxResult.Text = numberToAdd.ToString();
                isOperationJustExecuted = false;               
            }
        }

        private void executeOperation()
        {
            if (!isFirstNumberEntered)
                firstNumber = double.Parse(boxResult.Text);
            else
            {
                bool isResultInfinity = false;
                
                if (!isOperationJustExecuted)
                    secondNumber = double.Parse(boxResult.Text);
                switch (operationType)
                {
                    case OperationType.adding:
                        result = firstNumber + secondNumber;
                        break;
                    case OperationType.substracting:
                        result = firstNumber - secondNumber;
                        break;
                    case OperationType.multiplying:
                        result = firstNumber * secondNumber;
                        break;
                    case OperationType.dividing:
                        if (secondNumber != 0)
                            result = firstNumber / secondNumber;
                        else
                        {
                            result = 0;
                            isResultInfinity = true;
                        }
                        break;
                    case OperationType.none:
                        result = firstNumber;
                        break;
                 }
                if (isResultInfinity)
                {
                    clearAfterDividingByZero();
                }
                else
                {
                    boxResult.Text = result.ToString();
                    clearAfterExecutingOperation();
                }
            } 
        }

        private void setOperationReaction(string operationSign, OperationType operationTypeToSet)
        {
            if (!isFirstNumberEntered)
            {
                firstNumber = double.Parse(boxResult.Text);
                isFirstNumberEntered = true;
                boxResult.Text = "0";
            }
            else if (!isOperationJustExecuted)
            {
                secondNumber = double.Parse(boxResult.Text);
                executeOperation();                
            }
            operationType = operationTypeToSet;
            boxOperation.Text = firstNumber.ToString() + " " + operationSign;
        }

        private void clearAfterDividingByZero()
        {
            const string infinity = "∞";
            firstNumber = 0;
            secondNumber = 0;
            operationType = OperationType.none;
            result = 0;
            isFirstNumberEntered = false;
            boxOperation.Text = string.Empty;
            boxResult.Text = infinity;
            isOperationJustExecuted = true;
        }

        private void clearAfterExecutingOperation()
        {
            firstNumber = result;       
            boxResult.Text = result.ToString();
            boxOperation.Text = string.Empty;
            isOperationJustExecuted = true;
        }

        private void clearEverything()
        {
            firstNumber = 0;
            secondNumber = 0;
            operationType = OperationType.none;
            result = 0;
            isFirstNumberEntered = false;
            boxResult.Text = "0";
            boxOperation.Text = string.Empty;
            isOperationJustExecuted = false;
        }

        private void addDotToNumber()
        {
            const char dot = ',';
            if (!boxResult.Text.Contains(dot))            
                boxResult.Text += dot;
        }

        private void buttonNumber_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            char numberToAdd = char.Parse(button.Content.ToString());
            addToNumber(boxResult, numberToAdd);
        }

        private void buttonDot_Click(object sender, RoutedEventArgs e)
        {
            addDotToNumber();
        }

        private void buttonDivide_Click(object sender, RoutedEventArgs e)
        {
            setOperationReaction(((Button)sender).Content.ToString(), OperationType.dividing);            
        }

        private void buttonMultiply_Click(object sender, RoutedEventArgs e)
        {
            setOperationReaction(((Button)sender).Content.ToString(), OperationType.multiplying);
        }

        private void buttonSubstract_Click(object sender, RoutedEventArgs e)
        {
            setOperationReaction(((Button)sender).Content.ToString(), OperationType.substracting);
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            setOperationReaction(((Button)sender).Content.ToString(), OperationType.adding);
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            clearEverything();
        }

        private void buttonExecute_Click(object sender, RoutedEventArgs e)
        {
            executeOperation();
        }
    }
}
