﻿using System;
using System.Globalization;
using TollFeeCalculator;

public class TollCalculator
{

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        int tempFee = 0;
        int nextFee = 0;

        if (vehicle == null || dates == null || dates.Length == 0) return 0;

        foreach (DateTime date in dates)
        {
            if (date < intervalStart)
            {
                throw new ArgumentException($"Dates must be in chronological order. Found {date} before {intervalStart}.");
            }
            nextFee = GetTollFee(date, vehicle);
            tempFee = GetTollFee(intervalStart, vehicle);
            
            if (date.Date != intervalStart.Date)
            {
                totalFee = Math.Min(totalFee, 60);
                intervalStart = date;         // Start a new day    
                tempFee = 0;
            }

            double diffInMinutes = (date - intervalStart).TotalMinutes;

            if (diffInMinutes <= 60)
            {   
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                    
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
                intervalStart = date;
            }
        }
        return totalFee;
    }

    private Boolean IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        String vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        // Use ranges and conditions for better clarity
        return (hour, minute) switch
        {
            (6, >= 0 and <= 29) => 8,
            (6, >= 30 and <= 59) => 13,
            (7, _) => 18,
            (8, >= 0 and <= 29) => 13,
            (8, >= 30 and <= 59) => 8,
            ( >= 9 and <= 14, _) => 8, // Has been changed to be consistent insead of 0 every half hour
            (15, >= 0 and <= 30) => 13,
            (15, >= 30 and <= 59) => 18,
            (16, _) => 18,
            (17, _) => 13,
            (18, >= 0 and <= 29) => 8,
            _ => 0, // Default case for toll-free hours
        };
    }


    private bool IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        //Below Needs to be changed to handle every year, preferably import a library. 
        //Import the whole month of july and add Valborg

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
    //Its probably fine, perhaps research if there are better alternatives to using enums
    private enum TollFreeVehicles
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}