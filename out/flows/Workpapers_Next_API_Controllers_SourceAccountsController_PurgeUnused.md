[web] DELETE /api/source-accounts/purge-unused  (Workpapers.Next.API.Controllers.SourceAccountsController.PurgeUnused)  [L315–L320] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request PurgeUnusedSourceAccountsCommand -> PurgeUnusedSourceAccountsCommandHandler [L318]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.PurgeUnusedSourceAccountsCommandHandler.Handle [L36–L161]
      └─ uses_service IControlledRepository<AssetGroup> (Scoped (inferred))
        └─ method ReadQuery [L148]
          └─ implementation Cirrus.Data.Repository.Accounting.Assets.AssetGroupRepository.ReadQuery
      └─ uses_service IControlledRepository<Distribution> (Scoped (inferred))
        └─ method ReadQuery [L130]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AutoJournals.DistributionRepository.ReadQuery
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method WriteQuery [L91]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.WriteQuery
      └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
        └─ method ReadQuery [L68]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L62]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ impact_summary
    └─ requests 1
      └─ PurgeUnusedSourceAccountsCommand
    └─ handlers 1
      └─ PurgeUnusedSourceAccountsCommandHandler

