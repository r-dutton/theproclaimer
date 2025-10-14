[web] DELETE /api/reportsettings/policies/{id}  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.DeletePolicy)  [L141–L160] [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L152]
  └─ uses_service UserService
    └─ method IsSuperUser [L145]
  └─ sends_request GetFirmReportSettingQuery [L150]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]

