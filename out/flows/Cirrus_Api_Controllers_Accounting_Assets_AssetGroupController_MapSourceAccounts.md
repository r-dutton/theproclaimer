[web] GET /api/accounting/assets/asset-groups  (Cirrus.Api.Controllers.Accounting.Assets.AssetGroupController.MapSourceAccounts)  [L152–L172] [auth=user]
  └─ sends_request MapSourceAccountsCommand [L170]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.MapSourceAccountsCommandHandler.Handle [L52–L259]
      └─ calls SourceAccountsCachedRepository.AddTrackedRange [L92]
      └─ calls SourceAccountsCachedRepository.InDatabaseOnly [L80]
      └─ calls SourceAccountsCachedRepository.InMemoryOnly [L72]
      └─ uses_service IControlledRepository<Source>
        └─ method WriteQuery [L151]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L123]

