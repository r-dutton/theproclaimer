[web] GET /api/accounting/datasets/forfile/{fileId:Guid}  (Cirrus.Api.Controllers.Accounting.DatasetsController.GetForFile)  [L90–L112] status=200 [auth=user]
  └─ maps_to DatasetLightDto [L100]
    └─ automapper.registration CirrusMappingProfile (Dataset->DatasetLightDto) [L204]
  └─ calls DatasetRepository.ReadQuery [L100]
  └─ query Dataset [L100]
  └─ uses_service IControlledRepository<Dataset>
    └─ method ReadQuery [L100]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L99]
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

