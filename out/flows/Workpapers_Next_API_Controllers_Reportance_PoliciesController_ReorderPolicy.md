[web] PUT /api/reportsettings/policies/options/{id}/reorder/{index}  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.ReorderPolicy)  [L182–L200] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UserService
    └─ method IsSuperUser [L186]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
  └─ sends_request GetFirmReportSettingQuery [L191]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]
          └─ ... (no implementation details available)

