[web] GET /api/reportsettings/defaultreporttemplate/id  (Workpapers.Next.API.Controllers.Reportance.FirmReportSettingsController.GetDefaultReportTemplateId)  [L51–L58]
  └─ uses_service UserService
    └─ method IsSuperUser [L54]
  └─ sends_request GetFirmReportSettingQuery [L55]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]

