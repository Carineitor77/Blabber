using Application.Common.Core;
using Application.Common.DTO.User;
using Application.Common.Enums;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.User.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDto>>;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly BlabberContext _context;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(BlabberContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id.ToString(), cancellationToken);

        if (user is null)
        {
            return Result<UserDto>.Return(ReturnTypes.NotFound, message: "User with this id not found");
        }

        var userDto = _mapper.Map<UserDto>(user);

        return Result<UserDto>.Return(ReturnTypes.Ok, userDto);
    }
}