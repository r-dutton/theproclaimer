[web] POST /api/accounting/assets/assets/{id}/split  (Cirrus.Api.Controllers.Accounting.Assets.AssetSplitController.Split)  [L51–L55] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request SplitAssetCommand [L54]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Assets.SplitAssetCommandHandler.Handle [L39–L376]
      └─ maps_to AssetModel [L230]
        └─ automapper.registration CirrusMappingProfile (Asset->AssetModel) [L879]
        └─ automapper.registration MigrationMappingProfile (Asset->AssetModel) [L223]
      └─ maps_to DepreciationRecordModel [L260]
      └─ maps_to DepreciationRecordModel [L244]
      └─ uses_service IControlledRepository<Asset>
        └─ method WriteQuery [L59]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<AssetGroup>
        └─ method WriteQuery [L60]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L230]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L62]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

