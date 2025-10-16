[web] POST /api/source-accounts/from-standard-account  (Workpapers.Next.API.Controllers.SourceAccountsController.CreateFromStandardAccount)  [L159–L168] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request CreateSourceAccountFromStandardAccountCommand [L164]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.SourceAccounts.CreateSourceAccountFromStandardAccountCommandHandler.Handle [L34–L104]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L64]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L63]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Source>
        └─ method ReadQuery [L62]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method WriteQuery [L61]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L67]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L65]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

