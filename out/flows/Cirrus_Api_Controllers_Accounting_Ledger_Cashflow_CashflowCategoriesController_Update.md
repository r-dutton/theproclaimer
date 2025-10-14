[web] PUT /api/accounting/ledger/cashflow/categories/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowCategoriesController.Update)  [L104–L115] [auth=Authentication.UserPolicy]
  └─ calls CashflowCategoryRepository.WriteQuery [L107]
  └─ calls CashflowLineRepository.WriteQuery [L109]
  └─ writes_to CashflowCategory [L107]
    └─ reads_from CashflowCategories
  └─ writes_to CashflowLine [L109]
    └─ reads_from CashflowLines
  └─ uses_service IControlledRepository<CashflowCategory>
    └─ method WriteQuery [L107]
  └─ uses_service IControlledRepository<CashflowLine>
    └─ method WriteQuery [L109]
  └─ sends_request CanIAccessFileQuery [L108]
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

