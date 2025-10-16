[web] GET /api/reportsettings/colour  (Workpapers.Next.API.Controllers.Reportance.FirmReportSettingsController.GetPrimaryColour)  [L42–L49] status=200
  └─ uses_service UserService
    └─ method IsSuperUser [L45]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
  └─ sends_request GetFirmReportSettingQuery [L46]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]
          └─ ... (no implementation details available)

