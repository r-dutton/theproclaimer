[web] PUT /api/accounting/ledger/cashflow/reconciliation-lines/{id}/reorder  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowReconciliationLinesController.Reorder)  [L87–L110] [auth=Authentication.UserPolicy]
  └─ calls CashflowReconciliationLineRepository.ReadQuery [L102]
  └─ calls CashflowReconciliationLineRepository.WriteQuery [L90]
  └─ queries CashflowReconciliationLine [L102]
    └─ reads_from CashflowReconciliationLines
  └─ writes_to CashflowReconciliationLine [L90]
    └─ reads_from CashflowReconciliationLines
  └─ uses_service IControlledRepository<CashflowReconciliationLine>
    └─ method WriteQuery [L90]
  └─ uses_service IReadRepository (InstancePerLifetimeScope)
    └─ method Table [L95]
  └─ sends_request CanIAccessFileQuery [L93]
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

