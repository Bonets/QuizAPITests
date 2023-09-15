using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuizAPI;
using QuizAPI.Controllers;
using QuizAPI.Services;
using Xunit;

namespace QuizAPITests
{
    public class TestControllerTest
    {
        private readonly TestController _controller;
        private readonly IConfiguration _config;

        public TestControllerTest()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsetting.json", false, false)
                .AddEnvironmentVariables()
                .Build();
            _controller = new TestController(new ServiceFactory(GetQuizContext()), GetQuizContext(), _config);
        }

        public QuizContext GetQuizContext()
        {
            var connectionString = _config.GetConnectionString("quiz");
            var options = new DbContextOptionsBuilder<QuizContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).Options;

            var _context = new QuizContext(options, _config);

            return _context;
        }

        [Fact]
        public async void GetTestResult_Ok()
        {
            var okResult = await _controller.TryGetTestResult(1);

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async void GetTestResult_NotFound()
        {
            var NotFoundResult = await _controller.TryGetTestResult(-1);

            Assert.IsType<NotFoundObjectResult>(NotFoundResult);
        }

        [Fact]
        public async void CalculateTestResult_Ok()
        {
            var okResult = await _controller.TryCalculateTestResultIdResult(1);

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async void CalculateTestResult_NotFound()
        {
            var NotFoundResult = await _controller.TryCalculateTestResultIdResult(-1);

            Assert.IsType<NotFoundObjectResult>(NotFoundResult);
        }
    }
}