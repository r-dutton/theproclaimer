using AutoMapper;
using Sample.App.Commands;
using Sample.App.Dtos;
using Sample.Data.Entities;

namespace Sample.App.Mapping;

public class ReportProfile : Profile
{
    public ReportProfile()
    {
        CreateMap<CreateDetailedReportDto, CreateDetailedReportCommand>();

        CreateMap<CreateDetailedReportCommand, Report>()
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
            .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Body, opt => opt.MapFrom(src => $"Report generated for {src.TenantId}"));

        CreateMap<Report, ReportDto>()
            .ForMember(dest => dest.Classification, opt => opt.MapFrom(src => src.Metadata.Classification));

        CreateMap<Report, ReportSummaryDto>();
    }
}
