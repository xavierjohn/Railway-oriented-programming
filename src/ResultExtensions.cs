using System;
using System.Collections.Generic;

namespace CWiz.RailwayOrientedProgramming
{
    public static class ResultExtensions
    {
        public static Result<TIn> ToResult<TIn>(this Maybe<TIn> maybe, Result.Error error) where TIn : class
        {
            if (maybe.HasNoValue)
                return Result.Fail<TIn>(error);

            return Result.Ok(maybe.Value);
        }

        public static Result<TOut> OnSuccess<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> func)
        {
            if (result.IsFailure)
                return Result.Fail<TOut>(result.Errors);

            return func(result.Value);
        }

        public static Result<T> OnFailure<T>(this Result<T> result, Func<Result<T>, Result<T>> func)
        {
            if (result.IsSuccess)
                return result;

            return func(result);
        }

        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Result.Error error)
        {
            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result.Fail<T>(error);

            return result;
        }

        public static Result<string> EnsureNotNullOrWhiteSpace(this Maybe<string> maybe, Result.Error error)
        {
            return maybe.ToResult(error)
                    .Ensure(name => !string.IsNullOrWhiteSpace(name), error);
        }

        public static Result<K> Map<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Errors);

            return Result.Ok(func(result.Value));
        }

        public static Result<K> Map<K>(this Result result, Func<K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Errors);

            return Result.Ok<K>(func());
        }

        public static T OnBoth<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }
    }
}
