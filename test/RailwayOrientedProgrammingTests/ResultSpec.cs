using CWiz.RailwayOrientedProgramming;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RailwayOrientedProgrammingTests
{
    public class ResultSpec
    {
        [Fact]
        public void Combine_should_combine_all_failed_results_and_be_in_failed_state()
        {
            var failedResult1 = Result.Fail(new Result.Error("First failed result", "Field1"));
            var successResult = Result.Ok();
            var failedResult2 = Result.Fail(new Result.Error("Second failed result", "Field2"));

            var result = Result.Combine(failedResult1, successResult, failedResult2);
            Assert.True(result.IsFailure);
            Assert.Equal(2, result.Errors.Count);
            Assert.Equal("First failed result", result.Errors[0].Message);
            Assert.Equal("Field1", result.Errors[0].Field);
            Assert.Equal("Second failed result", result.Errors[1].Message);
            Assert.Equal("Field2", result.Errors[1].Field);
        }

        [Fact]
        public void Combine_should_be_in_success_state_when_all_results_are_success()
        {
            var successResult1 = Result.Ok();
            var successResult2 = Result.Ok();

            var result = Result.Combine(successResult1, successResult2);
            Assert.True(result.IsSuccess);
        }
    }
}
