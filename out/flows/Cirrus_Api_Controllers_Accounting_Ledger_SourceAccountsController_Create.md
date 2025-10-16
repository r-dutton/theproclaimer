[web] POST /api/accounting/ledger/source-accounts/  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.Create)  [L154–L161] status=201 [auth=user]
  └─ calls SourceRepository.WriteQuery [L157]
  └─ write Source [L157]
  └─ sends_request CreateSourceAccountsCommand -> CreateSourceAccountsCommandHandler [L159]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.CreateSourceAccountsCommandHandler.Handle [L52–L355]
      └─ calls SourceAccountsCachedRepository (methods: ReadQuery,Add) [L338]
      └─ uses_service IControlledRepository<StandardAccount> (Scoped (inferred))
        └─ method WriteQuery [L160]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardAccountRepository.WriteQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L158]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method ReadQuery [L105]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method WriteQuery [L84]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.WriteQuery
  └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L158]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L68]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Source writes=1 reads=0
    └─ requests 2
      └─ CanIAccessFileQuery
      └─ CreateSourceAccountsCommand
    └─ handlers 2
      └─ CanIAccessFileQueryHandler
      └─ CreateSourceAccountsCommandHandler
    └─ caches 1
      └─ IMemoryCache

