[web] PUT /api/reportsettings/policies/options/{id}  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.UpdatePolicyOptions)  [L162–L180] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UserService
    └─ method IsSuperUser [L166]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
  └─ sends_request GetFirmReportSettingQuery [L171]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]
          └─ ... (no implementation details available)

