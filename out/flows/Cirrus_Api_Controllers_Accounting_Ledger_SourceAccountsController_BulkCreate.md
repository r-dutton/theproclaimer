[web] POST /api/accounting/ledger/source-accounts/bulk  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.BulkCreate)  [L145–L152] status=201 [auth=user]
  └─ calls SourceRepository.WriteQuery [L148]
  └─ write Source [L148]
  └─ uses_service IControlledRepository<Source>
    └─ method WriteQuery [L148]
      └─ ... (no implementation details available)
  └─ sends_request MapSourceAccountsCommand [L150]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.MapSourceAccountsCommandHandler.Handle [L52–L259]
      └─ calls SourceAccountsCachedRepository.AddTrackedRange [L92]
      └─ calls SourceAccountsCachedRepository.InDatabaseOnly [L80]
      └─ calls SourceAccountsCachedRepository.InMemoryOnly [L72]
      └─ uses_service IControlledRepository<Source>
        └─ method WriteQuery [L151]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L123]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L149]
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

