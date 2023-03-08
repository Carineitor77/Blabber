using Application.Common.Core;
using Application.Common.DTO.User;
using Application.Common.Enums;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.User.Queries;

public record GetAllUsersQuery : IRequest<Result<List<UserDto>>>;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<UserDto>>>
{
    private readonly BlabberContext _context;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public GetAllUsersQueryHandler(BlabberContext context, IMapper mapper, IUserService userService)
    {
        _context = context;
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<Result<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Users.AsQueryable();
        
        var userId = _userService.GetUserId();

        query = query.Where(u => u.Id != userId.ToString());

        var usersDto = await query.ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return Result<List<UserDto>>.Return(ReturnTypes.Ok, usersDto);
    }
}