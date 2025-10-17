[web] PUT /api/standard-accounts/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.UpdateStandardAccount)  [L150–L157] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request UpdateStandardAccountCommand -> UpdateStandardAccountCommandHandler [L154]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.StandardAccounts.UpdateStandardAccountCommandHandler.Handle [L27–L49]
      └─ calls MasterAccountRepository.ReadQuery [L43]
      └─ calls StandardAccountRepository (methods: ReadQuery,WriteQuery) [L42]
  └─ impact_summary
    └─ requests 1
      └─ UpdateStandardAccountCommand
    └─ handlers 1
      └─ UpdateStandardAccountCommandHandler

