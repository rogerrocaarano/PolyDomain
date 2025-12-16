using PolyDomain.Core.Primitives;

namespace PolyDomain.Tests.Primitives;

public class EnumerationTests
{
    private class CardType : Enumeration
    {
        public static readonly CardType Debit = new(1, "Debit");
        public static readonly CardType Credit = new(2, "Credit");

        private CardType(int id, string name)
            : base(id, name) { }
    }

    private class EmployeeType : Enumeration
    {
        public static readonly EmployeeType Manager = new(1, "Manager");

        private EmployeeType(int id, string name)
            : base(id, name) { }
    }

    [Fact]
    public void get_all_should_return_all_defined_static_fields()
    {
        // Act
        var all = Enumeration.GetAll<CardType>().ToList();

        // Assert
        Assert.Equal(2, all.Count);
        Assert.Contains(CardType.Debit, all);
        Assert.Contains(CardType.Credit, all);
    }

    [Fact]
    public void from_value_should_return_correct_item()
    {
        // Act
        var item = Enumeration.FromValue<CardType>(1);

        // Assert
        Assert.Equal(CardType.Debit, item);
    }

    [Fact]
    public void from_value_should_throw_exception_if_not_found()
    {
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => Enumeration.FromValue<CardType>(99));
    }

    [Fact]
    public void from_display_name_should_return_correct_item()
    {
        // Act
        var item = Enumeration.FromDisplayName<CardType>("Credit");

        // Assert
        Assert.Equal(CardType.Credit, item);
    }

    [Fact]
    public void equals_should_return_true_for_same_values()
    {
        // Arrange
        var item1 = CardType.Debit;
        var item2 = Enumeration.FromValue<CardType>(1);

        // Act & Assert
        Assert.True(item1.Equals(item2));
        Assert.True(item1 == item2);
    }

    [Fact]
    public void equals_should_return_false_for_different_types_with_same_id()
    {
        // Arrange
        var card = CardType.Debit; // ID 1
        var employee = EmployeeType.Manager; // ID 1

        // Act & Assert
        Assert.False(card.Equals(employee));
    }

    [Fact]
    public void compare_to_should_sort_by_id()
    {
        // Arrange
        var list = new List<CardType> { CardType.Credit, CardType.Debit }; // [2, 1]

        // Act
        list.Sort();

        // Assert
        Assert.Equal(CardType.Debit, list[0]); // 1
        Assert.Equal(CardType.Credit, list[1]); // 2
    }
}
