[web] POST /api/loan-matrices/copy-from-prior  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.CopyFromPriorMatrix)  [L84–L90] [auth=AuthorizationPolicies.User]
  └─ sends_request CreateLoanMatrixFromPriorMatrixCommand [L87]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.LoanMatrices.CreateLoanMatrixFromPriorMatrixCommandHandler.Handle [L29–L92]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L42]
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method WriteQuery [L51]

