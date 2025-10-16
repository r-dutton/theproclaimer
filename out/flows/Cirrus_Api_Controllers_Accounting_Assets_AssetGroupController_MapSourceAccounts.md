[web] GET /api/accounting/assets/asset-groups  (Cirrus.Api.Controllers.Accounting.Assets.AssetGroupController.MapSourceAccounts)  [L152–L172] status=200 [auth=user]
  └─ sends_request MapSourceAccountsCommand [L170]
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

