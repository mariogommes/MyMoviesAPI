using FluentValidation.TestHelper;
using MyMoviesV2.Models;
using MyMoviesV2.Repositories;
using MyMoviesV2.Validators;
using NUnit.Framework;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProjectTests
{
    [TestFixture]
    public class Tests
    {

        private MovieValidator _validator;
        //Inicializando o repositório que vou testar mas não quero mexer no 
        //Banco real então mudei a dependencia para um banco mockado.
        private MovieRepository _movieRepository;
        private MovieContext _context_for_test_in_memory;

        [SetUp]
        public void Setup()
        {
            _validator = new MovieValidator();
            //Criando options do contexto que configura a base de dados para InMemory
            //Não é boa praticar usar Mockar a classe EF core, que não é Mock friendly
            var dbContextOptions = 
                new DbContextOptionsBuilder<MovieContext>().UseInMemoryDatabase(databaseName: "TestDb");

            //Iniciando o contexto para teste com banco de dados in_memory
            _context_for_test_in_memory = new MovieContext(dbContextOptions.Options);

            //Iniciando o reposório injetando a dependencia do contexto com data base in memory.
            _movieRepository = new MovieRepository(_context_for_test_in_memory);
        }

        [TearDown]
        public void Teardown()
        {
            //Garantindo que a cada teste tem um banco novo para testar
            _context_for_test_in_memory.Database.EnsureDeleted();
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

        //Pelo TDD está retornando null
        //Agora tem que retornar o filme esperado
        //Se outro teste pedir, eu uso o método create no data base.
        [Test]
        public void Insert_a_movie_in_data_base() 
        {
            //Arrange
            var movieToCreate = new Movie { Id = 1, Title = "Kill Bill", Director = "Tarantino", Synopsis = "Blood" };

            //Act
            var movieReturned = _movieRepository.Create(movieToCreate);

            //Assert
            Assert.AreEqual(movieToCreate, movieReturned.Result);
        }

        [Test]
        public void Insert_two_movies_in_data_base() 
        {
            //Arrange
            _context_for_test_in_memory.Movies.Add(new Movie { Id = 1, Title = "Kill Bill", Director = "Tarantino", Synopsis = "Blood" });
            _context_for_test_in_memory.Movies.Add(new Movie { Id = 2, Title = "Kill Bill 2", Director = "Tarantino", Synopsis = "Blood" });
            _context_for_test_in_memory.SaveChanges();

            //Act
            var movies = _context_for_test_in_memory.Movies.ToListAsync();
       
            //Assert
            Assert.Greater(movies.Result.Count, 1);
        }

        [Test]
        public async Task Delete_a_movie_from_data_base() 
        {
            //Arrange
            _context_for_test_in_memory.Movies.Add(new Movie { Id = 3, Title = "Kill Bill 3", Director = "Tarantino", Synopsis = "Blood" });
            _context_for_test_in_memory.SaveChanges();

            //Act
            await _movieRepository.Delete(3);

            //Assert
            Assert.IsNull(_context_for_test_in_memory.Movies.Find(3));
        }

        [Test]
        public void Get_a_movie_from_data_base() 
        {
            //Arrange
            var movieToBeReturned = new Movie { Id = 1, Title = null, Director = "James Camaron", Synopsis = "Aliens, Dude!" };
            _context_for_test_in_memory.Movies.Add(movieToBeReturned);

            //Act
            var movieReturned = _movieRepository.Get(1);

            //Assert
            Assert.AreEqual(movieReturned.Result, movieToBeReturned);
        }

    }
}