﻿using System;
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

        // check at which moment of program we are, which variables do we have
        public OperationProgress CheckOperationProgress()
        {
            if (FirstNumber.HasValue)
            {
                if (OperationType != OperationType.None)
                {
                    if (SecondNumber.HasValue)
                    {
                        if (Result.HasValue)
                            return OperationProgress.OnResult;
                        else
                            return OperationProgress.OnSecondNumber;
                    }
                    else
                        return OperationProgress.OnOperationType;
                }
                else
                    return OperationProgress.OnFirstNumber;
            }
            else
                return OperationProgress.None;
        }

        /* Returns operation result in enum number */
        public OperationResult ExecuteOperation()
        {
            OperationProgress currentProgress = CheckOperationProgress();
            if (currentProgress == OperationProgress.None)
                return OperationResult.None;
            else
            {
                if (currentProgress == OperationProgress.OnFirstNumber || currentProgress == OperationProgress.OnOperationType)                
                {
                    Result = FirstNumber;
                    return OperationResult.Good;
                } 
                else // OnSecondNumber || OnResult
                {
                    switch (OperationType)
                    {
                        case OperationType.Addition:
                            Result = FirstNumber + SecondNumber;
                            return OperationResult.Good;

                        case OperationType.Substraction:
                            Result = FirstNumber - SecondNumber;
                            return OperationResult.Good;

                        case OperationType.Multiplication:
                            Result = FirstNumber * SecondNumber;
                            return OperationResult.Good;

                        case OperationType.Division:
                            if (FirstNumber == 0 && SecondNumber == 0)
                                return OperationResult.Undefined;
                            else if (SecondNumber == 0)
                                return OperationResult.Infinity;     
                            Result = FirstNumber / SecondNumber;
                            return OperationResult.Good;
                    }
                }
            }
            return OperationResult.None;
        }

        /*public void ClearAfterDividingByZero()
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
        }*/

        public void Clear(OperationResult operationResult)
        {
            switch (operationResult)
            {
                case OperationResult.None:
                    // TO DO not sure if this should be 
                    ClearEverything(); 
                    break;
                case OperationResult.Good:
                    FirstNumber = Result;
                    Result = null;
                    break;
                case OperationResult.Infinity:
                    ClearEverything();
                    break;
                case OperationResult.Undefined:
                    ClearEverything();
                    break;
            }
        }

        public void ClearEverything()
        {
            FirstNumber = null;
            SecondNumber = null;
            OperationType = OperationType.None;
            Result = null;
        }
    }
}