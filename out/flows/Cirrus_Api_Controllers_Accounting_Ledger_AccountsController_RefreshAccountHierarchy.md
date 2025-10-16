[web] GET /api/accounting/ledger/accounts/{fileId:Guid}/hierarchy/refresh  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.RefreshAccountHierarchy)  [L212–L247] status=200 [auth=user]
  └─ calls FileRepository.ReadQuery [L218]
  └─ calls AccountRepository.ReadQuery [L224]
  └─ query File [L218]
    └─ reads_from Files
  └─ query Account [L224]
  └─ uses_service IControlledRepository<Account>
    └─ method ReadQuery [L224]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<File>
    └─ method ReadQuery [L218]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L215]
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

