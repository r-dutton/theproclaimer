using AutoMapper;
using Sample.App.Commands;
using Sample.App.Dtos;
using Sample.App.Notifications;
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

        CreateMap<ReportGeneratedNotification, ReportAudit>()
            .ForMember(dest => dest.ReportId, opt => opt.MapFrom(src => src.ReportId))
            .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.RecordedAt, opt => opt.Ignore());
    }
}
