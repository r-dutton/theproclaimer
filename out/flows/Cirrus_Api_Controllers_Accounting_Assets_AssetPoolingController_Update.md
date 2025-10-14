[web] PUT /api/accounting/assets/pooling/allocate-to-pool  (Cirrus.Api.Controllers.Accounting.Assets.AssetPoolingController.Update)  [L56–L61] [auth=user]
  └─ sends_request AllocateAssetsToPoolCommand [L59]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Assets.AllocateAssetsToPoolCommandHandler.Handle [L29–L89]
      └─ uses_service IControlledRepository<Asset>
        └─ method OptimisedWriteQuery [L52]
      └─ uses_service IControlledRepository<DepreciationRecord>
        └─ method WriteQuery [L78]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L72]

