[web] POST /api/accounting/assets/assets/{id}/split  (Cirrus.Api.Controllers.Accounting.Assets.AssetSplitController.Split)  [L51–L55] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request SplitAssetCommand -> SplitAssetCommandHandler [L54]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Assets.SplitAssetCommandHandler.Handle [L39–L376]
      └─ maps_to DepreciationRecordModel [L260]
      └─ maps_to AssetModel [L230]
        └─ automapper.registration MigrationMappingProfile (Asset->AssetModel) [L223]
        └─ automapper.registration CirrusMappingProfile (Asset->AssetModel) [L879]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L62]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<AssetGroup> (Scoped (inferred))
        └─ method WriteQuery [L60]
          └─ implementation Cirrus.Data.Repository.Accounting.Assets.AssetGroupRepository.WriteQuery
      └─ uses_service IControlledRepository<Asset> (Scoped (inferred))
        └─ method WriteQuery [L59]
          └─ implementation Cirrus.Data.Repository.Accounting.Assets.AssetRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ SplitAssetCommand
    └─ handlers 1
      └─ SplitAssetCommandHandler
    └─ mappings 2
      └─ AssetModel
      └─ DepreciationRecordModel

