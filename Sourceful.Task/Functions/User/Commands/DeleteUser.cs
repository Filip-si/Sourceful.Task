using MediatR;
using Sourceful.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sourceful.Task.Functions.User.Commands
{
    public class DeleteUser
    {
        //Command
        public record Command(Guid Id) : IRequest<string>;
        //Handler
        public class Handler : IRequestHandler<Command, string>
        {
            private readonly DbSourcefulTask _dbSourcefulTask;

            public Handler(DbSourcefulTask dbSourcefulTask)
            {
                _dbSourcefulTask = dbSourcefulTask;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                await using var transaction = await _dbSourcefulTask.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var user = _dbSourcefulTask.Users.SingleOrDefault(x => x.UserId == request.Id);
                    if (user != null)
                    {
                        var address = _dbSourcefulTask.UserAddresses.Single(x => x.UserId == request.Id);
                        var setting = _dbSourcefulTask.UserSettings.Single(x => x.UserId == request.Id);
                        _dbSourcefulTask.UserAddresses.Remove(address);
                        _dbSourcefulTask.UserSettings.Remove(setting);
                        _dbSourcefulTask.Users.Remove(user);
                    }

                    await _dbSourcefulTask.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return user == null ? "User not found" : "User has been deleted";
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
