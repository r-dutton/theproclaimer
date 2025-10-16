[web] POST /api/accounting/ledger/source-accounts/  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.Create)  [L154–L161] status=201 [auth=user]
  └─ calls SourceRepository.WriteQuery [L157]
  └─ write Source [L157]
  └─ uses_service IControlledRepository<Source>
    └─ method WriteQuery [L157]
      └─ ... (no implementation details available)
  └─ sends_request CreateSourceAccountsCommand [L159]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.CreateSourceAccountsCommandHandler.Handle [L52–L355]
      └─ calls SourceAccountsCachedRepository.ReadQuery [L338]
      └─ calls SourceAccountsCachedRepository.Add [L316]
      └─ calls SourceAccountsCachedRepository.Add [L255]
      └─ calls SourceAccountsCachedRepository.ReadQuery [L224]
      └─ calls SourceAccountsCachedRepository.Add [L198]
      └─ calls SourceAccountsCachedRepository.Add [L117]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L105]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Dataset>
        └─ method WriteQuery [L84]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method WriteQuery [L160]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L158]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L158]
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

