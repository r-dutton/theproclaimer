[web] PUT /api/accounting/ledger/accounts/{id:Guid}/link-standard  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.LinkStandardAccount)  [L525–L529] status=200 [auth=user]
  └─ sends_request LinkToStandardAccountCommand [L528]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.LinkToStandardAccountCommandHandler.Handle [L45–L183]
      └─ uses_service IControlledRepository<Account>
        └─ method WriteQuery [L65]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L101]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L69]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L63]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

