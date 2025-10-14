[web] PUT /api/accounting/assets/dispose/  (Cirrus.Api.Controllers.Accounting.Assets.AssetDisposalController.BulkDispose)  [L46–L53] [auth=user]
  └─ sends_request BulkDisposalCommand [L51]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Assets.BulkDisposalCommandHandler.Handle [L38–L150]
      └─ uses_service IControlledRepository<Asset>
        └─ method ReadQuery [L68]
      └─ uses_service IControlledRepository<DepreciationRecord>
        └─ method ReadQuery [L109]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L93]
  └─ sends_request CanIAccessFileQuery [L50]
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

