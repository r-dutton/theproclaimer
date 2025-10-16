[web] PUT /api/accounting/assets/pooling/write-off  (Cirrus.Api.Controllers.Accounting.Assets.AssetPoolingController.SetPoolBalance)  [L45–L51] status=200 [auth=user]
  └─ sends_request WriteOffPoolBalanceCommand [L49]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Assets.WriteOffPoolBalanceCommandHandler.Handle [L34–L88]
      └─ uses_service IControlledRepository<Asset>
        └─ method OptimisedWriteQuery [L58]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DepreciationRecord>
        └─ method WriteQuery [L75]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L73]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L48]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
          └─ implementation ITenantService.GetCurrentTenant [L14-L14]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L68]

