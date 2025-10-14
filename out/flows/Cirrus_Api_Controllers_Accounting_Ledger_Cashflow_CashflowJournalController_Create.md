[web] POST /api/accounting/ledger/cashflow/journals/  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.Create)  [L81–L86] [auth=Authentication.UserPolicy]
  └─ sends_request CreateCashflowJournalCommand [L84]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.CreateUpdateCashflowJournalCommandHandler.Handle [L57–L147]
      └─ uses_service GetCashflowLinesQuery
        └─ method Execute [L113]
      └─ uses_service IControlledRepository<CashflowJournal>
        └─ method Add [L86]
      └─ uses_service IControlledRepository<Dataset>
        └─ method WriteQuery [L80]
      └─ uses_service IControlledRepository<File>
        └─ method ReadQuery [L140]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L81]

