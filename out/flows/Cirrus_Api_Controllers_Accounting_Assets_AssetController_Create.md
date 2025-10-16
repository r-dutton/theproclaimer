[web] POST /api/accounting/assets/assets/  (Cirrus.Api.Controllers.Accounting.Assets.AssetController.Create)  [L98–L111] status=201 [auth=user]
  └─ calls AssetGroupRepository.WriteQuery [L102]
  └─ calls AssetRepository.Add [L108]
  └─ insert Asset [L108]
    └─ reads_from Assets
  └─ write AssetGroup [L102]
    └─ reads_from AssetGroups
  └─ uses_service IControlledRepository<Asset>
    └─ method Add [L108]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<AssetGroup>
    └─ method WriteQuery [L102]
      └─ ... (no implementation details available)
  └─ sends_request GetAssetsSettingsQuery [L105]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetAssetsSettingsQueryHandler.Handle [L25–L45]
      └─ uses_service IControlledRepository<DepreciationYear>
        └─ method ReadQuery [L37]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L103]
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

