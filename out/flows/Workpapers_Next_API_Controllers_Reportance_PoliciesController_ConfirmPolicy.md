[web] PUT /api/reportsettings/policies/options/{id}/confirm  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.ConfirmPolicy)  [L202–L213] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L206]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetFirmId [L207]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]

