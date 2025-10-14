[web] PUT /api/reportsettings/reporttemplates/setdefault/{id:Guid}  (Workpapers.Next.API.Controllers.Reportance.ReportTemplatesController.SetDefault)  [L94–L109] [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L101]
  └─ uses_service UserService
    └─ method IsSuperUser [L98]
  └─ sends_request GetFirmReportSettingQuery [L99]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]

