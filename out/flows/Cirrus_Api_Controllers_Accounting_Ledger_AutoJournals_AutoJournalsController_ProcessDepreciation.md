[web] PUT /api/accounting/ledger/auto-journals/process/depreciation  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.AutoJournalsController.ProcessDepreciation)  [L58–L84] [auth=user]
  └─ calls DepreciationYearRepository.ReadQuery [L67]
  └─ queries DepreciationYear [L67]
    └─ reads_from DepreciationYears
  └─ uses_service IControlledRepository<DepreciationYear>
    └─ method ReadQuery [L67]
  └─ sends_request ProcessDepreciationJournalCommand [L80]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.AutoJournals.ProcessDepreciationJournalCommandHandler.Handle [L71–L627]
      └─ maps_to DatasetLightDto [L193]
        └─ automapper.registration CirrusMappingProfile (Dataset->DatasetLightDto) [L204]
      └─ maps_to DatasetLightDto [L120]
        └─ automapper.registration CirrusMappingProfile (Dataset->DatasetLightDto) [L204]
      └─ uses_service GetDepreciationByAssetGroupQuery
        └─ method Execute [L292]
      └─ uses_service GetMonthlyDisposalsByAssetGroupQuery
        └─ method Execute [L310]
      └─ uses_service IControlledRepository<AssetGroup>
        └─ method ReadQuery [L262]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L104]
      └─ uses_service IControlledRepository<DepreciationYear>
        └─ method ReadQuery [L114]
      └─ uses_service IControlledRepository<Journal>
        └─ method Remove [L184]
      └─ uses_service IMapper
        └─ method Map [L120]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L105]

