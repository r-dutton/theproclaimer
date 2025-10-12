using AutoMapper;
using Sample.App.Dtos;
using Sample.Data.Entities;

namespace Sample.App.Mapping;

public class ReportProfile : Profile
{
    public ReportProfile()
    {
        CreateMap<Report, ReportDto>();
    }
}
