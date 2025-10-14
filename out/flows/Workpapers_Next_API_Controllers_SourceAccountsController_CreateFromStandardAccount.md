[web] POST /api/source-accounts/from-standard-account  (Workpapers.Next.API.Controllers.SourceAccountsController.CreateFromStandardAccount)  [L159–L168] [auth=AuthorizationPolicies.User]
  └─ sends_request CreateSourceAccountFromStandardAccountCommand [L164]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.SourceAccounts.CreateSourceAccountFromStandardAccountCommandHandler.Handle [L34–L104]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L64]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L63]
      └─ uses_service IControlledRepository<Source>
        └─ method ReadQuery [L62]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method WriteQuery [L61]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L67]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L65]

