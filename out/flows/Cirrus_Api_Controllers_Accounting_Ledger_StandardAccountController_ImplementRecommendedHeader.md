[web] POST /api/accounting/ledger/standard-accounts/header/accept-recommendation  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.ImplementRecommendedHeader)  [L184–L189] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request CreateHeaderFromStandardHeaderCommand [L187]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.CreateHeaderFromStandardHeaderCommandHandler.Handle [L48–L113]
      └─ uses_service IControlledRepository<Account>
        └─ method WriteQuery [L68]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<File>
        └─ method ReadQuery [L74]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L79]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L72]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

