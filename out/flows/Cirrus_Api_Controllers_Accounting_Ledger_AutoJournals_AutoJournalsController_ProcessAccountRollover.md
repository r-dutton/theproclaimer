[web] PUT /api/accounting/ledger/auto-journals/process/account-rollover  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.AutoJournalsController.ProcessAccountRollover)  [L48–L53] status=200 [auth=user]
  └─ sends_request ProcessAccountRolloverJournalCommand [L51]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.AutoJournals.ProcessAccountRolloverJournalCommandHandler.Handle [L40–L224]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method ExecuteForRollover [L130]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L68]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Journal>
        └─ method Remove [L114]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L150]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L70]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

