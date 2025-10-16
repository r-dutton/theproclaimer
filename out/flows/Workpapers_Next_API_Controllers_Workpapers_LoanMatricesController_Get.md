[web] GET /api/loan-matrices/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.Get)  [L35–L39] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetLoanMatrixQuery [L38]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.LoanMatrices.GetLoanMatrixQueryHandler.Handle [L31–L86]
      └─ maps_to LoanMatrixDto [L58]
        └─ automapper.registration WorkpapersMappingProfile (LoanMatrix->LoanMatrixDto) [L997]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L76]
          └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
          └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
      └─ uses_service UserService
        └─ method GetUser [L76]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L74]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method ReadQuery [L58]
          └─ ... (no implementation details available)

