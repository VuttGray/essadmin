using AutoMapper;
using ESS.Admin.Core.Domain.Administration;
using ESS.Admin.WebHost.Models;

namespace ESS.Admin.WebHost.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<CreateOrEditUserRequest, User>();

            CreateMap<Message, MessageResponse>();
            CreateMap<CreateMessageRequest, Message>();
            CreateMap<EditMessageRequest, Message>();
        }
    }
}
