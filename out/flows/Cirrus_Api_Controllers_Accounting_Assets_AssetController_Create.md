[web] POST /api/accounting/assets/assets/  (Cirrus.Api.Controllers.Accounting.Assets.AssetController.Create)  [L98–L111] [auth=user]
  └─ calls AssetGroupRepository.WriteQuery [L102]
  └─ calls AssetRepository.Add [L108]
  └─ writes_to Asset [L108]
    └─ reads_from Assets
  └─ writes_to AssetGroup [L102]
    └─ reads_from AssetGroups
  └─ uses_service IControlledRepository<Asset>
    └─ method Add [L108]
  └─ uses_service IControlledRepository<AssetGroup>
    └─ method WriteQuery [L102]
  └─ sends_request GetAssetsSettingsQuery [L105]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetAssetsSettingsQueryHandler.Handle [L25–L45]
      └─ uses_service IControlledRepository<DepreciationYear>
        └─ method ReadQuery [L37]
  └─ sends_request CanIAccessFileQuery [L103]
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

