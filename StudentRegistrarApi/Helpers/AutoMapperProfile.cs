using AutoMapper;
using StudentRegistrarApi.ApiModels.Request;
using StudentRegistrarApi.ApiModels.Update;
using StudentRegistrarApi.Models;

namespace StudentRegistrarApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateRequest -> Student
            CreateMap<CreateRequest, Student>();

            // UpdateRequest -> Student
            CreateMap<UpdateRequest, Student>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                    // ignore both null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    // ignore null role
                        //if (x.DestinationMember.Name == "Id" && src.Id == null) return false;

                        return true;
                    }
                ));
        }
    }
}
