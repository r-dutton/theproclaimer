[web] PUT /api/loan-matrices/refresh-balances  (Workpapers.Next.API.Controllers.Workpapers.LoanMatricesController.RefreshBalances)  [L108–L115] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request RefreshLoanMatrixBalancesCommand [L112]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.LoanMatrices.RefreshLoanMatrixBalancesCommandHandler.Handle [L36–L115]
      └─ uses_service UserService
        └─ method GetUser [L93]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L92]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<LoanMatrix>
        └─ method WriteQuery [L60]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L102]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettings [L93]
          └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]

