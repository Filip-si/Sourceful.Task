using MediatR;
using Microsoft.EntityFrameworkCore;
using Sourceful.Application.Models;
using Sourceful.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sourceful.Task.Functions.User
{
    public class GetUserById
    {
        //Query
        public record Query(Guid Id) : IRequest<UserResponse>;

        //Handler
        public class Handler : IRequestHandler<Query, UserResponse>
        {
            private readonly DbSourcefulTask _dbSourcefulTask;

            public Handler(DbSourcefulTask dbSourcefulTask)
            {
                _dbSourcefulTask = dbSourcefulTask;
            }
            public async Task<UserResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = _dbSourcefulTask.Users.SingleOrDefault(x => x.UserId == request.Id);
                var address = await _dbSourcefulTask.UserAddresses.SingleAsync(x => x.UserId == request.Id, cancellationToken: cancellationToken);
                var setting = await _dbSourcefulTask.UserSettings.SingleAsync(x => x.UserId == request.Id, cancellationToken: cancellationToken);
                return user == null ? null : 
                    new UserResponse(
                    user.UserId, 
                    user.FirstName, 
                    user.LastName, 
                    user.Age, 
                    address.StreetName, 
                    address.Number, 
                    address.PostCode,
                    setting.Email,
                    setting.Name);
            }
        }
    }
}
