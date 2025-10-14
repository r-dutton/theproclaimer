[web] PUT /api/loan-matrices/refresh-balances  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.RefreshBalances)  [L108–L115] [auth=AuthorizationPolicies.User]
  └─ sends_request RefreshLoanMatrixBalancesCommand [L112]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.LoanMatrices.RefreshLoanMatrixBalancesCommandHandler.Handle [L36–L115]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L93]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L92]
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method WriteQuery [L60]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L102]
      └─ uses_service UserService
        └─ method GetUser [L93]

