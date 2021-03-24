using FluentValidation.TestHelper;
using MyMoviesV2.Models;
using MyMoviesV2.Repositories;
using MyMoviesV2.Validators;
using NUnit.Framework;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProjectTests
{
    [TestFixture]
    public class Tests
    {

        private MovieValidator _validator;
        //Declarando um mock de MovieContext, mockando a classe de contexto que acessa o banco
        //que é a dependencia da classe de repositório
        private Mock<IMovieContext> _mockMovieContext;
        //Inicializando o repositório que vou testar mas não quero mexer no 
        //Banco real então mudei a dependencia para um banco mockado.
        private IMovieRepository _movieRepository;


        [SetUp]
        public void Setup()
        {
            _validator = new MovieValidator();
            //Inicializando o Mock
            _mockMovieContext = new Mock<IMovieContext>();
            _movieRepository = new MovieRepository(_mockMovieContext.Object);
        }

        [Test]
        public void Should_have_error_when_Name_is_null()
        {
            //Arrange
            var model = new Movie { Id = 1, Title = null, Director = "James Camaron", Synopsis = "Aliens, Dude!" };

            //Act
            var result = _validator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(person => person.Title);
        }

        [Test]
        public void Return_a_movie_in_data_base() 
        {
            //Assert
            var movieInDataBase = new Movie { Id = 1, Title = "Kill Bill", Director = "Tarantino", Synopsis = "Blood" };
            _mockMovieContext.Setup(context => context.Movies.FindAsync(1)).ReturnsAsync(movieInDataBase);

            //Act
            var movieToGet = _movieRepository.Get(1);

            //Assert
            Assert.AreEqual(movieToGet.Result, movieInDataBase);
        }

    }
}