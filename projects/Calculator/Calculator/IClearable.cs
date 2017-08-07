using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    interface IClearable
    {
        void Clear(OperationResult operationResult);
        void ClearEverything();
    }
}
