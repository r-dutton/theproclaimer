[web] POST /api/accounting/ledger/standard-accounts/header/accept-recommendation  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.ImplementRecommendedHeader)  [L184–L189] [auth=Authentication.UserPolicy]
  └─ sends_request CreateHeaderFromStandardHeaderCommand [L187]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.CreateHeaderFromStandardHeaderCommandHandler.Handle [L48–L113]
      └─ uses_service IControlledRepository<Account>
        └─ method WriteQuery [L68]
      └─ uses_service IControlledRepository<File>
        └─ method ReadQuery [L74]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L79]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L72]

