using FluentValidation;
using MyMoviesV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMoviesV2.Validators
{
    public class MovieValidator : AbstractValidator<Movie>
    {
        public MovieValidator()
        {
            RuleFor(movie => movie.Id).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();

            RuleFor(movie => movie.Title).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Movie's title can't be empty").Length(3, 100)
                .WithMessage($"Title invalid lenght({{TotalLength}})");

            RuleFor(movie => movie.Director).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Movie's director's name can't be empty").Length(2, 60)
                .WithMessage($"Title invalid lenght({{TotalLength}})");

            RuleFor(movie => movie.Synopsis).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Movie's synopsis can't be empty").Length(2, 300)
                .WithMessage($"Title invalid lenght({{TotalLength}})");
        }
    }
}
