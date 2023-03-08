using Application.Common.Core;
using Application.Common.DTO.User;
using Application.Common.Enums;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.User.Queries;

public record GetAllUsersQuery : IRequest<Result<List<UserDto>>>;

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
        var users = await _context.Users.ToListAsync(cancellationToken);

        var usersDto = _mapper.Map<List<AuthUser>, List<UserDto>>(users);

        return Result<List<UserDto>>.Return(ReturnTypes.Ok, usersDto);
    }
}