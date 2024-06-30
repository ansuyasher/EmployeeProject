using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeApi.Tests
{
    public class EmployeesControllerTests : IClassFixture<WebApplicationFactory<EmployeeApi.Startup>>
    {
        private readonly WebApplicationFactory<EmployeeApi.Startup> _factory;

        public EmployeesControllerTests(WebApplicationFactory<EmployeeApi.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task AddEmployee_ReturnsCreatedResponse()
        {
            // Arrange
            var client = _factory.CreateClient();

            var newEmployee = new
            {
                FirstName = "Ansuya",
                LastName = "Sher",
                Address1 = "123 Some Street",
                PayPerHour = 20.5m
            };

            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(newEmployee), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/employees", content);

            // Assert
            response.EnsureSuccessStatusCode(); 

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Ansuya", responseString); 
        }
    }
}
