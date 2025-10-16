[web] GET /api/loan-matrices/for-binder/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.GetForBinderId)  [L58–L62] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetLoanMatrixForBinderQuery [L61]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.LoanMatrices.GetLoanMatrixForBinderQueryHandler.Handle [L33–L102]
      └─ maps_to LoanMatrixDto [L69]
        └─ automapper.registration WorkpapersMappingProfile (LoanMatrix->LoanMatrixDto) [L997]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L92]
          └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
          └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
      └─ uses_service UserService
        └─ method GetUser [L92]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L90]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method ReadQuery [L69]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L62]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L71]
          └─ ... (no implementation details available)

