using System;
using Xunit;
using TollFeeCalculator;

namespace toll_calculator.Tests
{
    public class TollCalculatorTests
    {

        TollCalculator _tollCalculator;
        Vehicle _car;
        Vehicle _truck;
        Vehicle _diplomat;
        Vehicle _emergency;
        Vehicle _foreign;
        Vehicle _military;
        Vehicle _motorbike;
        Vehicle _tractor;


        public TollCalculatorTests()
        { 
            _tollCalculator = new TollCalculator();
            //regular vehicles
            _car = new Car();
            _tractor = new Tractor();
            //toll free vehicles            
            _truck = new Truck();
            _diplomat = new Diplomat();
            _emergency = new Emergency();
            _foreign = new Foreign();
            _military = new Military();
            _motorbike = new Motorbike();    
        }


        [Theory]
        [InlineData("2024-12-04T00:00:00", "2024-12-04T05:59:59", "2024-12-04T06:00:00", "2024-12-04T10:14:05", "2024-12-04T10:35:00", "2024-12-04T16:17:00", 34)]
        public void GetTollFee_ShouldReturnFeeForTruck(string dateTime1, string dateTime2, string dateTime3, string dateTime4, string dateTime5, string dateTime6, int expectedFee)
        {
            // Arrange
            var listOfDates = new[]
            {
                DateTime.Parse(dateTime1),
                DateTime.Parse(dateTime2),
                DateTime.Parse(dateTime3),
                DateTime.Parse(dateTime4),
                DateTime.Parse(dateTime5),
                DateTime.Parse(dateTime6),
            };

            // Act
            var result = _tollCalculator.GetTollFee(_truck, listOfDates);

            // Assert
            Assert.Equal(expectedFee, result);
        }

        [Theory]
        [InlineData("2024-12-04T05:45:00", "2024-12-04T05:59:59", "2024-12-04T06:00:00", 8)]
        public void GetTollFee_ShouldReturnFeeForCarEdgeCaseMorning(string dateTime1, string dateTime2, string dateTime3, int expectedFee)
        {
            // Arrange
            var listOfDates = new[]
            {
                DateTime.Parse(dateTime1),
                DateTime.Parse(dateTime2),
                DateTime.Parse(dateTime3),
            };

            // Act
            var result = _tollCalculator.GetTollFee(_car, listOfDates);

            // Assert
            Assert.Equal(expectedFee, result);
        }

        [Theory]
        [InlineData("2024-12-04T18:15:00", "2024-12-04T18:30:09", 8)]
        public void GetTollFee_ShouldReturnFeeForCarEdgeCaseEvening(string dateTime1, string dateTime2, int expectedFee)
        {
            // Arrange
            var listOfDates = new[]
            {
                DateTime.Parse(dateTime1),
                DateTime.Parse(dateTime2),
            };

            // Act
            var result = _tollCalculator.GetTollFee(_car, listOfDates);

            // Assert
            Assert.Equal(expectedFee, result);
        }

        [Theory] // 2024-12-04 : = 75 => 60 , 2024-12-05: = 18
                 // =>  60 + 13 = 73 
        [InlineData("2024-12-04T06:15:00", "2024-12-04T06:30:09", "2024-12-04T07:20:09", "2024-12-04T08:21:09", "2024-12-04T16:20:09", "2024-12-04T17:30:09", "2024-12-05T17:30:09", 73 )] 
        public void GetTollFee_ShouldReturnCorrectFeeOverMultipleDays(string dateTime1, string dateTime2, string dateTime3, string dateTime4, string dateTime5, string dateTime6, string dateTime7, int expectedFee)
        {
            // Arrange
            var listOfDates = new[]
            {
                DateTime.Parse(dateTime1),
                DateTime.Parse(dateTime2),
                DateTime.Parse(dateTime3),
                DateTime.Parse(dateTime4),
                DateTime.Parse(dateTime5),
                DateTime.Parse(dateTime6),
                DateTime.Parse(dateTime7),
            };

            // Act
            var result = _tollCalculator.GetTollFee(_car, listOfDates);

            // Assert
            Assert.Equal(expectedFee, result);
        }

