[web] PUT /api/accounting/assets/pooling/write-off  (Cirrus.Api.Controllers.Accounting.Assets.AssetPoolingController.SetPoolBalance)  [L45–L51] [auth=user]
  └─ sends_request WriteOffPoolBalanceCommand [L49]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Assets.WriteOffPoolBalanceCommandHandler.Handle [L34–L88]
      └─ uses_service IControlledRepository<Asset>
        └─ method OptimisedWriteQuery [L58]
      └─ uses_service IControlledRepository<DepreciationRecord>
        └─ method WriteQuery [L75]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L73]
  └─ sends_request CanIAccessFileQuery [L48]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
      └─ uses_cache IDistributedCache [L79]
        └─ method SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache [L71]
        └─ method DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache [L68]
        └─ method CreateAccessKey [write] [L68]

