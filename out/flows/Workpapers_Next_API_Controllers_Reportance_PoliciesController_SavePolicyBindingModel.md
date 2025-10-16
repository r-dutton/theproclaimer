[web] PUT /api/reportsettings/policies/  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.SavePolicyBindingModel)  [L128–L139] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UserService
    └─ method IsSuperUser [L133]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
  └─ sends_request GetEditablePolicyQuery [L133]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetEditablePolicyQueryHandler.Handle [L13–L48]
      └─ uses_service RequestProcessor
        └─ method Process [L39]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.Process [L9-L32]
      └─ uses_service UnitOfWork
        └─ method Table [L27]
          └─ ... (no implementation details available)

