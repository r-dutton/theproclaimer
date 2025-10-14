[web] PUT /api/loan-matrices/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.Update)  [L92–L98] [auth=AuthorizationPolicies.User]
  └─ sends_request UpdateLoanMatrixCommand [L95]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.LoanMatrices.UpdateLoanMatrixCommandHandler.Handle [L29–L66]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L55]
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method WriteQuery [L42]

