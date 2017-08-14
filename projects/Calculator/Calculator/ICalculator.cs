using SimpleCalculator.Enums;

namespace SimpleCalculator
{
    interface ICalculator
    {
        double? FirstNumber { get; set; }
        double? SecondNumber { get; set; }
        double? Result { get; set; }
        OperationType OperationType { get; set; }

        OperationProgress CheckOperationProgress();
        OperationResult ExecuteOperation();
        void Clear(OperationResult operationResult);
        void ClearEverything();
        void ClearSecondNumberAndResult();
    }
}
