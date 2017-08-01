using System;

namespace CWiz.RailwayOrientedProgramming
{
    public static class ResultExtensions
    {
        public static Result<TIn> ToResult<TIn>(this Maybe<TIn> maybe, string errorMessage) where TIn : class
        {
            if (maybe.HasNoValue)
                return Result.Fail<TIn>(errorMessage);

            return Result.Ok(maybe.Value);
        }

        public static Result<TOut> OnSuccess<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func)
        {
            if (result.IsFailure)
                return Result.Fail<TOut>(result.Error);

            return Result.Ok(func(result.Value));
        }
        public static Result<TOut> OnSuccess<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<TOut>(result.Error);
            }

            return func(result.Value);
        }

        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorMessage)
        {
            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result.Fail<T>(errorMessage);

            return result;
        }

        public static Result<K> Map<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return Result.Ok(func(result.Value));
        }

        public static Result<K> Map<K>(this Result result, Func<K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Error);

            return Result.Ok<K>(func());
        }


        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        public static T OnBoth<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }
    }
}
