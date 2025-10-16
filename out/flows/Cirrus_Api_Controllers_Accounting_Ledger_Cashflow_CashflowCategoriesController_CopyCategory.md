[web] POST /api/accounting/ledger/cashflow/categories/{id}/copy  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowCategoriesController.CopyCategory)  [L86–L102] status=201 [auth=Authentication.UserPolicy]
  └─ calls CashflowCategoryRepository.Add [L101]
  └─ calls CashflowCategoryRepository.WriteQuery [L89]
  └─ insert CashflowCategory [L101]
    └─ reads_from CashflowCategories
  └─ write CashflowCategory [L89]
    └─ reads_from CashflowCategories
  └─ uses_service IControlledRepository<CashflowCategory>
    └─ method WriteQuery [L89]
      └─ ... (no implementation details available)
  └─ uses_service IReadRepository (InstancePerLifetimeScope)
    └─ method Table [L95]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L93]
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

