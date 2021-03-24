using FluentValidation.TestHelper;
using MyMoviesV2.Models;
using MyMoviesV2.Validators;
using NUnit.Framework;

namespace ProjectTests
{
    [TestFixture]
    public class Tests
    {

        MovieValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new MovieValidator();
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
        public void Return_list_of_movies_in_data_base() 
        {
            //Arrange



            //Act



            //Assert
        }

    }
}