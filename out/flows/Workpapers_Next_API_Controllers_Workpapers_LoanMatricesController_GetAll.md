[web] GET /api/loan-matrices/  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.GetAll)  [L70–L74] [auth=AuthorizationPolicies.User]
  └─ sends_request GetLoanMatricesQuery [L73]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.LoanMatrices.GetLoanMatricesQueryHandler.Handle [L28–L46]
      └─ maps_to LoanMatrixLightDto [L41]
        └─ automapper.registration WorkpapersMappingProfile (LoanMatrix->LoanMatrixLightDto) [L995]
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method ReadQuery [L41]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L43]

