[web] POST /api/reportsettings/policies/  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.AddPolicyBindingModel)  [L110–L126] [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Add [L117]
  └─ uses_service UserService
    └─ method IsSuperUser [L114]
  └─ sends_request GetFirmReportSettingQuery [L122]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]

