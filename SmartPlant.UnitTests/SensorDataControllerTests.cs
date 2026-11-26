using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SmartPlantApi.Controllers;
using SmartPlantApi.Dtos;
using Xunit;


namespace SmartPlant.UnitTests
{
    public class SensorDataControllerTests
    {
        [Fact]
        public void Post_ValidSensorData_ReturnsOk()
        {
            // Arrange
            var controller = new SensorDataController();
            var request = new SensorDataRequest
            {
                SoilMoisturePercent = 45,
                LightLevelLux = 600,
                RecordedAt = DateTimeOffset.UtcNow
            };

            // Act
            var result = controller.Post(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);

        }

        // Test for ugyldig data, f.eks. jordfugtighed uden for 0-100 intervallet
        [Theory]
        [InlineData(-10)]
        [InlineData(120)]
        public void Post_InvalidMoisture_ReturnsBadRequest(double moisture)
        {
            // Arrange
            var controller = new SensorDataController();

            var request = new SensorDataRequest
            {
                SoilMoisturePercent = moisture,
                LightLevelLux = 600,
                RecordedAt = DateTimeOffset.UtcNow
            };

            // Act
            var result = controller.Post(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Tester, at ugyldige lysniveauer (negative værdier, NaN og Infinity) afvises.
        [Theory]
        [InlineData(-50)]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        public void Post_InvalidLightLevel_ReturnsBadRequest(double lightLevel)
        {
            // Arrange
            var controller = new SensorDataController();

            var request = new SensorDataRequest
            {
                SoilMoisturePercent = 45,
                LightLevelLux = lightLevel,
                RecordedAt = DateTimeOffset.UtcNow
            };

            // Act
            var result = controller.Post(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test for ugyldig RecordedAt, f.eks. default(DateTimeOffset)
        [Fact]
        public void Post_InvalidRecordedAt_ReturnsBadRequest()
        {
            // Arrange
            var controller = new SensorDataController();

            var request = new SensorDataRequest
            {
                SoilMoisturePercent = 45,
                LightLevelLux = 600,
                RecordedAt = default
            };

            // Act
            var result = controller.Post(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }



    }
}
