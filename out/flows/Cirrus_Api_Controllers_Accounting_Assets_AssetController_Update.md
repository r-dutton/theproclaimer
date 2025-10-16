[web] PUT /api/accounting/assets/assets/move  (Cirrus.Api.Controllers.Accounting.Assets.AssetController.Update)  [L165–L170] status=200 [auth=user]
  └─ sends_request MoveAssetsToNewAssetGroupCommand -> MoveAssetsToNewAssetGroupCommandHandler [L168]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Assets.MoveAssetsToNewAssetGroupCommandHandler.Handle [L26–L71]
      └─ uses_service IControlledRepository<AssetGroup> (Scoped (inferred))
        └─ method ReadQuery [L64]
          └─ implementation Cirrus.Data.Repository.Accounting.Assets.AssetGroupRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L61]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Asset> (Scoped (inferred))
        └─ method OptimisedWriteQuery [L48]
          └─ implementation Cirrus.Data.Repository.Accounting.Assets.AssetRepository.OptimisedWriteQuery
  └─ impact_summary
    └─ requests 1
      └─ MoveAssetsToNewAssetGroupCommand
    └─ handlers 1
      └─ MoveAssetsToNewAssetGroupCommandHandler

