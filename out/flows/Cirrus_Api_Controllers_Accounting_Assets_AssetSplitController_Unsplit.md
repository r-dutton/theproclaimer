[web] PUT /api/accounting/assets/assets/{id}/unsplit  (Cirrus.Api.Controllers.Accounting.Assets.AssetSplitController.Unsplit)  [L61–L74] status=200 [auth=Authentication.UserPolicy]
  └─ calls AssetRepository.WriteQuery [L64]
  └─ write Asset [L64]
    └─ reads_from Assets
  └─ uses_service IControlledRepository<Asset>
    └─ method WriteQuery [L64]
      └─ ... (no implementation details available)
  └─ sends_request GetAssetsSettingsQuery [L71]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetAssetsSettingsQueryHandler.Handle [L25–L45]
      └─ uses_service IControlledRepository<DepreciationYear>
        └─ method ReadQuery [L37]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L70]
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

