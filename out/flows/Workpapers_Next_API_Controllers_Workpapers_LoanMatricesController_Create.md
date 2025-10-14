[web] POST /api/loan-matrices/  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.Create)  [L76–L82] [auth=AuthorizationPolicies.User]
  └─ sends_request CreateLoanMatrixCommand [L79]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.LoanMatrices.CreateLoanMatrixCommandHandler.Handle [L18–L34]
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method Add [L30]

