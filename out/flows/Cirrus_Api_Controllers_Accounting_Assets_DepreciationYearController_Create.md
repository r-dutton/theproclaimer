[web] POST /api/accounting/assets/depreciation-years/  (Cirrus.Api.Controllers.Accounting.Assets.DepreciationYearController.Create)  [L72–L87] status=201 [auth=user]
  └─ calls DepreciationYearRepository.Add [L84]
  └─ calls DepreciationYearRepository.ReadQuery [L77]
  └─ insert DepreciationYear [L84]
    └─ reads_from DepreciationYears
  └─ query DepreciationYear [L77]
    └─ reads_from DepreciationYears
  └─ uses_service IControlledRepository<DepreciationYear>
    └─ method ReadQuery [L77]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L75]
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

