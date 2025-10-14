[web] GET /api/accounting/datasets/{id}  (Cirrus.Api.Controllers.Accounting.DatasetsController.Get)  [L51–L58] [auth=user]
  └─ maps_to DatasetDto [L55]
    └─ automapper.registration CirrusMappingProfile (Dataset->DatasetDto) [L202]
    └─ automapper.registration ExternalApiMappingProfile (Dataset->DatasetDto) [L64]
  └─ calls DatasetRepository.ReadQuery [L55]
  └─ queries Dataset [L55]
  └─ uses_service IControlledRepository<Dataset>
    └─ method ReadQuery [L55]
  └─ sends_request CanIAccessDatasetQuery [L54]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CanIAccessDatasetQueryHandler.Handle [L58–L140]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L127]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L101]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L129]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L88]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L103]
      └─ uses_cache IDistributedCache [L116]
        └─ method SetRecordAsync [write] [L116]
      └─ uses_cache IDistributedCache [L106]
        └─ method DoesRecordExistAsync [access] [L106]
      └─ uses_cache IDistributedCache [L103]
        └─ method CreateAccessKey [write] [L103]
      └─ uses_cache IDistributedCache [L90]
        └─ method DoesRecordExistAsync [access] [L90]
      └─ uses_cache IDistributedCache [L88]
        └─ method CreateDatasetLockKey [write] [L88]

