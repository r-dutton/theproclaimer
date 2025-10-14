[web] GET /api/accounting/assets/depreciation-years/  (Cirrus.Api.Controllers.Accounting.Assets.DepreciationYearController.GetAll)  [L54–L66] [auth=user]
  └─ maps_to DepreciationYearDto [L61]
    └─ automapper.registration CirrusMappingProfile (DepreciationYear->DepreciationYearDto) [L871]
  └─ calls DepreciationYearRepository.ReadQuery [L61]
  └─ queries DepreciationYear [L61]
    └─ reads_from DepreciationYears
  └─ uses_service IControlledRepository<DepreciationYear>
    └─ method ReadQuery [L61]
  └─ sends_request CanIAccessFileQuery [L60]
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

