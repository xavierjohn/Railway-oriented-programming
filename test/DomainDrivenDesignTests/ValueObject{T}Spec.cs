using CWiz.DomainDrivenDesign;
using Xunit;

namespace DomainDrivenDesignTests
{
    public class Person : ValueObject<Person>
    {
        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }
    }

    public class ValueObject_T_Spec
    {
        [Fact]
        public void Two_different_instances_of_a_value_object_with_same_values_should_be_equal()
        {
            var fname = "Xavier";
            var lname = "John";
            var age = 99;
            var instance1 = new Person(fname, lname, age);
            var instance2 = new Person(fname, lname, age);
            Assert.Equal(instance1, instance2);
        }

        [Fact]
        public void Two_different_instances_of_a_value_object_with_different_values_should_be_different()
        {
            var instance1 = new Person("Xavier", "John", 99);
            var instance2 = new Person("Space", "Ghost", 99);
            Assert.NotEqual(instance1, instance2);
        }
    }
}