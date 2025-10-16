[web] GET /api/loan-matrices/for-client-group/{clientGroupId:guid}  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.GetForClientGroup)  [L47–L51] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetLoanMatrixByClientGroupQuery [L50]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.LoanMatrices.GetLoanMatrixByClientGroupQueryHandler.Handle [L36–L94]
      └─ maps_to LoanMatrixDto [L63]
        └─ automapper.registration WorkpapersMappingProfile (LoanMatrix->LoanMatrixDto) [L997]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L84]
          └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
          └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
      └─ uses_service UserService
        └─ method GetUser [L84]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L82]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method ReadQuery [L63]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L66]
          └─ ... (no implementation details available)

