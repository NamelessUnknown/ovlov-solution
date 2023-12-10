using System;
using System.Collections.Generic;
using System.Text;

namespace congestion.calculator
{
    public class Emergency : IVehicle

    {
        public string GetVehicleType()
        {
            return "Emergency";
        }
    }
}
