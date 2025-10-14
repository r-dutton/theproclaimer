[web] POST /api/source-accounts/standard-chart/for-binder/{binderId:guid}  (Workpapers.Next.API.Controllers.SourceAccountsController.BuildStandardChartForBinder)  [L231–L245] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L236]
  └─ queries Binder [L236]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L236]
  └─ sends_request CanIAccessBinderQuery [L234]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L101]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
      └─ uses_service UserService
        └─ method GetUserId [L91]
      └─ uses_cache IDistributedCache [L121]
        └─ method SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache [L109]
        └─ method DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache [L92]
        └─ method CreateAccessKey [write] [L92]
  └─ sends_request CreateHeaderSourceAccountsCommand [L244]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Connections.Common.Commands.SourceAccounts.CreateHeaderSourceAccountsCommandHandler.Handle [L32–L256]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L77]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L72]
      └─ uses_service IControlledRepository<MasterAccount>
        └─ method ReadQuery [L213]
      └─ uses_service IControlledRepository<Source>
        └─ method ReadQuery [L67]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method WriteQuery [L59]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L186]

