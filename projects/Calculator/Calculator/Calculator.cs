using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    class Calculator : IClearable
    {
        public double? FirstNumber { get; set; } = null;
        public double? SecondNumber { get; set; } = null;
        public double? Result { get; set; } = null;
        public OperationType OperationType { get; set; } = OperationType.None;
        public bool IsOperationJustExecuted { get; set; } = false;

        /* Returns true if Result is not Infinity */
        public bool ExecuteOperation()
        {
            bool isResultInfinity = false;
            if (FirstNumber.HasValue)
            {
                if (!SecondNumber.HasValue)
                    Result = FirstNumber;
                else
                {
                    switch (OperationType)
                    {
                        case OperationType.Addition:
                            Result = FirstNumber + SecondNumber;
                            break;
                        case OperationType.Substraction:
                            Result = FirstNumber - SecondNumber;
                            break;
                        case OperationType.Multiplication:
                            Result = FirstNumber * SecondNumber;
                            break;
                        case OperationType.Division:
                            if (SecondNumber != 0)
                                Result = FirstNumber / SecondNumber;
                            else
                                isResultInfinity = true;
                            break;
                        case OperationType.None:
                            Result = FirstNumber;
                            break;
                    }
                }
            }
            if (isResultInfinity)
            {
                ClearAfterDividingByZero();
                return false;
            }
            else
            {
                ClearAfterExecutingOperation();
                return true;
            }
        }

        public void ClearAfterDividingByZero()
        {
            FirstNumber = null;
            SecondNumber = null;
            OperationType = OperationType.None;
            Result = null;
            IsOperationJustExecuted = true;
        }

        public void ClearAfterExecutingOperation()
        {
            FirstNumber = Result;
            IsOperationJustExecuted = true;
        }

        public void ClearEverything()
        {
            FirstNumber = null;
            SecondNumber = null;
            OperationType = OperationType.None;
            Result = null;
            IsOperationJustExecuted = false;
        }
    }
}
