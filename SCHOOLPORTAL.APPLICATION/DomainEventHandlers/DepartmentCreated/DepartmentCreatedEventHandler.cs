namespace Application;
using MediatR;
using DomainEvents;
using SeedWork;
using Entities;

public class DepartmentCreatedEventHandler:INotificationHandler<DepartmentDomainEvent>
{
      private readonly IGenericRepository<Todo> _todoRepository;

      public DepartmentCreatedEventHandler(IGenericRepository<Todo> todoRepository)
      {
        _todoRepository=todoRepository;
      }
      public async Task Handle(DepartmentDomainEvent notification, CancellationToken cancellationToken)
      {
          await _todoRepository.AddAsync(new Todo(1,$"Department({notification.Name}) Created", "Testing domain events"));
          await _todoRepository.UnitOfWork.SaveAsync(cancellationToken);
      }
}