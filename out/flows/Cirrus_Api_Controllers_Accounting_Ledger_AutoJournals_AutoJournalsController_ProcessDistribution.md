[web] PUT /api/accounting/ledger/auto-journals/process/distribution  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.AutoJournalsController.ProcessDistribution)  [L37–L42] status=200 [auth=user]
  └─ sends_request ProcessDistributionJournalCommand -> ProcessDistributionJournalCommandHandler [L40]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.AutoJournals.ProcessDistributionJournalCommandHandler.Handle [L37–L233]
      └─ uses_service GetNetProfitForDatesQuery
        └─ method Execute [L112]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Distribution> (Scoped (inferred))
        └─ method WriteQuery [L104]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AutoJournals.DistributionRepository.WriteQuery
      └─ uses_service IControlledRepository<Journal> (Scoped (inferred))
        └─ method Remove [L91]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.JournalRepository.Remove
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L58]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L57]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ ProcessDistributionJournalCommand
    └─ handlers 1
      └─ ProcessDistributionJournalCommandHandler

