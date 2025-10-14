[web] GET /api/loan-matrices/for-binder/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.GetForBinderId)  [L58–L62] [auth=AuthorizationPolicies.User]
  └─ sends_request GetLoanMatrixForBinderQuery [L61]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.LoanMatrices.GetLoanMatrixForBinderQueryHandler.Handle [L33–L102]
      └─ maps_to LoanMatrixDto [L69]
        └─ automapper.registration WorkpapersMappingProfile (LoanMatrix->LoanMatrixDto) [L996]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L92]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L90]
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method ReadQuery [L69]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L62]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L71]
      └─ uses_service UserService
        └─ method GetUser [L92]

