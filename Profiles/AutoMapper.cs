using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace TaskScheduler.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TaskScheduler.Data.Models.TaskList, TaskScheduler.Dtos.TaskListDto>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName));

            CreateMap<TaskScheduler.Dtos.TaskListDto, TaskScheduler.Data.Models.TaskList>()
            .ForMember(dest => dest.ApplicationUser, opt => opt.Ignore());

            //} CreateMap<TaskScheduler.Data.Models.TaskList,TaskScheduler.Dtos.TaskListDto>()
            //    .ForMember(
            //        dest => dest.Id,
            //        opt => opt.MapFrom(src => src.Id)
            //    )
            //    .ForMember(
            //        dest => dest.Title,
            //        opt => opt.MapFrom(src => src.Title)
            //    )
            //    .ForMember(
            //        dest => dest.Description,
            //        opt => opt.MapFrom(src => src.Description)
            //    )
            //    .ForMember(
            //        dest => dest.DueDate,
            //        opt => opt.MapFrom(src => src.DueDate)
            //    )
            // .ForMember(
            //    dest => dest.UserId,
            //    opt => opt.MapFrom(src => src.UserId)
            //)
            // .ForMember(
            //    dest => dest.UserName,
            //    opt => opt.MapFrom(src => src.ApplicationUser.UserName)
            //).ReverseMap();
        }
    }
}
