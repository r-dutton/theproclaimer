[web] PUT /api/accounting/ledger/auto-journals/process/account-rollover  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.AutoJournalsController.ProcessAccountRollover)  [L48–L53] status=200 [auth=user]
  └─ sends_request ProcessAccountRolloverJournalCommand -> ProcessAccountRolloverJournalCommandHandler [L51]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.AutoJournals.ProcessAccountRolloverJournalCommandHandler.Handle [L40–L224]
      └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
        └─ method ReadQuery [L150]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method ExecuteForRollover [L130]
          └─ resolves_request Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQuery.ExecuteForRollover
            └─ handled_by Workpapers.Next.ApplicationService.Queries.LedgerReports.GetTrialBalanceForDatesQueryHandler.Handle [L45–L251]
              └─ calls SourceAccountRepository.ReadQuery [L151]
      └─ uses_service IControlledRepository<Journal> (Scoped (inferred))
        └─ method Remove [L114]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.JournalRepository.Remove
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L70]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L68]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
  └─ impact_summary
    └─ requests 2
      └─ GetTrialBalanceForDatesQuery
      └─ ProcessAccountRolloverJournalCommand
    └─ handlers 2
      └─ GetTrialBalanceForDatesQueryHandler
      └─ ProcessAccountRolloverJournalCommandHandler

