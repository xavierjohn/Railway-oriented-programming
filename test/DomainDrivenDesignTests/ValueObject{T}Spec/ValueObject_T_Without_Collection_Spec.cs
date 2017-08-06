using System.Collections.Generic;
using CWiz.DomainDrivenDesign;
using Xunit;

namespace DomainDrivenDesignTests
{
    [Trait("Category", "ValueObjectWithoutCollection")]
    public class ValueObject_T_Without_Collection_Spec
    {
        ValueObjectWithoutCollection SameValueInstance1 = new ValueObjectWithoutCollection("Prop1", "Prop2", 99);
        ValueObjectWithoutCollection SameValueInstance2 = new ValueObjectWithoutCollection("Prop1", "Prop2", 99);
        ValueObjectWithoutCollection DifferentValueInstance1 = new ValueObjectWithoutCollection("Prop1", "Different", 99);


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

        public class ValueObjectWithoutCollection : ValueObject<ValueObjectWithoutCollection>
        {
            public ValueObjectWithoutCollection(string firstName, string lastName, int age)
            {
                FirstName = firstName;
                LastName = lastName;
                Age = age;
            }

            public string FirstName { get; }
            public string LastName { get; }
            public int Age { get; }
        }
    }
}