using Xunit;
using CWiz.RailwayOrientedProgramming;

namespace DomainDrivenDesignTests
{
    public class RequiredString_T_Spec
    {
        [Fact]
        public void Cannot_create_empty_RequiredString()
        {
            var trackingId1 = TrackingId.Create("");
            Assert.True(trackingId1.IsFailure);
        }

        [Fact]
        public void Two_RequiredString_with_same_value_should_be_equal()
        {
            var trackingId1 = TrackingId.Create("SameValue");
            var trackingId2 = TrackingId.Create("SameValue");
            var result = Result.Combine(trackingId2, trackingId1);
            Assert.True(result.IsSuccess);
            Assert.Equal(trackingId1.Value, trackingId2.Value);
        }
        [Fact]
        public void Two_RequiredString_with_different_value_should_be__not_equal()
        {
            var trackingId1 = TrackingId.Create("Value1");
            var trackingId2 = TrackingId.Create("Value2");
            var result = Result.Combine(trackingId2, trackingId1);
            Assert.True(result.IsSuccess);
            Assert.NotEqual(trackingId1.Value, trackingId2.Value);
        }
    }
}
