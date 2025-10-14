[web] GET /api/accounting/assets/assets/  (Cirrus.Api.Controllers.Accounting.Assets.AssetController.Get)  [L60–L92] [auth=user]
  └─ calls AssetRepository.ReadQuery [L71]
  └─ calls DepreciationRecordRepository.ReadQuery [L78]
  └─ queries Asset [L71]
    └─ reads_from Assets
  └─ queries DepreciationRecord [L78]
    └─ reads_from DepreciationRecords
  └─ uses_service IControlledRepository<Asset>
    └─ method ReadQuery [L71]
  └─ uses_service IControlledRepository<DepreciationRecord>
    └─ method ReadQuery [L78]
  └─ sends_request CanIAccessFileQuery [L69]
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

