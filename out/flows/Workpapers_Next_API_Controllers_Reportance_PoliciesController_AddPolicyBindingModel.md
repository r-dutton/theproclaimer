[web] POST /api/reportsettings/policies/  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.AddPolicyBindingModel)  [L110–L126] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Add [L117]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L114]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
  └─ sends_request GetFirmReportSettingQuery [L122]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]
          └─ ... (no implementation details available)

