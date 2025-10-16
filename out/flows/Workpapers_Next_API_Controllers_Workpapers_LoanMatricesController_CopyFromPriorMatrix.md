[web] POST /api/loan-matrices/copy-from-prior  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.CopyFromPriorMatrix)  [L84–L90] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request CreateLoanMatrixFromPriorMatrixCommand [L87]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.LoanMatrices.CreateLoanMatrixFromPriorMatrixCommandHandler.Handle [L29–L92]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L42]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method WriteQuery [L51]
          └─ ... (no implementation details available)

