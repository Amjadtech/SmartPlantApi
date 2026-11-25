using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SmartPlantApi.Models;

namespace SmartPlant.UnitTests
{
    public class SensorReadingTests
    {
        [Theory] // Betyder at testen kan køre med forskellige inputs
        [InlineData(-10)]
        [InlineData(120)]
        public void Validate_InvalidMoisture_ThrowsException(double moisture)
        {
            // Arrange
            SensorReading reading = new SensorReading
            {
                SoilMoisturePercent = moisture
            };

            // Act & Asser
            Assert.Throws<ArgumentOutOfRangeException>(
                () => reading.Validate());
        }

        // testen skal køre med forskellige inputs, som er gyldige
        [Theory]
        [InlineData(0)]
        [InlineData(35.5)]
        [InlineData(100)]
        public void Validate_ValidMoisture_DoesNotThrow(double moisture)
        {
            // Arrange
            SensorReading reading = new SensorReading
            {
                SoilMoisturePercent = moisture
            };

            // Act
            reading.Validate();

            // Assert
            Assert.InRange(reading.SoilMoisturePercent, 0, 100);
        }


        [Theory]
        [InlineData(double.NaN)] // NaN = Not a Number
        [InlineData(double.PositiveInfinity)] // Infinity = uendelig
        public void Validate_InvalidNumber_ThrowsException(double moisture)
        {
            // Arrange
            SensorReading reading = new SensorReading
            {
                SoilMoisturePercent = moisture
            };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(
                () => reading.Validate());
        }

    }
}

