[web] POST /api/accounting/ledger/cashflow/journals/  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.Create)  [L81–L86] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request CreateCashflowJournalCommand [L84]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.CreateUpdateCashflowJournalCommandHandler.Handle [L57–L147]
      └─ uses_service GetCashflowLinesQuery
        └─ method Execute [L113]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<CashflowJournal>
        └─ method Add [L86]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method WriteQuery [L80]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<File>
        └─ method ReadQuery [L140]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L81]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

