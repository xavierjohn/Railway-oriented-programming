using System.Collections.Generic;
using CWiz.DomainDrivenDesign;
using Xunit;

namespace DomainDrivenDesignTests
{
    [Trait("Category", "ValueObjectWithCollection")]
    public class ValueObject_T_With_Collection_Spec
    {
        ValueObjectWithCollection SameValueInstance1 = new ValueObjectWithCollection(new List<string> { "123-456-7890", "234-567-8901" });
        ValueObjectWithCollection SameValueInstance2 = new ValueObjectWithCollection(new List<string> { "123-456-7890", "234-567-8901" });
        ValueObjectWithCollection DifferentValueInstance1 = new ValueObjectWithCollection(new List<string> { "123-456-7890" });


        [Fact]
        public void Equal_should_return_true_for_two_different_instances_with_same_values()
        {
            Assert.Equal(SameValueInstance1, SameValueInstance2);
        }

        [Fact]
        public void X3DX3D_should_return_true_for_two_different_instances_with_same_value()
        {
            Assert.True(SameValueInstance1 == SameValueInstance2);
        }

        [Fact]
        public void NotEqual_should_return_true_when_values_are_different()
        {
            Assert.NotEqual(SameValueInstance1, DifferentValueInstance1);
        }

        [Fact]
        public void ne_should_return_true_when_values_are_different()
        {
            Assert.True(SameValueInstance1 != DifferentValueInstance1);
        }

        public class ValueObjectWithCollection : ValueObject<ValueObjectWithCollection>
        {
            public ValueObjectWithCollection(List<string> phoneNumbers)
            {
                PhoneNumbers = phoneNumbers;
            }

            public List<string> PhoneNumbers { get; }
        }
    }


}