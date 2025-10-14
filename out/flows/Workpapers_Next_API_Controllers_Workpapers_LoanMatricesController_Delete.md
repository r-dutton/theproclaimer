[web] DELETE /api/loan-matrices/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.Delete)  [L117–L123] [auth=AuthorizationPolicies.User]
  └─ sends_request DeleteLoanMatrixCommand [L120]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.LoanMatrices.DeleteLoanMatrixCommandHandler.Handle [L23–L40]
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method WriteQuery [L34]

