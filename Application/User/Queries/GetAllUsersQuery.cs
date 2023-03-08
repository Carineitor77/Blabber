using Application.Common.Core;
using Application.Common.DTO.User;
using Application.Common.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.User.Queries;

public record GetAllUsersQuery(Guid Id) : IRequest<Result<List<UserDto>>>;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<UserDto>>>
{
    private readonly BlabberContext _context;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(BlabberContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Result<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Users.AsQueryable();

        query = query.Where(u => u.Id != request.Id.ToString());

        var usersDto = await query.ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return Result<List<UserDto>>.Return(ReturnTypes.Ok, usersDto);
    }
}