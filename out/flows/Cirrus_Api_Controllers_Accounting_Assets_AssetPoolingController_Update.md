[web] PUT /api/accounting/assets/pooling/allocate-to-pool  (Cirrus.Api.Controllers.Accounting.Assets.AssetPoolingController.Update)  [L56–L61] status=200 [auth=user]
  └─ sends_request AllocateAssetsToPoolCommand [L59]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Assets.AllocateAssetsToPoolCommandHandler.Handle [L29–L89]
      └─ uses_service IControlledRepository<Asset>
        └─ method OptimisedWriteQuery [L52]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DepreciationRecord>
        └─ method WriteQuery [L78]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L72]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

