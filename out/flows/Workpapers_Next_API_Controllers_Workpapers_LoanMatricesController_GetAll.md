[web] GET /api/loan-matrices/  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.GetAll)  [L70–L74] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetLoanMatricesQuery [L73]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.LoanMatrices.GetLoanMatricesQueryHandler.Handle [L28–L46]
      └─ maps_to LoanMatrixLightDto [L41]
        └─ automapper.registration WorkpapersMappingProfile (LoanMatrix->LoanMatrixLightDto) [L996]
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method ReadQuery [L41]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L43]
          └─ ... (no implementation details available)

