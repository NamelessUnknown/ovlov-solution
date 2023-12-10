using System;
using System.Collections.Generic;
using System.Text;

namespace congestion.calculator.Contracts
{
    public interface ICongestionTaxCalculator
    {
        Dictionary<string, int> GetTax(IVehicle vehicle, DateTime[] dates);
        int GetTollFee(DateTime date, IVehicle vehicle);
        object ConvertStringToVehicle(string vehicle);
        DateTime[] ConvertStringArrayIntoDateArray(string[] dates);
    }
}
