using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuizAPI;
using QuizAPI.Controllers;
using QuizAPI.Services;
using Xunit;

namespace QuizAPITests
{
    public class AnswerControllerTest
    {
        private readonly AnswerController _controller;
        private readonly IConfiguration _config;

        public AnswerControllerTest()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build();
            _controller = new AnswerController(new ServiceFactory(GetQuizContext()), GetQuizContext(), _config);
        }

        public QuizContext GetQuizContext()
        {
            var connectionString = _config.GetConnectionString("WebApiDatabase");
            var options = new DbContextOptionsBuilder<QuizContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).Options;

            var _context = new QuizContext(options, _config);

            return _context;
        }

        //[Fact]
        //public async void GetTestResult_Ok()
        //{
        //    var okResult = await _controller.TryGetQuestions(1);

        //    Assert.IsType<OkObjectResult>(okResult);
        //}

        //[Fact]
        //public async void GetTestResult_NotFound()
        //{
        //    var NotFoundResult = await _controller.TryGetQuestions(-1);

        //    Assert.IsType<NotFoundObjectResult>(NotFoundResult);
        //}
    }
}