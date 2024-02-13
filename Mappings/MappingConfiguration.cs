using AutoMapper;
using zavrsni_backend.Entities;
using zavrsni_backend.Models.DTO;

namespace zavrsni_backend.Mappings
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            #region User
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.PasswordHash, act => act.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));
            #endregion

            #region
            CreateMap<Record, RecordDTO>();
            CreateMap<RecordDTO, Record>();
            #endregion
        }
    }
}
