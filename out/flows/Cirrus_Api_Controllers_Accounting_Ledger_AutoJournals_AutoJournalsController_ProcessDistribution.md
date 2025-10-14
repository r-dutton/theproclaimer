[web] PUT /api/accounting/ledger/auto-journals/process/distribution  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.AutoJournalsController.ProcessDistribution)  [L37–L42] [auth=user]
  └─ sends_request ProcessDistributionJournalCommand [L40]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.AutoJournals.ProcessDistributionJournalCommandHandler.Handle [L37–L233]
      └─ uses_service GetNetProfitForDatesQuery
        └─ method Execute [L112]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L57]
      └─ uses_service IControlledRepository<Distribution>
        └─ method WriteQuery [L104]
      └─ uses_service IControlledRepository<Journal>
        └─ method Remove [L91]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L58]

