[web] GET /api/loan-matrices/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.Get)  [L35–L39] [auth=AuthorizationPolicies.User]
  └─ sends_request GetLoanMatrixQuery [L38]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.LoanMatrices.GetLoanMatrixQueryHandler.Handle [L31–L86]
      └─ maps_to LoanMatrixDto [L58]
        └─ automapper.registration WorkpapersMappingProfile (LoanMatrix->LoanMatrixDto) [L996]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L76]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L74]
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method ReadQuery [L58]
      └─ uses_service UserService
        └─ method GetUser [L76]

