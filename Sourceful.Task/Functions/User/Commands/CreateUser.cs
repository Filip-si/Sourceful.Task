using MediatR;
using Sourceful.Application.Models;
using Sourceful.Domain.Entities;
using Sourceful.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sourceful.Task.Functions.User
{
    public class CreateUser
    {
        //Command
        public record Command(UserRequest User) : IRequest<Guid>;
        //Handler
        public class Handler : IRequestHandler<Command, Guid>
        {
            private readonly DbSourcefulTask _dbSourcefulTask;

            public Handler(DbSourcefulTask dbSourcefulTask)
            {
                _dbSourcefulTask = dbSourcefulTask;
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                await using var transaction = await _dbSourcefulTask.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    Domain.Entities.User User = new()
                    {
                        FirstName = request.User.FirstName,
                        LastName = request.User.LastName,
                        Age = request.User.Age
                    };

                    _dbSourcefulTask.Users.Add(User);

                    UserAddress UserAddress = new()
                    {
                        StreetName = request.User.StreetName,
                        Number = request.User.Number,
                        PostCode = request.User.PostCode,
                        UserId = User.UserId
                    };

                    UserSetting UserSetting = new()
                    {
                        Email = request.User.Email,
                        Name = request.User.Name,
                        UserId = User.UserId
                    };

                    _dbSourcefulTask.UserAddresses.Add(UserAddress);
                    _dbSourcefulTask.UserSettings.Add(UserSetting);

                    User.UserAddressId = UserAddress.UserAddressId;
                    User.UserSettingId = UserSetting.UserSettingId;
                    await _dbSourcefulTask.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return User.UserId;
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
