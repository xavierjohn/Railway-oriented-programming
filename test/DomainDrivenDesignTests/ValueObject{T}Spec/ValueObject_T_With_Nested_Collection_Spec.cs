using System.Collections.Generic;
using CWiz.DomainDrivenDesign;
using Xunit;

namespace DomainDrivenDesignTests
{
    [Trait("Category", "ValueObjectWithNestedCollection")]
    public class ValueObject_T_With_Nested_Collection_Spec
    {
        ValueObjectWithNestedCollection SameValueInstance1 = new ValueObjectWithNestedCollection()
        {
            FirstProperty = 1,
            SecondProperty = 2,
            NestedCollection = new List<ValueObjectWithNestedCollection.NestedClass>()
            {
                new ValueObjectWithNestedCollection.NestedClass() {
                    NestedFirstProperty = "RandomOne", NestedSecondProperty = "RandomTwo",
                    NestedNestedCollection =  new List<string>() { "ListItem1","ListItem2"}
                }
            }

        };
        ValueObjectWithNestedCollection SameValueInstance2 = new ValueObjectWithNestedCollection()
        {
            FirstProperty = 1,
            SecondProperty = 2,
            NestedCollection = new List<ValueObjectWithNestedCollection.NestedClass>()
            {
                new ValueObjectWithNestedCollection.NestedClass() {
                    NestedFirstProperty = "RandomOne", NestedSecondProperty = "RandomTwo",
                    NestedNestedCollection =  new List<string>() { "ListItem1","ListItem2"}
                }
            }
        };

        ValueObjectWithNestedCollection DifferentValueInstance1 = new ValueObjectWithNestedCollection()
        {
            FirstProperty = 1,
            SecondProperty = 2,
            NestedCollection = new List<ValueObjectWithNestedCollection.NestedClass>()
            {
                new ValueObjectWithNestedCollection.NestedClass() {
                    NestedFirstProperty = "RandomOne", NestedSecondProperty = "RandomTwo",
                    NestedNestedCollection =  new List<string>() { "DifferentItem1","ListItem2"}
                }
            }
        };

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

        public class ValueObjectWithNestedCollection : ValueObject<ValueObjectWithNestedCollection>
        {
            public int FirstProperty { get; set; }
            public int SecondProperty { get; set; }

            public List<NestedClass> NestedCollection { get; set; }

            public class NestedClass
            {
                public string NestedFirstProperty { get; set; }
                public string NestedSecondProperty { get; set; }

                public List<string> NestedNestedCollection { get; set; }

            }
        }
    }


}