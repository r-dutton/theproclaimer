[web] PUT /api/accounting/ledger/cashflow/categories/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowCategoriesController.Update)  [L104–L115] status=200 [auth=Authentication.UserPolicy]
  └─ calls CashflowCategoryRepository.WriteQuery [L107]
  └─ calls CashflowLineRepository.WriteQuery [L109]
  └─ write CashflowCategory [L107]
    └─ reads_from CashflowCategories
  └─ write CashflowLine [L109]
    └─ reads_from CashflowLines
  └─ uses_service IControlledRepository<CashflowCategory>
    └─ method WriteQuery [L107]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<CashflowLine>
    └─ method WriteQuery [L109]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L108]
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

