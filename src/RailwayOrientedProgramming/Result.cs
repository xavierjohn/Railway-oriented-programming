using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CWiz.RailwayOrientedProgramming
{
    public class Result
    {
        public class Error
        {
            public Error(string errorMessage, string field = null)
            {
                if (string.IsNullOrEmpty(errorMessage))
                    throw new InvalidOperationException();
                Message = errorMessage;
                Field = field;
            }

            public string Field { get; }
            public string Message { get; }
        }

        protected Result(bool isSuccess, ReadOnlyCollection<Error> errors)
        {
            if (isSuccess && errors != null)
                throw new InvalidOperationException();
            if (!isSuccess && errors == null)
                throw new InvalidOperationException();
            else
                Errors = errors;

            IsSuccess = isSuccess;
        }

        protected Result(bool isSuccess, Error error = null)
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException();
            if (!isSuccess && error == null)
                throw new InvalidOperationException();
            else
                Errors = (new List<Error> { error }).AsReadOnly();

            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
        public ReadOnlyCollection<Error> Errors { get; }

        public bool IsFailure => !IsSuccess;

        public static Result Fail(Error error)
        {
            return new Result(false, error);
        }

        public static Result<T> Fail<T>(Error error)
        {
            return new Result<T>(default(T), false, error);
        }

        public static Result<T> Fail<T>(ReadOnlyCollection<Error> errors)
        {
            return new Result<T>(default(T), false, errors);
        }

        public static Result Ok()
        {
            return new Result(true);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true);
        }

        public static Result Combine(params Result[] results)
        {
            var errors = new List<Error>();
            foreach (var result in results)
                if (result.IsFailure)
                    errors.AddRange(result.Errors);

            if (errors.Count > 0)
                return new Result(false, errors.AsReadOnly());

            return Ok();
        }
    }


    public class Result<T> : Result
    {
        private readonly T _value;

        protected internal Result([AllowNull] T value, bool isSuccess, Error error = null)
            : base(isSuccess, error)
        {
            _value = value;
        }

        protected internal Result([AllowNull] T value, bool isSuccess, ReadOnlyCollection<Error> errors)
    : base(isSuccess, errors)
        {
            _value = value;
        }

        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException();

                return _value;
            }
        }
    }
}