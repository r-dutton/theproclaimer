[web] DELETE /api/accounting/assets/depreciation-years/{id}  (Cirrus.Api.Controllers.Accounting.Assets.DepreciationYearController.Delete)  [L107–L126] [auth=user]
  └─ calls DepreciationYearRepository.Remove [L123]
  └─ calls DepreciationYearRepository.ReadQuery [L116]
  └─ calls DepreciationYearRepository.WriteQuery [L110]
  └─ queries DepreciationYear [L116]
    └─ reads_from DepreciationYears
  └─ writes_to DepreciationYear [L123]
    └─ reads_from DepreciationYears
  └─ writes_to DepreciationYear [L110]
    └─ reads_from DepreciationYears
  └─ uses_service IControlledRepository<DepreciationYear>
    └─ method WriteQuery [L110]
  └─ sends_request CanIAccessFileQuery [L114]
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

