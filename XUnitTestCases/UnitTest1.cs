using IdentityManager.Controllers;
using IdentityManager.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Music.Controllers;
using Music.Models;
using Music.MultipleModel;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestCases
{
    public class UnitTest1
    {
        private readonly IdentityManager.Data.ApplicationDbContext _db;

        HomeViewController viewController = new HomeViewController();
        
        [Fact]
        public void Home_Index()
        {
            ViewResult? result = viewController.Homepage() as ViewResult;
            Assert.NotNull(result);
        }
        [Fact]
        public void Payment()
        {
            ViewResult? result = viewController.Payment() as ViewResult;
            Assert.NotNull(result);
        }
        [Fact]
        public void StatisticSongPartial()
        {
            ViewResult? result = viewController.StatisticSongPartial() as ViewResult;
            Assert.NotNull(result);
        }
        

    }
}