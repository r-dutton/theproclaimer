[web] PUT /api/accounting/assets/assets/calculate  (Cirrus.Api.Controllers.Accounting.Assets.AssetController.Calculate)  [L130–L144] [auth=user]
  └─ maps_to AssetDto [L143]
  └─ calls AssetGroupRepository.ReadQuery [L133]
  └─ calls AssetRepository.WriteQuery [L139]
  └─ writes_to Asset [L139]
    └─ reads_from Assets
  └─ queries AssetGroup [L133]
    └─ reads_from AssetGroups
  └─ uses_service IControlledRepository<Asset>
    └─ method WriteQuery [L139]
  └─ uses_service IControlledRepository<AssetGroup>
    └─ method ReadQuery [L133]
  └─ uses_service IMapper
    └─ method Map [L143]
  └─ sends_request GetAssetsSettingsQuery [L135]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetAssetsSettingsQueryHandler.Handle [L25–L45]
      └─ uses_service IControlledRepository<DepreciationYear>
        └─ method ReadQuery [L37]
  └─ sends_request CanIAccessFileQuery [L134]
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

