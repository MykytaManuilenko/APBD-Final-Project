using System.Threading.Tasks;
using AdvertApi.Controllers;
using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Exceptions;
using AdvertApi.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AdvertApiTests.UnitTests.Client
{
    [TestFixture]
    public class ClientsUnitTests
    {
        [Test]
        public async Task RegistrateNewClient_201CreatedCorrect()
        {
            //Arrange
            var dbLayer = new Mock<ICampaignService>();
            var newClient = new RegistrateNewClientRequest
            {
                FirstName = "Kamil",
                LastName = "Kowalski",
                Email = "kowalski@wp.pl",
                Phone = "454-232-222",
                Login = "Kamil123",
                Password = "Ffjan123"
            };

            dbLayer.Setup(d => d.RegistrateClientAsync(newClient))
                   .ReturnsAsync(new RegistratedClientResponse
                   {
                       FirstName = "Kamil",
                       LastName = "Kowalski",
                       Email = "kowalski@wp.pl",
                       Phone = "454-232-222",
                       Login = "Kamil123",
                   });

            var controller = new ClientController(dbLayer.Object);

            //Act
            var response = await controller.RegistrateClient(newClient);

            //Assert
            Assert.IsNotNull(true);
            var result = response as CreatedAtActionResult;
            var vr = (ObjectResult)response;
            Assert.IsNotNull(vr.Value);
            Assert.IsTrue(vr.Value is RegistratedClientResponse);
            var vm = (RegistratedClientResponse)vr.Value;
            Assert.IsTrue(vm.FirstName.Equals("Kamil"));
            Assert.IsTrue(vm.Login.Equals("Kamil123"));
        }

        [Test]
        public async Task RegistrateNewClient_400ClientHasAlreadyExists()
        {
            //Arrange
            var dbLayer = new Mock<ICampaignService>();
            var newClient = new RegistrateNewClientRequest
            {
                FirstName = "John",
                LastName = "Brayan",
                Email = "john@wp.pl",
                Phone = "455-777-777",
                Login = "Kamil123",
                Password = "bhbhbiu"
            };

            dbLayer.Setup(d => d.RegistrateClientAsync(newClient))
                   .ThrowsAsync(new ClientHasAlreadyExistsException());

            var controller = new ClientController(dbLayer.Object);

            //Act
            var response = await controller.RegistrateClient(newClient);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response is ObjectResult);
            var vr = (ObjectResult)response;
            Assert.IsTrue(vr.StatusCode == 400);
        }
    }
}
