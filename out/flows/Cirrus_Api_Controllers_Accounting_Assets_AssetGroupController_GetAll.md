[web] GET /api/accounting/assets/asset-groups/  (Cirrus.Api.Controllers.Accounting.Assets.AssetGroupController.GetAll)  [L53–L62] [auth=user]
  └─ maps_to AssetGroupDto [L57]
    └─ automapper.registration CirrusMappingProfile (AssetGroup->AssetGroupDto) [L874]
  └─ calls AssetGroupRepository.ReadQuery [L57]
  └─ queries AssetGroup [L57]
    └─ reads_from AssetGroups
  └─ uses_service IControlledRepository<AssetGroup>
    └─ method ReadQuery [L57]
  └─ sends_request CanIAccessFileQuery [L56]
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

