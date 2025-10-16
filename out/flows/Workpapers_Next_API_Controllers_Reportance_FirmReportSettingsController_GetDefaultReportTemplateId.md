[web] GET /api/reportsettings/defaultreporttemplate/id  (Workpapers.Next.API.Controllers.Reportance.FirmReportSettingsController.GetDefaultReportTemplateId)  [L51–L58] status=200
  └─ uses_service UserService
    └─ method IsSuperUser [L54]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
  └─ sends_request GetFirmReportSettingQuery [L55]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]
          └─ ... (no implementation details available)

