[web] POST /api/accounting/ledger/source-accounts/  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.Create)  [L154–L161] [auth=user]
  └─ calls SourceRepository.WriteQuery [L157]
  └─ writes_to Source [L157]
  └─ uses_service IControlledRepository<Source>
    └─ method WriteQuery [L157]
  └─ sends_request CreateSourceAccountsCommand [L159]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.CreateSourceAccountsCommandHandler.Handle [L52–L355]
      └─ calls SourceAccountsCachedRepository.ReadQuery [L338]
      └─ calls SourceAccountsCachedRepository.Add [L316]
      └─ calls SourceAccountsCachedRepository.Add [L255]
      └─ calls SourceAccountsCachedRepository.ReadQuery [L224]
      └─ calls SourceAccountsCachedRepository.Add [L198]
      └─ calls SourceAccountsCachedRepository.Add [L117]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L105]
      └─ uses_service IControlledRepository<Dataset>
        └─ method WriteQuery [L84]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method WriteQuery [L160]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L158]
  └─ sends_request CanIAccessFileQuery [L158]
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

