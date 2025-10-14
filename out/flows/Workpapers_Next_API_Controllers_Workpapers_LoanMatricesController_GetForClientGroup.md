[web] GET /api/loan-matrices/for-client-group/{clientGroupId:guid}  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.GetForClientGroup)  [L47–L51] [auth=AuthorizationPolicies.User]
  └─ sends_request GetLoanMatrixByClientGroupQuery [L50]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.LoanMatrices.GetLoanMatrixByClientGroupQueryHandler.Handle [L36–L94]
      └─ maps_to LoanMatrixDto [L63]
        └─ automapper.registration WorkpapersMappingProfile (LoanMatrix->LoanMatrixDto) [L996]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L84]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L82]
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method ReadQuery [L63]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L66]
      └─ uses_service UserService
        └─ method GetUser [L84]

