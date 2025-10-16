[web] PUT /api/loan-matrices/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.Update)  [L92–L98] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request UpdateLoanMatrixCommand -> UpdateLoanMatrixCommandHandler [L95]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.LoanMatrices.UpdateLoanMatrixCommandHandler.Handle [L29–L66]
      └─ calls ClientRepository.ReadQuery [L55]
      └─ uses_client ClientRepository [L55]
      └─ uses_service IControlledRepository<LoanMatrix> (Scoped (inferred))
        └─ method WriteQuery [L42]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.LoanMatrixRepository.WriteQuery
  └─ impact_summary
    └─ clients 1
      └─ ClientRepository
    └─ requests 1
      └─ UpdateLoanMatrixCommand
    └─ handlers 1
      └─ UpdateLoanMatrixCommandHandler

