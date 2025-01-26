using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.ClientAggregate;
using Template.Domain.ValueObjects;

namespace Api.Test.Domain.ClientTest
{
    internal class ClientTest
    {
        [Test]
        public void ClientCreationIsValid()
        {
            // Arrange
            string name = "David";
            string phone = "75324397";
            string email = "davidfernando.chavez777@gmail.com";
            // Act
            var client = Client.CreateClient(name,phone,email);
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(client.Name, Is.EqualTo(name));
                Assert.That(client.Phone, Is.EqualTo(phone));
                Assert.That(client.Email.Value, Is.EqualTo(email));
            });

        }

        [Test]
        public void ClientAddAddresIsValid()
        {
            // Arrange
            string name = "David";
            string phone = "75324397";
            string email = "davidfernando.chavez777@gmail.com";

            string city = "Bolivia";
            string addres = "Santa cruz";
            decimal longitud = 1222l;
            decimal latidud = 1222l;


            // Act
            var client = Client.CreateClient(name, phone, email);
            client.AddAddres(city,addres,latidud,longitud);
            // Assert
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(client.Addresses, Is.Not.Empty);

            });
        }

        [Test]
        public void ClientAddMedicalIsValid()
        {
            // Arrange
            string name = "David";
            string phone = "75324397";
            string email = "davidfernando.chavez777@gmail.com";

            string nameMedical = "Cancer";
            string descriptionMedical = "Cancer de cerebro";
            string type = "cancerigeno";


            // Act
            var client = Client.CreateClient(name, phone, email);
            client.AddMedicalIllnesses(nameMedical, descriptionMedical, type);
            // Assert
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(client.MedicalIllnessess, Is.Not.Empty);

            });

        }
    }
}
