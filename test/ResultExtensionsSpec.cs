using CWiz.RailwayOrientedProgramming;
using FluentAssertions;
using System;
using Xunit;

namespace RailwayOrientedProgrammingTests
{
    public class ResultExtensionsSpec
    {
        [Fact]
        public void EnsureNotEmpty_should_fail_if_the_string_is_empty()
        {
            Maybe<string> emptyString = string.Empty;
            var result = emptyString.EnsureNotNullOrWhiteSpace(new Result.Error("emptyString should not be null or empty", "emptyString"));
            Assert.True(result.IsFailure);
            Assert.Single(result.Errors);
            Assert.Equal("emptyString should not be null or empty", result.Errors[0].Message);
            Assert.Equal("emptyString", result.Errors[0].Field);
        }

        [Fact]
        public void EnsureNotEmpty_should_fail_if_the_string_is_null()
        {
            Maybe<string> emptyString = null;
            var result = emptyString.EnsureNotNullOrWhiteSpace(new Result.Error("emptyString should not be null or empty", "emptyString"));
            Assert.True(result.IsFailure);
            Assert.Single(result.Errors);
            Assert.Equal("emptyString should not be null or empty", result.Errors[0].Message);
            Assert.Equal("emptyString", result.Errors[0].Field);
        }

        [Fact]
        public void EnsureNotEmpty_should_fail_if_the_string_is_whitespaces()
        {
            Maybe<string> emptyString = "   ";
            var result = emptyString.EnsureNotNullOrWhiteSpace(new Result.Error("emptyString should not be null or empty", "emptyString"));
            Assert.True(result.IsFailure);
            Assert.Single(result.Errors);
            Assert.Equal("emptyString should not be null or empty", result.Errors[0].Message);
            Assert.Equal("emptyString", result.Errors[0].Field);
        }

        [Fact]
        public void EnsureNotEmpty_should_pass_if_the_string_is_valid()
        {
            Maybe<string> validString = "This is a valid string";
            var result = validString.EnsureNotNullOrWhiteSpace(new Result.Error("validString should not be null or empty", "validString"));
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void OnSuccess_should_be_called()
        {
            string validated = null;
            Maybe<string> toValidate = "This is a valid string";
            toValidate.EnsureNotNullOrWhiteSpace(new Result.Error("validString should not be null or empty", nameof(toValidate)))
                .OnSuccess((valid) => { validated = valid; return Result<bool>.Ok(valid); });

            validated.Should().Be(toValidate.Value);
        }

        [Fact]
        public void OnFailure_should_be_called()
        {
            string validated = null;
            bool failureCalled = false;
            Maybe<string> toValidate = string.Empty;
            toValidate.EnsureNotNullOrWhiteSpace(new Result.Error("toValidate should not be null or empty", nameof(toValidate)))
                .OnSuccess((valid) => { validated = valid; return Result<bool>.Ok(valid); })
                .OnFailure(result =>
                  {
                      failureCalled = true;
                      result.Errors.Count.Should().Be(1);
                      result.Errors[0].Message.Should().Be("toValidate should not be null or empty");
                      return result;
                  });

            validated.Should().BeNull();
            failureCalled.Should().BeTrue();
        }
    }
}
