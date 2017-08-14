using SimpleCalculator.Enums;

namespace SimpleCalculator
{
    class Calculator : ICalculator
    {
        public double? FirstNumber { get; set; }
        public double? SecondNumber { get; set; }
        public double? Result { get; set; }
        public OperationType OperationType { get; set; } = OperationType.None;

        /// <summary>
        /// Check at which moment of program we are, which members of Calculator do we have and which are null (we dont have)
        /// </summary>
        /// <returns> Returns moment in the program in enum </returns>
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

        /// <summary> 
        /// Executes operation and returns type of operation result in enum number
        /// </summary>
        public OperationResult ExecuteOperation()
        {
            var currentProgress = CheckOperationProgress();
            if (currentProgress == OperationProgress.None)
                return OperationResult.None;
            if (currentProgress == OperationProgress.OnFirstNumber || currentProgress == OperationProgress.OnOperationType)
            {
                Result = FirstNumber;
                return OperationResult.Good;
            }
            else
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
            return OperationResult.None;
        }

        /// <summary>
        /// Clears the properties on different cases of result of operation
        /// </summary>
        /// <param name="operationResult"></param>
        public void Clear(OperationResult operationResult)
        {            
            switch (operationResult)
            {
                case OperationResult.Good:
                    FirstNumber = Result;
                    break;
                case OperationResult.Infinity:
                    ClearEverything();
                    break;
                case OperationResult.Undefined:
                    ClearEverything();
                    break;
            }
        }

        /// <summary>
        /// Clears all the properties
        /// </summary>
        public void ClearEverything()
        {
            FirstNumber = null;
            SecondNumber = null;
            OperationType = OperationType.None;
        }

        /// <summary>
        /// Clears only SecondNumber and ResultNumber properties
        /// </summary>
        public void ClearSecondNumberAndResult()
        {
            SecondNumber = null;
            Result = null;
        }
    }
}
