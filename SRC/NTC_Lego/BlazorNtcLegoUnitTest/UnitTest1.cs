using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bunit;
using Moq;
using Moq.AutoMock;
using NTC_Lego.Client;
using Microsoft.EntityFrameworkCore;
using NTC_Lego.Server.Services;
using NTC_Lego.Shared;
using NTC_Lego.Client.Pages.AdminPortal;
using NTC_Lego.Client.Shared;
using Autofac.Extras.Moq;

namespace BlazorNtcLegoUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LoadUser_ValidCall()
        {
            var mockDBSet = new Mock<DbSet<DataService>>();

            //mockDBSet.Setup(x => x.Load<User>())
                //.Returns();
        }

        // Test to see if footer.razor div class have correct text
        [TestMethod]
        public void FooterPageMainLayoutRenderCorrectly()
        {
            var ctx = new Bunit.TestContext();

            //var cut = ctx.RenderComponent<Footer>();

            //var smallElmText = cut.Find("div").TextContent;

            var expectedHtml = "NTC Lego 2022 &reg";
            
            //smallElmText.MarkupMatches(expectedHtml);
            
        }
       
    }
}