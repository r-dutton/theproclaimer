[web] PUT /api/accounting/ledger/auto-journals/process/depreciation  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.AutoJournalsController.ProcessDepreciation)  [L58–L84] status=200 [auth=user]
  └─ calls DepreciationYearRepository.ReadQuery [L67]
  └─ query DepreciationYear [L67]
    └─ reads_from DepreciationYears
  └─ sends_request ProcessDepreciationJournalCommand -> ProcessDepreciationJournalCommandHandler [L80]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.AutoJournals.ProcessDepreciationJournalCommandHandler.Handle [L71–L627]
      └─ maps_to DatasetLightDto [L193]
        └─ automapper.registration CirrusMappingProfile (Dataset->DatasetLightDto) [L204]
      └─ uses_service GetMonthlyDisposalsByAssetGroupQuery
        └─ method Execute [L310]
          └─ ... (no implementation details available)
      └─ uses_service GetDepreciationByAssetGroupQuery
        └─ method Execute [L292]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<AssetGroup> (Scoped (inferred))
        └─ method ReadQuery [L262]
          └─ implementation Cirrus.Data.Repository.Accounting.Assets.AssetGroupRepository.ReadQuery
      └─ uses_service IControlledRepository<Journal> (Scoped (inferred))
        └─ method Remove [L184]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.JournalRepository.Remove
      └─ uses_service IControlledRepository<DepreciationYear> (Scoped (inferred))
        └─ method ReadQuery [L114]
          └─ implementation Cirrus.Data.Repository.Accounting.Assets.DepreciationYearRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L105]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L104]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DepreciationYear writes=0 reads=1
    └─ requests 1
      └─ ProcessDepreciationJournalCommand
    └─ handlers 1
      └─ ProcessDepreciationJournalCommandHandler
    └─ mappings 1
      └─ DatasetLightDto

