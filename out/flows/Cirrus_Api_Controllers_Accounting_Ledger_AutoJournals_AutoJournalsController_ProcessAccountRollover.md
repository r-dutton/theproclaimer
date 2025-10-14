[web] PUT /api/accounting/ledger/auto-journals/process/account-rollover  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.AutoJournalsController.ProcessAccountRollover)  [L48–L53] [auth=user]
  └─ sends_request ProcessAccountRolloverJournalCommand [L51]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.AutoJournals.ProcessAccountRolloverJournalCommandHandler.Handle [L40–L224]
      └─ uses_service GetTrialBalanceForDatesQuery
        └─ method ExecuteForRollover [L130]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L68]
      └─ uses_service IControlledRepository<Journal>
        └─ method Remove [L114]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L150]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L70]

