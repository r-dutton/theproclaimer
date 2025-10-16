[web] POST /api/accounting/ledger/cashflow/journals/  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowJournalController.Create)  [L81–L86] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request CreateCashflowJournalCommand -> CreateUpdateCashflowJournalCommandHandler [L84]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.CreateUpdateCashflowJournalCommandHandler.Handle [L57–L147]
      └─ uses_service IControlledRepository<File> (Scoped (inferred))
        └─ method ReadQuery [L140]
          └─ implementation Cirrus.Data.Repository.Accounting.FileRepository.ReadQuery
      └─ uses_service GetCashflowLinesQuery
        └─ method Execute [L113]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<CashflowJournal> (Scoped (inferred))
        └─ method Add [L86]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.Cashflow.CashflowJournalRepository.Add
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L81]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method WriteQuery [L80]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ CreateCashflowJournalCommand
    └─ handlers 1
      └─ CreateUpdateCashflowJournalCommandHandler

