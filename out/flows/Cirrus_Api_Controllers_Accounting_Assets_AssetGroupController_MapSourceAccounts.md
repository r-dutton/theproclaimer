[web] GET /api/accounting/assets/asset-groups  (Cirrus.Api.Controllers.Accounting.Assets.AssetGroupController.MapSourceAccounts)  [L152–L172] status=200 [auth=user]
  └─ sends_request MapSourceAccountsCommand -> MapSourceAccountsCommandHandler [L170]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.MapSourceAccountsCommandHandler.Handle [L52–L259]
      └─ calls SourceAccountsCachedRepository (methods: AddTrackedRange,InDatabaseOnly,InMemoryOnly) [L92]
      └─ uses_service IControlledRepository<Source> (Scoped (inferred))
        └─ method WriteQuery [L151]
          └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceRepository.WriteQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L123]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ impact_summary
    └─ requests 1
      └─ MapSourceAccountsCommand
    └─ handlers 1
      └─ MapSourceAccountsCommandHandler

