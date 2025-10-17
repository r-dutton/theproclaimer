[web] PUT /api/accounting/assets/pooling/allocate-to-pool  (Cirrus.Api.Controllers.Accounting.Assets.AssetPoolingController.Update)  [L56–L61] status=200 [auth=user]
  └─ sends_request AllocateAssetsToPoolCommand -> AllocateAssetsToPoolCommandHandler [L59]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Assets.AllocateAssetsToPoolCommandHandler.Handle [L29–L89]
      └─ uses_service IControlledRepository<DepreciationRecord> (Scoped (inferred))
        └─ method WriteQuery [L78]
          └─ implementation Cirrus.Data.Repository.Accounting.Assets.DepreciationRecordRepository.WriteQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L72]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Asset> (Scoped (inferred))
        └─ method OptimisedWriteQuery [L52]
          └─ implementation Cirrus.Data.Repository.Accounting.Assets.AssetRepository.OptimisedWriteQuery
  └─ impact_summary
    └─ requests 1
      └─ AllocateAssetsToPoolCommand
    └─ handlers 1
      └─ AllocateAssetsToPoolCommandHandler

