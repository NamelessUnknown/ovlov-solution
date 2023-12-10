using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using congestion.calculator;
using congestion.calculator.Contracts;
using congestion.calculator.Models;

public class CongestionTaxCalculator : ICongestionTaxCalculator
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */

    public Dictionary<string, int> GetTax(IVehicle vehicle, DateTime[] dates)
    {
        Dictionary<string, int> resultDictionary = new Dictionary<string, int>();
        DateTime intervalStart = dates[0];

        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            long diffInMillies = (long)(date - intervalStart).TotalMilliseconds;
            long minutes = diffInMillies / 1000 / 60;

            if (minutes < 60)
            {
                string dayKey = intervalStart.ToString("yyyy-MM-dd");

                if (resultDictionary.ContainsKey(dayKey))
                {
                    resultDictionary[dayKey] -= tempFee;
                }

                if (nextFee >= tempFee)
                {
                    tempFee = nextFee;
                }

                if (resultDictionary.ContainsKey(dayKey))
                {
                    resultDictionary[dayKey] += tempFee;
                }
                else
                {
                    resultDictionary.Add(dayKey, tempFee);
                }
            }
            else
            {
                string dayKey = date.ToString("yyyy-MM-dd");

                if (resultDictionary.ContainsKey(dayKey))
                {
                    resultDictionary[dayKey] += nextFee;
                }
                else
                {
                    resultDictionary.Add(dayKey, nextFee);
                }
            }

            intervalStart = date;
        }

        foreach (var key in resultDictionary.Keys.ToList())
        {
            if (resultDictionary[key] > 60)
            {
                resultDictionary[key] = 60;
            }
        }

        return resultDictionary;
    }


    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle == null) return false;
        String vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorcycle.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Buss.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }

    public int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }

    public object ConvertStringToVehicle(string vehicle)
    {
        switch (vehicle.ToLower())
        {
            case "car":
                return new Car();
            case "buss":
                return new Buss();
            case "diplomat":
                return new Diplomat();
            case "emergency":
                return new Emergency();
            case "foreign":
                return new Foreign();
            case "military":
                return new Military();
            case "motorcycle":
                return new Motorcycle();
            case "tractor":
                return new Tractor();
            default:
                throw new ArgumentException($"Unknown vehicle type: {vehicle}");
        }
    }

    public DateTime[] ConvertStringArrayIntoDateArray(string[] dates)
    {

        List<DateTime> dateTimeList = new List<DateTime>();

        foreach (string date in dates)
        {
            DateTime parsedDate;
            bool isValid = DateTime.TryParseExact(date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);

            if (isValid)
            {
                dateTimeList.Add(parsedDate);
            }
            else
            {
                throw new ArgumentException($"Provided string: {date} does not seem to have valid 8601 datetime");
            }
        }

        return  dateTimeList.ToArray();
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }

    private enum TollFreeVehicles
    {
        Motorcycle = 0,
        Buss = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}