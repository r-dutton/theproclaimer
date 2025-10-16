[web] PUT /api/accounting/assets/assets/calculate  (Cirrus.Api.Controllers.Accounting.Assets.AssetController.Calculate)  [L130–L144] status=200 [auth=user]
  └─ maps_to AssetDto [L143]
  └─ calls AssetGroupRepository.ReadQuery [L133]
  └─ calls AssetRepository.WriteQuery [L139]
  └─ write Asset [L139]
    └─ reads_from Assets
  └─ query AssetGroup [L133]
    └─ reads_from AssetGroups
  └─ uses_service IControlledRepository<Asset>
    └─ method WriteQuery [L139]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<AssetGroup>
    └─ method ReadQuery [L133]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L143]
      └─ ... (no implementation details available)
  └─ sends_request GetAssetsSettingsQuery [L135]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetAssetsSettingsQueryHandler.Handle [L25–L45]
      └─ uses_service IControlledRepository<DepreciationYear>
        └─ method ReadQuery [L37]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L134]
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

