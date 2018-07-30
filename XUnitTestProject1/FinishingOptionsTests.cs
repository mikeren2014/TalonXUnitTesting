using Newtonsoft.Json.Converters;
using RestEase;
using Shouldly;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Talon.Api.Testing.Interfaces;
using Talon.Models.Common;
using Talon.Web.Models.Create;
using Taylor.UFP.Common.Models;
using Taylor.UFP.XCore.AutoFill;
using Xunit;

namespace Talon.Api.Testing
{
    public class FinishingOptionsTests
    {
        const int ACCOUNT_ID = 23910;
        const string URL = "http://localhost:28221/api";
        //const string URL = "http://talonsvcdev.navitor.com/api";

        IFinishingOptions client;

        public FinishingOptionsTests()
        {
            var token = TokenHelper.GetToken(ACCOUNT_ID).Result;
            client = new RestClient(URL)
            {
                JsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
                {
                    Converters = { new StringEnumConverter() }
                }
            }.For<IFinishingOptions>();
            client.Token = new AuthenticationHeaderValue("Bearer", token);
        }



        [Fact]
        public async Task Get_NotFound()
        {
            // Arrange
            var foId = 999999;

            // Act
            var response = await client.GetById(foId);

            // Assert
            response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Success()
        {
            // Arrange
            var foId = 218;

            // Act
            var response = await client.GetById(foId);
            var data = response.GetContent().Data;

            // Assert
            response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK);
            data.ShouldNotBeNull();
        }

        [Fact]
        public async Task Create_Sucess()
        {
            // Arragen
            var createPayload = new CreateFinishingOptionPayload
            {
                ApplicableTo = ApplicableTo.Configuration,
                Attributes = new Dictionary<string, string>(),
                BuyerName = "TestBuyerName",
                ExternalId = 1234567,
                ManufacturerCode = "Test Mfg Code",
                ManufacturerName = "Test Mfg name",
                PreventConfigurationAggregation = false,
                QuantityPer = QuantityPer.Each,
                SubType = BdcFinishingOptionSubType.Folding
            };

            // Act
            var response = await client.Create(createPayload);

            // Assert
            response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.Created);
            var data = response.GetContent();
            data.Data.ExternalId.ShouldBe(createPayload.ExternalId);
        }
    }

}
