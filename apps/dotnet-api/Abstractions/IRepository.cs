using System;

namespace FindMyPet.Api.Abstractions;

public interface IRepository<T>
{
  T? GetById(int it);
  IEnumerable<T> GetAll();
  void Create(T entity);
  void Update(T entity);
  void Delete(T entity);
}
