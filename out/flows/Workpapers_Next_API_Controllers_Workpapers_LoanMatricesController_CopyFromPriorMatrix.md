[web] POST /api/loan-matrices/copy-from-prior  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.CopyFromPriorMatrix)  [L84–L90] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request CreateLoanMatrixFromPriorMatrixCommand -> CreateLoanMatrixFromPriorMatrixCommandHandler [L87]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.LoanMatrices.CreateLoanMatrixFromPriorMatrixCommandHandler.Handle [L29–L92]
      └─ uses_service IControlledRepository<LoanMatrix> (Scoped (inferred))
        └─ method WriteQuery [L51]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.LoanMatrixRepository.WriteQuery
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L42]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ CreateLoanMatrixFromPriorMatrixCommand
    └─ handlers 1
      └─ CreateLoanMatrixFromPriorMatrixCommandHandler

