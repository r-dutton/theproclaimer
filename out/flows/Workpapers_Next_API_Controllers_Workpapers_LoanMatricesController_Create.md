[web] POST /api/loan-matrices/  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.Create)  [L76–L82] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request CreateLoanMatrixCommand [L79]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.LoanMatrices.CreateLoanMatrixCommandHandler.Handle [L18–L34]
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method Add [L30]
          └─ ... (no implementation details available)

