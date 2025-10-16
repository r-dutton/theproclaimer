[web] PUT /api/accounting/ledger/auto-journals/process/distribution  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.AutoJournalsController.ProcessDistribution)  [L37–L42] status=200 [auth=user]
  └─ sends_request ProcessDistributionJournalCommand [L40]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.AutoJournals.ProcessDistributionJournalCommandHandler.Handle [L37–L233]
      └─ uses_service GetNetProfitForDatesQuery
        └─ method Execute [L112]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L57]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Distribution>
        └─ method WriteQuery [L104]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Journal>
        └─ method Remove [L91]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L58]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

