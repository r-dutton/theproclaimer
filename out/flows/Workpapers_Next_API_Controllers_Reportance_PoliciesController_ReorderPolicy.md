[web] PUT /api/reportsettings/policies/options/{id}/reorder/{index}  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.ReorderPolicy)  [L182–L200] [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UserService
    └─ method IsSuperUser [L186]
  └─ sends_request GetFirmReportSettingQuery [L191]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]

