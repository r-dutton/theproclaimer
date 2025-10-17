[web] DELETE /api/source-accounts/{id:Guid}  (Workpapers.Next.API.Controllers.SourceAccountsController.Delete)  [L302–L310] status=200 [auth=AuthorizationPolicies.User]
  └─ calls SourceAccountRepository.ReadQuery [L305]
  └─ query SourceAccount [L305]
    └─ reads_from SourceAccounts
  └─ uses_service SourceAccountRepository
    └─ method ReadQuery [L305]
      └─ implementation Workpapers.Next.Data.Repository.Workpapers.SourceAccountRepository.ReadQuery [L8-L38]
  └─ sends_request PurgeUnusedSourceAccountsCommand -> PurgeUnusedSourceAccountsCommandHandler [L307]
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
    └─ entities 1 (writes=0, reads=1)
      └─ SourceAccount writes=0 reads=1
    └─ services 1
      └─ SourceAccountRepository
    └─ requests 1
      └─ PurgeUnusedSourceAccountsCommand
    └─ handlers 1
      └─ PurgeUnusedSourceAccountsCommandHandler

