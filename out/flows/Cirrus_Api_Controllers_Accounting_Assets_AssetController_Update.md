[web] PUT /api/accounting/assets/assets/move  (Cirrus.Api.Controllers.Accounting.Assets.AssetController.Update)  [L165–L170] status=200 [auth=user]
  └─ sends_request MoveAssetsToNewAssetGroupCommand [L168]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Assets.MoveAssetsToNewAssetGroupCommandHandler.Handle [L26–L71]
      └─ uses_service IControlledRepository<Asset>
        └─ method OptimisedWriteQuery [L48]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<AssetGroup>
        └─ method ReadQuery [L64]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L61]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

