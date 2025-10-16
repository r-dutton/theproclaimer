[web] PUT /api/loan-matrices/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.Update)  [L92–L98] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request UpdateLoanMatrixCommand [L95]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.LoanMatrices.UpdateLoanMatrixCommandHandler.Handle [L29–L66]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L55]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method WriteQuery [L42]
          └─ ... (no implementation details available)

