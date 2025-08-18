using Xunit;
using System;
using AssetManagementApi.Services; 

public class AssetServiceTests
{
    [Fact]
    public void CalculateAge_InstallationDateIsTenYearsAgo_ReturnsTen()
    {
        // Arrange
        var service = new AssetService();
        var installationDate = DateTime.Today.AddYears(-10);

        // Act
        var actualAge = service.CalculateAge(installationDate);

        // Assert
        Assert.Equal(10, actualAge);
    }
}