        [Theory] // 2024-12-04 : = 75 => 60 , 2024-12-05: = 18
                 // =>  60 + 18 = 78 
        [InlineData("2024-12-04T06:15:00", "2024-12-04T06:30:09", "2024-12-04T07:20:09", "2024-12-04T08:21:09", "2024-12-04T16:20:09", "2024-12-04T17:25:09", "2024-12-05T18:30:00", 60)]
        public void GetTollFee_ShouldNotExceed60ForOneDay(string dateTime1, string dateTime2, string dateTime3, string dateTime4, string dateTime5, string dateTime6, string dateTime7, int expectedFee)
        {
            // Arrange
            var listOfDates = new[]
            {
                DateTime.Parse(dateTime1),
                DateTime.Parse(dateTime2),
                DateTime.Parse(dateTime3),
                DateTime.Parse(dateTime4),
                DateTime.Parse(dateTime5),
                DateTime.Parse(dateTime6),
                DateTime.Parse(dateTime7),
            };

            // Act
            var result = _tollCalculator.GetTollFee(_car, listOfDates);

            // Assert
            Assert.Equal(expectedFee, result);
        }


        [Theory] 
        [InlineData( "2024-12-04T06:30:09", "2024-12-04T07:20:09", "2024-12-04T08:21:09", "2024-12-04T08:32:09", 18 + 13)]
        public void GetTollFee_ShouldChangeIntervalStart(string dateTime1, string dateTime2, string dateTime3, string dateTime4, int expectedFee)
        {
            // Arrange
            var listOfDates = new[]
            {
                DateTime.Parse(dateTime1),
                DateTime.Parse(dateTime2),
                DateTime.Parse(dateTime3),
                DateTime.Parse(dateTime4),
            };

            // Act
            var result = _tollCalculator.GetTollFee(_car, listOfDates);

            // Assert
            Assert.Equal(expectedFee, result);
        }

        //Test over several days

        /*

                [Theory]
                [InlineData("2024-12-04T00:00:00")] 
                [InlineData("2024-12-04T00:00:00")] 
                [InlineData("2024-12-04T00:00:00")] // A weekday in December
                public void GetTollFee_ShouldNotExceedMaximumFeeForCar(string dateTime, int expectedFee = 60)
                {
                    var tollCalculator = new TollCalculator();
                    //ARRANGE
                    var testDate = DateTime.Parse(dateTime);




                }


                [Theory]
                [InlineData("2024-07-18T00:00:00")] 
                [InlineData("2024-07-18T00:00:00")] 
                [InlineData("2024-07-18T00:00:00")] // A weekday in July
                public void GetTollFee_ShouldReturnZeroForCarJuly(string dateTime, int expectedFee = 0)
                {
                    var tollCalculator = new TollCalculator();
                    //ARRANGE



                }

                [Theory]
                [InlineData("2024-08-17T00:00:00")] 
                [InlineData("2024-08-17T00:00:00")] 
                [InlineData("2024-08-17T00:00:00")] // A Saturday in August
                public void GetTollFee_ShouldReturnZeroForCarWeekend(string dateTime, int expectedFee = 0)
                {
                    var tollCalculator = new TollCalculator();
                    //ARRANGE


                }

                [Theory]
                [InlineData("2024-04-30T00:00:00")] 
                [InlineData("2024-04-30T00:00:00")] // Valborg
                public void GetTollFee_ShouldReturnZeroForTruckValborg(string dateTime, int expectedFee = 0)
                {
                    var tollCalculator = new TollCalculator();
                    //ARRANGE



                }

                [Theory]
                [InlineData("2024-12-04T00:00:00")] 
                [InlineData("2024-12-04T00:00:00")] 
                [InlineData("2024-12-04T00:00:00")] // A weekday date
                public void GetTollFee_ShouldReturnZeroForEmergencyWeekday(string date, int expectedFee = 0)
                {
                    var tollCalculator = new TollCalculator();
                    //ARRANGE



                }


                [Theory]
                [InlineData("2024-12-01T00:00:00", true)] // A weekend date
                [InlineData("2024-12-04", false)] // A weekday date
                [InlineData("2024-12-24", true)] // Christmas Eve
                public void GetTollFee_ShouldReturnZeroForEmergencyChristmasEve(string dateTime, bool isFree = true)
                {
                    var tollCalculator = new TollCalculator();
                    //ARRANGE



                }
        */
    }
}
