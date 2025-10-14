[web] GET /api/accounting/files/for-entity/{entityId}  (Cirrus.Api.Controllers.Accounting.FilesController.GetForEntity)  [L137–L158] [auth=user]
  └─ maps_to FileLightDto [L153]
    └─ automapper.registration CirrusMappingProfile (File->FileLightDto) [L192]
  └─ calls FileRepository.ReadQuery [L153]
  └─ calls EntityRepository.ReadQuery [L145]
  └─ queries File [L153]
    └─ reads_from Files
  └─ queries Entity [L145]
  └─ uses_service IControlledRepository<Entity>
    └─ method ReadQuery [L145]
  └─ uses_service IControlledRepository<File>
    └─ method ReadQuery [L153]
  └─ sends_request CanIAccessEntityQuery [L152]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessEntityQueryHandler.Handle [L41–L99]
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L81]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L67]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L83]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L72]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L72]
      └─ uses_cache IDistributedCache [L92]
        └─ method SetRecordAsync [write] [L92]
      └─ uses_cache IDistributedCache [L74]
        └─ method DoesRecordExistAsync [access] [L74]
      └─ uses_cache IDistributedCache [L72]
        └─ method CreateAccessKey [write] [L72]

