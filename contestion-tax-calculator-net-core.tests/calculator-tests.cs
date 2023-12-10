using congestion.calculator;
using congestion.calculator.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace contestion_tax_calculator_net_core.tests
{
    public class Tests
    {
        [TestFixture]
        public class SingleTollTests
        {
            private CongestionTaxCalculator congestionCalculator;

            [SetUp]
            public void Setup()
            {
                congestionCalculator = new CongestionTaxCalculator();
            }

            [TestCaseSource(nameof(SingleTollTestsData))]
            public void GetTax_ShouldReturnCorrespondingValueBasedOnHour(IVehicle vehicle, DateTime[] dates, Dictionary<string, int> expectedResults)
            {
                var actualResult = congestionCalculator.GetTax(vehicle, dates);

                TestContext.WriteLine($"Tax equals: {string.Join(", ", actualResult)} SEK. Expected result is {string.Join(", ", expectedResults)} SEK");

                CollectionAssert.AreEquivalent(expectedResults, actualResult);
            }

            public static IEnumerable<TestCaseData> SingleTollTestsData()
            {
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] { DateTime.Parse("2013-01-03 06:00:00") },
                    new Dictionary<string, int> { { "2013-01-03", 8 } }
                );
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] { DateTime.Parse("2013-01-03 06:30:00") },
                    new Dictionary<string, int> { { "2013-01-03", 13 } }
                );
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] { DateTime.Parse("2013-01-03 07:00:00") },
                    new Dictionary<string, int> { { "2013-01-03", 18 } }
                 );
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] { DateTime.Parse("2013-01-03 08:00:00") },
                    new Dictionary<string, int> { { "2013-01-03", 13 } }
                 );
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] { DateTime.Parse("2013-01-03 08:30:00") },
                    new Dictionary<string, int> { { "2013-01-03", 8 } }
                 );
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] { DateTime.Parse("2013-01-03 14:59:00") },
                    new Dictionary<string, int> { { "2013-01-03", 8 } }
                 );
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] { DateTime.Parse("2013-01-03 15:00:00") },
                    new Dictionary<string, int> { { "2013-01-03", 13 } }
                 );
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] { DateTime.Parse("2013-01-03 15:30:00") },
                    new Dictionary<string, int> { { "2013-01-03", 18 } }
                 );
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] { DateTime.Parse("2013-01-03 17:00:00") },
                    new Dictionary<string, int> { { "2013-01-03", 13 } }
                 );
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] { DateTime.Parse("2013-01-03 18:00:00") },
                    new Dictionary<string, int> { { "2013-01-03", 8 } }
                 );
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] { DateTime.Parse("2013-01-03 19:00:00") },
                    new Dictionary<string, int> { { "2013-01-03", 0 } }
                 );
            }
        }

        [TestFixture]
        public class MultiTollWorkDaysTests
        {
            private CongestionTaxCalculator congestionCalculator;

            [SetUp]
            public void Setup()
            {
                congestionCalculator = new CongestionTaxCalculator();
            }

            [TestCaseSource(nameof(SumTollWorkdaysTestsData))]
            public void GetTax_ShouldReturnCorrespondingSumOfToll(IVehicle vehicle, DateTime[] dates, Dictionary<string, int> expectedResults)
            {
                var actualResult = congestionCalculator.GetTax(vehicle, dates);

                TestContext.WriteLine($"Tax equals: {string.Join(", ", actualResult)} SEK. Expected result is {string.Join(", ", expectedResults)} SEK");

                CollectionAssert.AreEquivalent(expectedResults, actualResult);
            }

            public static IEnumerable<TestCaseData> SumTollWorkdaysTestsData()
            {
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-01-03 06:00:00"),
                    DateTime.Parse("2013-01-03 07:00:00"),
                    DateTime.Parse("2013-01-03 08:00:00") },
                    new Dictionary<string, int> { { "2013-01-03", 39 } }
                );

                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-01-03 07:00:00"),
                    DateTime.Parse("2013-01-03 15:30:00"),
                    DateTime.Parse("2013-01-03 18:20:00")
                    },
                    new Dictionary<string, int> { { "2013-01-03", 44 } }
                );

                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-01-03 04:00:00"),
                    DateTime.Parse("2013-01-03 04:10:00"),
                    DateTime.Parse("2013-01-03 06:20:00")
                    },
                    new Dictionary<string, int> { { "2013-01-03", 8 } }
                );
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-01-03 02:00:00"),
                    DateTime.Parse("2013-01-03 03:10:00"),
                    DateTime.Parse("2013-01-03 04:20:00")
                    },
                    new Dictionary<string, int> { { "2013-01-03", 0 } }
                );
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-01-03 06:00:00"),
                    DateTime.Parse("2013-01-03 07:00:00"),
                    DateTime.Parse("2013-01-03 08:10:00"),
                    DateTime.Parse("2013-01-03 09:10:00"),
                    DateTime.Parse("2013-01-03 10:10:00"),
                    DateTime.Parse("2013-01-03 11:10:00"),
                    DateTime.Parse("2013-01-03 12:10:00"),
                    DateTime.Parse("2013-01-03 13:10:00"),
                    DateTime.Parse("2013-01-03 14:10:00"),
                    DateTime.Parse("2013-01-03 15:10:00"),
                    DateTime.Parse("2013-01-03 16:10:00"),
                    DateTime.Parse("2013-01-03 17:59:00"),
                    DateTime.Parse("2013-01-03 18:59:00"),
                    DateTime.Parse("2013-01-03 18:20:00")
                    },
                    new Dictionary<string, int> { { "2013-01-03", 60 } }
                );
            }
        }
    }

            

        [TestFixture]
        public class WeekendTollTests
        {
            private CongestionTaxCalculator congestionCalculator;

            [SetUp]
            public void Setup()
            {
                congestionCalculator = new CongestionTaxCalculator();
            }

            [TestCaseSource(nameof(WeekendTollTestsData))]
            public void GetTax_ShouldReturn0AsDatesPointToWeekendDays(IVehicle vehicle, DateTime[] dates, Dictionary<string, int> expectedResults)
        {
            var actualResult = congestionCalculator.GetTax(vehicle, dates);

            TestContext.WriteLine($"Tax equals: {string.Join(", ", actualResult)} SEK. Expected result is {string.Join(", ", expectedResults)} SEK");

            CollectionAssert.AreEquivalent(expectedResults, actualResult);
        }

        public static IEnumerable<TestCaseData> WeekendTollTestsData()
            {
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-01-12 06:00:00"),
                    DateTime.Parse("2013-01-12 07:00:00"),
                    DateTime.Parse("2013-01-12 15:00:00") },
                    new Dictionary<string, int> { { "2013-01-12", 0 } }
                );

                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-11-23 11:25:00"),
                    DateTime.Parse("2013-11-23 15:03:00"),
                    DateTime.Parse("2013-11-23 16:25:00") },
                    new Dictionary<string, int> { { "2013-11-23", 0 } }
                );

                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-06-23 06:47:00"),
                    DateTime.Parse("2013-06-23 08:22:00"),
                    DateTime.Parse("2013-06-23 14:07:00") },
                    new Dictionary<string, int> { { "2013-06-23", 0 } }
                );
            }
        }

        [TestFixture]
        public class PublicHolidaysTollTests
        {
            private CongestionTaxCalculator congestionCalculator;

            [SetUp]
            public void Setup()
            {
                congestionCalculator = new CongestionTaxCalculator();
            }

            [TestCaseSource(nameof(PublicHolidaysTollTestsData))]
            public void GetTax_ShouldReturn0AsDatesPointToPublicHolidays(IVehicle vehicle, DateTime[] dates, Dictionary<string, int> expectedResults)
        {
            var actualResult = congestionCalculator.GetTax(vehicle, dates);

            TestContext.WriteLine($"Tax equals: {string.Join(", ", actualResult)} SEK. Expected result is {string.Join(", ", expectedResults)} SEK");

            CollectionAssert.AreEquivalent(expectedResults, actualResult);
        }

        public static IEnumerable<TestCaseData> PublicHolidaysTollTestsData()
            {
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-01-01 06:00:00"),
                    DateTime.Parse("2013-01-01 07:00:00"),
                    DateTime.Parse("2013-01-01 15:00:00") },
                    new Dictionary<string, int> { { "2013-01-01", 0 } }
                );

                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-12-25 11:25:00"),
                    DateTime.Parse("2013-12-25 15:03:00"),
                    DateTime.Parse("2013-12-25 16:25:00") },
                    new Dictionary<string, int> { { "2013-12-25", 0 } }
                );

                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-06-06 06:47:00"),
                    DateTime.Parse("2013-06-06 08:22:00"),
                    DateTime.Parse("2013-06-06 14:07:00") },
                    new Dictionary<string, int> { { "2013-06-06", 0 } }
                );
            }
        }


        [TestFixture]
        public class VehicleTollFreeTests
        {
            private CongestionTaxCalculator congestionCalculator;

            [SetUp]
            public void Setup()
            {
                congestionCalculator = new CongestionTaxCalculator();
            }

            [TestCaseSource(nameof(TollFreeVehiclesTestsData))]
            public void GetTax_ShouldReturn0AsVehicleTypeIsNotTaxable(IVehicle vehicle, DateTime[] dates, Dictionary<string, int> expectedResults)
            {
                var actualResult = congestionCalculator.GetTax(vehicle, dates);

                TestContext.WriteLine($"Tax equals: {string.Join(", ", actualResult)} SEK. Expected result is {string.Join(", ", expectedResults)} SEK");

                CollectionAssert.AreEquivalent(expectedResults, actualResult);
            }

        public static IEnumerable<TestCaseData> TollFreeVehiclesTestsData()
            {
                yield return new TestCaseData(
                new Emergency(),
                new DateTime[] {
                    DateTime.Parse("2013-06-04 06:47:00"),
                    DateTime.Parse("2013-06-04 08:22:00"),
                    DateTime.Parse("2013-06-04 14:07:00") },
                    new Dictionary<string, int> { { "2013-06-04", 0 } }
                );

                yield return new TestCaseData(
                    new Motorcycle(),
                    new DateTime[] {
                    DateTime.Parse("2013-06-04 06:00:00"),
                    DateTime.Parse("2013-06-04 07:00:00"),
                    DateTime.Parse("2013-06-04 15:00:00") },
                    new Dictionary<string, int> { { "2013-06-04", 0 } }
                );

                yield return new TestCaseData(
                    new Foreign(),
                    new DateTime[] {
                    DateTime.Parse("2013-06-04 11:25:00"),
                    DateTime.Parse("2013-06-04 15:03:00"),
                    DateTime.Parse("2013-06-04 16:25:00") },
                    new Dictionary<string, int> { { "2013-06-04", 0 } }
                );

                yield return new TestCaseData(
                    new Diplomat(),
                    new DateTime[] {
                    DateTime.Parse("2013-06-04 06:47:00"),
                    DateTime.Parse("2013-06-04 08:22:00"),
                    DateTime.Parse("2013-06-04 14:07:00") },
                    new Dictionary<string, int> { { "2013-06-04", 0 } }
                );

                yield return new TestCaseData(
                    new Buss(),
                    new DateTime[] {
                    DateTime.Parse("2013-06-04 06:47:00"),
                    DateTime.Parse("2013-06-04 08:22:00"),
                    DateTime.Parse("2013-06-04 14:07:00") },
                    new Dictionary<string, int> { { "2013-06-04", 0 } }
                );

                yield return new TestCaseData(
                    new Military(),
                    new DateTime[] {
                    DateTime.Parse("2013-06-04 06:47:00"),
                    DateTime.Parse("2013-06-04 08:22:00"),
                    DateTime.Parse("2013-06-04 14:07:00") },
                    new Dictionary<string, int> { { "2013-06-04", 0 } }
                );
            }
        }


        [TestFixture]
        public class BiggestTollShouldStayTests
        {
            private CongestionTaxCalculator congestionCalculator;

            [SetUp]
            public void Setup()
            {
                congestionCalculator = new CongestionTaxCalculator();
            }

            [Test]
            [TestCaseSource(nameof(BiggestTollShouldStayTestsData))]
            public void GetTax_ShouldReturnBiggestTollWhenGoingThru2InShortTime(IVehicle vehicle, DateTime[] dates, Dictionary<string, int> expectedResults)
            {
                var actualResult = congestionCalculator.GetTax(vehicle, dates);

                TestContext.WriteLine($"Tax equals: {string.Join(", ", actualResult)} SEK. Expected result is {string.Join(", ", expectedResults)} SEK");

                CollectionAssert.AreEquivalent(expectedResults, actualResult);
            }

            public static IEnumerable<TestCaseData> BiggestTollShouldStayTestsData()
            {
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-01-03 06:45:00"),
                    DateTime.Parse("2013-01-03 07:00:00") },
                    new Dictionary<string, int> { { "2013-01-03", 18 } }
                );

                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-01-03 17:45:00"),
                    DateTime.Parse("2013-01-03 18:10:00") },
                    new Dictionary<string, int> { { "2013-01-03", 13 } }
                );
                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-01-03 18:29:00"),
                    DateTime.Parse("2013-01-03 19:10:00") },
                    new Dictionary<string, int> { { "2013-01-03", 8 } }
                );

                yield return new TestCaseData(
                    new Car(),
                    new DateTime[] {
                    DateTime.Parse("2013-01-03 17:45:00"),
                    DateTime.Parse("2013-01-03 17:46:00"),
                    DateTime.Parse("2013-01-03 17:47:00"),
                    DateTime.Parse("2013-01-03 17:48:00"),
                    DateTime.Parse("2013-01-03 17:49:00"),
                    DateTime.Parse("2013-01-03 17:50:00"),
                    DateTime.Parse("2013-01-03 17:51:00") },
                    new Dictionary<string, int> { { "2013-01-03", 13 } }
                );
            }
        }




    [TestFixture]
    public class SeveralDatesTests
    {
        private CongestionTaxCalculator congestionCalculator;

        [SetUp]
        public void Setup()
        {
            congestionCalculator = new CongestionTaxCalculator();
        }

        [Test]
        [TestCaseSource(nameof(SeveralDatesTestsData))]
        public void GetTax_ShouldReturnCollectionOfManyDatesAndTolls(IVehicle vehicle, DateTime[] dates, Dictionary<string, int> expectedResults)
        {
            var actualResult = congestionCalculator.GetTax(vehicle, dates);

            TestContext.WriteLine($"Tax equals: {string.Join(", ", actualResult)} SEK. Expected result is {string.Join(", ", expectedResults)} SEK");

            CollectionAssert.AreEquivalent(expectedResults, actualResult);
        }

        public static IEnumerable<TestCaseData> SeveralDatesTestsData()
        {
            yield return new TestCaseData(
                new Car(),
                new DateTime[] {
                    DateTime.Parse("2013-01-18 06:45:00"), 
                    DateTime.Parse("2013-01-18 07:00:00"),
                    DateTime.Parse("2013-01-18 07:45:00"), 
                    DateTime.Parse("2013-01-18 15:31:00"),

                    DateTime.Parse("2013-01-03 06:45:00"),
                    DateTime.Parse("2013-01-03 07:00:00"),

                    DateTime.Parse("2013-02-05 06:00:00"),
                    DateTime.Parse("2013-02-05 07:00:00"),
                    DateTime.Parse("2013-02-05 08:00:00"),
                    DateTime.Parse("2013-02-05 12:00:00"),
                    DateTime.Parse("2013-02-05 15:00:00"),
                    DateTime.Parse("2013-02-05 16:00:00"),
                    DateTime.Parse("2013-02-05 17:00:00"),
                    DateTime.Parse("2013-02-05 18:02:00"),

                    DateTime.Parse("2013-12-25 07:00:00"),
                    DateTime.Parse("2013-12-25 16:00:00"),
                    DateTime.Parse("2013-12-25 17:00:00"),

                    DateTime.Parse("2013-06-06 06:47:00"),
                    DateTime.Parse("2013-06-06 08:22:00"),
                    DateTime.Parse("2013-06-06 14:07:00"),
                },

                new Dictionary<string, int> { 
                    { "2013-01-18", 36 },
                    { "2013-01-03", 18 },
                    { "2013-02-05", 60 },
                    { "2013-06-06", 0 },
                    { "2013-12-25", 0 },
                }
            );
        }
    }
}

