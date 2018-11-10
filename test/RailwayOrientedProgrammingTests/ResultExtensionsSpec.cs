using CWiz.RailwayOrientedProgramming;
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
            var result = emptyString.EnsureStringNotEmpty(new Result.Error("emptyString should not be null or empty", "emptyString"));
            Assert.True(result.IsFailure);
            Assert.Single(result.Errors);
            Assert.Equal("emptyString should not be null or empty", result.Errors[0].Message);
            Assert.Equal("emptyString", result.Errors[0].Field);
        }

        [Fact]
        public void EnsureNotEmpty_should_fail_if_the_string_is_null()
        {
            Maybe<string> emptyString = null;
            var result = emptyString.EnsureStringNotEmpty(new Result.Error("emptyString should not be null or empty", "emptyString"));
            Assert.True(result.IsFailure);
            Assert.Single(result.Errors);
            Assert.Equal("emptyString should not be null or empty", result.Errors[0].Message);
            Assert.Equal("emptyString", result.Errors[0].Field);
        }

        [Fact]
        public void EnsureNotEmpty_should_pass_if_the_string_is_valid()
        {
            Maybe<string> validString = "This is a valid string";
            var result = validString.EnsureStringNotEmpty(new Result.Error("validString should not be null or empty", "validString"));
            Assert.True(result.IsSuccess);
        }
    }
}
