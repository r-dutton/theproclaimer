[web] DELETE /api/accounting/ledger/cashflow/categories/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowCategoriesController.Delete)  [L134–L140] status=200 [auth=Authentication.UserPolicy]
  └─ calls CashflowCategoryRepository.Remove [L139]
  └─ calls CashflowCategoryRepository.WriteQuery [L137]
  └─ delete CashflowCategory [L139]
    └─ reads_from CashflowCategories
  └─ write CashflowCategory [L137]
    └─ reads_from CashflowCategories
  └─ uses_service IControlledRepository<CashflowCategory>
    └─ method WriteQuery [L137]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L138]
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

