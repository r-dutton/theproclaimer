[web] DELETE /api/source-accounts/{id:Guid}  (Workpapers.Next.API.Controllers.SourceAccountsController.Delete)  [L302–L310] [auth=AuthorizationPolicies.User]
  └─ calls SourceAccountRepository.ReadQuery [L305]
  └─ queries SourceAccount [L305]
    └─ reads_from SourceAccounts
  └─ uses_service IControlledRepository<SourceAccount>
    └─ method ReadQuery [L305]
  └─ sends_request PurgeUnusedSourceAccountsCommand [L307]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.PurgeUnusedSourceAccountsCommandHandler.Handle [L36–L161]
      └─ uses_service IControlledRepository<Account>
        └─ method WriteQuery [L91]
      └─ uses_service IControlledRepository<AssetGroup>
        └─ method ReadQuery [L148]
      └─ uses_service IControlledRepository<Distribution>
        └─ method ReadQuery [L130]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L68]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L62]

