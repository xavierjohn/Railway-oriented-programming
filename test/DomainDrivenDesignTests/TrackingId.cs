using CWiz.DomainDrivenDesign;

namespace DomainDrivenDesignTests
{
    public class TrackingId : RequiredString<TrackingId>
    {
        private TrackingId(string value) : base(value)
        {
        }

        public static explicit operator TrackingId(string trackingId)
        {
            return Create(trackingId).Value;
        }

        public static implicit operator string(TrackingId trackingId)
        {
            return trackingId.Value;
        }
    }
}