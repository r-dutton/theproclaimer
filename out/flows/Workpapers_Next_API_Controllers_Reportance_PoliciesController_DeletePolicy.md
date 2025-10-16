[web] DELETE /api/reportsettings/policies/{id}  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.DeletePolicy)  [L141–L160] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L152]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L145]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
  └─ sends_request GetFirmReportSettingQuery [L150]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]
          └─ ... (no implementation details available)

