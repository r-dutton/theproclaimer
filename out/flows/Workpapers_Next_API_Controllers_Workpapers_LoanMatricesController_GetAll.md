[web] GET /api/loan-matrices/  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.GetAll)  [L70–L74] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetLoanMatricesQuery -> GetLoanMatricesQueryHandler [L73]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.LoanMatrices.GetLoanMatricesQueryHandler.Handle [L28–L46]
      └─ maps_to LoanMatrixLightDto [L41]
        └─ automapper.registration WorkpapersMappingProfile (LoanMatrix->LoanMatrixLightDto) [L996]
      └─ uses_service IControlledRepository<LoanMatrix> (Scoped (inferred))
        └─ method ReadQuery [L41]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.LoanMatrixRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetLoanMatricesQuery
    └─ handlers 1
      └─ GetLoanMatricesQueryHandler
    └─ mappings 1
      └─ LoanMatrixLightDto

