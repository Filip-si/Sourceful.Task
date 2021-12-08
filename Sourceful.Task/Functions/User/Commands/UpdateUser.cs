using MediatR;
using Sourceful.Application.Models;
using Sourceful.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sourceful.Task.Functions.User.Commands
{
    public class UpdateUser
    {
        //Command
        public record Command(UserUpdateRequest User) : IRequest<Domain.Entities.User>;
        //Handler
        public class Handler : IRequestHandler<Command, Domain.Entities.User>
        {
            private readonly DbSourcefulTask _dbSourcefulTask;

            public Handler(DbSourcefulTask dbSourcefulTask)
            {
                _dbSourcefulTask = dbSourcefulTask;
            }

            public async Task<Domain.Entities.User> Handle(Command request, CancellationToken cancellationToken)
            {
                await using var transaction = await _dbSourcefulTask.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var user = _dbSourcefulTask.Users.SingleOrDefault(x => x.UserId == request.User.UserId);
                    var address = _dbSourcefulTask.UserAddresses.SingleOrDefault(x => x.UserId == request.User.UserId);
                    var setting = _dbSourcefulTask.UserSettings.SingleOrDefault(x => x.UserId == request.User.UserId);
                    if (user != null)
                    {
                        user.FirstName = request.User.FirstName;
                        user.LastName = request.User.LastName;
                        user.Age = request.User.Age;
                        address.StreetName = request.User.StreetName;
                        address.Number = request.User.Number;
                        address.PostCode = request.User.PostCode;
                        setting.Email = request.User.Email;
                        setting.Name = request.User.Name;
                    }

                    await _dbSourcefulTask.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return user;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
                    
            }
        }
    }
}
