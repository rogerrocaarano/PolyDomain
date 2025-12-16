namespace RosettaDomain.Core.Abstractions;

/// <summary>
/// Defines the contract for a Factory in Domain-Driven Design.
/// Factories are responsible for encapsulating the complex logic required to create
/// valid aggregates or entities, ensuring all invariants are met upon creation.
/// </summary>
/// <typeparam name="T">The type of the object created by this factory. Covariant.</typeparam>
public interface IFactory<out T>
{
    // We do not enforce a generic 'Create()' method signature here because
    // the parameters required to create an entity vary significantly between domains.
    //
    // Concrete interfaces should define specific creation methods, e.g.:
    // T Create(string name, Address address);
}
