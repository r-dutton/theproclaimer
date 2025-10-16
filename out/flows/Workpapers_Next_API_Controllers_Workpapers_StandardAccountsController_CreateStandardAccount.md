[web] POST /api/standard-accounts  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.CreateStandardAccount)  [L115–L124] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request CreateStandardAccountCommand -> CreateStandardAccountCommandHandler [L121]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.StandardAccounts.CreateStandardAccountCommandHandler.Handle [L26–L71]
      └─ calls StandardAccountRepository.WriteQuery [L64]
      └─ calls MasterAccountRepository.WriteQuery [L57]
      └─ calls StandardAccountRepository (methods: WriteQuery,Add) [L50]
  └─ impact_summary
    └─ requests 1
      └─ CreateStandardAccountCommand
    └─ handlers 1
      └─ CreateStandardAccountCommandHandler

