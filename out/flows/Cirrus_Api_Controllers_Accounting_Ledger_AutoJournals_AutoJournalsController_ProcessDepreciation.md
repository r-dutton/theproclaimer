[web] PUT /api/accounting/ledger/auto-journals/process/depreciation  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.AutoJournalsController.ProcessDepreciation)  [L58–L84] status=200 [auth=user]
  └─ calls DepreciationYearRepository.ReadQuery [L67]
  └─ query DepreciationYear [L67]
    └─ reads_from DepreciationYears
  └─ uses_service IControlledRepository<DepreciationYear>
    └─ method ReadQuery [L67]
      └─ ... (no implementation details available)
  └─ sends_request ProcessDepreciationJournalCommand [L80]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.AutoJournals.ProcessDepreciationJournalCommandHandler.Handle [L71–L627]
      └─ maps_to DatasetLightDto [L193]
        └─ automapper.registration CirrusMappingProfile (Dataset->DatasetLightDto) [L204]
      └─ maps_to DatasetLightDto [L120]
        └─ automapper.registration CirrusMappingProfile (Dataset->DatasetLightDto) [L204]
      └─ uses_service GetDepreciationByAssetGroupQuery
        └─ method Execute [L292]
          └─ ... (no implementation details available)
      └─ uses_service GetMonthlyDisposalsByAssetGroupQuery
        └─ method Execute [L310]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<AssetGroup>
        └─ method ReadQuery [L262]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L104]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DepreciationYear>
        └─ method ReadQuery [L114]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Journal>
        └─ method Remove [L184]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L120]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L105]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

