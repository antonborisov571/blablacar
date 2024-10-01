namespace Blablacar.Core.Entities;

/// <summary>
/// Интерфейс сущности
/// </summary>
/// <typeparam name="TId">Тип данных для Id</typeparam>
public interface IEntity<TId>
{
    /// <summary>
    /// Id
    /// </summary>
    public TId Id { get; set; }
}