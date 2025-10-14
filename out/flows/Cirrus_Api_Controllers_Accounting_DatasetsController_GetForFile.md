[web] GET /api/accounting/datasets/forfile/{fileId:Guid}  (Cirrus.Api.Controllers.Accounting.DatasetsController.GetForFile)  [L90–L112] [auth=user]
  └─ maps_to DatasetLightDto [L100]
    └─ automapper.registration CirrusMappingProfile (Dataset->DatasetLightDto) [L204]
  └─ calls DatasetRepository.ReadQuery [L100]
  └─ queries Dataset [L100]
  └─ uses_service IControlledRepository<Dataset>
    └─ method ReadQuery [L100]
  └─ sends_request CanIAccessFileQuery [L99]
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

