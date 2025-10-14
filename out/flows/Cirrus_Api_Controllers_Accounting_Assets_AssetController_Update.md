[web] PUT /api/accounting/assets/assets/move  (Cirrus.Api.Controllers.Accounting.Assets.AssetController.Update)  [L165–L170] [auth=user]
  └─ sends_request MoveAssetsToNewAssetGroupCommand [L168]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Assets.MoveAssetsToNewAssetGroupCommandHandler.Handle [L26–L71]
      └─ uses_service IControlledRepository<Asset>
        └─ method OptimisedWriteQuery [L48]
      └─ uses_service IControlledRepository<AssetGroup>
        └─ method ReadQuery [L64]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L61]

