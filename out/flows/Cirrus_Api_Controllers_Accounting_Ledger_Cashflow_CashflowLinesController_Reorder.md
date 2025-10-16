[web] PUT /api/accounting/ledger/cashflow/lines/{id}/reorder  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowLinesController.Reorder)  [L87–L114] status=200 [auth=Authentication.UserPolicy]
  └─ calls CashflowLineRepository.ReadQuery [L102]
  └─ calls CashflowLineRepository.WriteQuery [L90]
  └─ query CashflowLine [L102]
    └─ reads_from CashflowLines
  └─ write CashflowLine [L90]
    └─ reads_from CashflowLines
  └─ uses_service IControlledRepository<CashflowLine>
    └─ method WriteQuery [L90]
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

