[web] DELETE /api/accounting/assets/depreciation-years/{id}  (Cirrus.Api.Controllers.Accounting.Assets.DepreciationYearController.Delete)  [L107–L126] status=200 [auth=user]
  └─ calls DepreciationYearRepository.Remove [L123]
  └─ calls DepreciationYearRepository.ReadQuery [L116]
  └─ calls DepreciationYearRepository.WriteQuery [L110]
  └─ delete DepreciationYear [L123]
    └─ reads_from DepreciationYears
  └─ query DepreciationYear [L116]
    └─ reads_from DepreciationYears
  └─ write DepreciationYear [L110]
    └─ reads_from DepreciationYears
  └─ uses_service IControlledRepository<DepreciationYear>
    └─ method WriteQuery [L110]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L114]
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

