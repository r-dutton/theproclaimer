[web] POST /api/reportsettings/customise  (Workpapers.Next.API.Controllers.Reportance.FirmReportSettingsController.Customise)  [L87–L97] [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UserService
    └─ method IsSuperUser [L91]
  └─ sends_request GetFirmReportSettingQuery [L94]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]

