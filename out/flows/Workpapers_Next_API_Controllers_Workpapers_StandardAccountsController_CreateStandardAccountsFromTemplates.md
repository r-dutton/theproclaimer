[web] POST /api/standard-accounts/create-from-template  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.CreateStandardAccountsFromTemplates)  [L132–L141] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request BulkCreateStandardAccountsCommand -> BulkCreateStandardAccountsCommandHandler [L138]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.StandardAccounts.BulkCreateStandardAccountsCommandHandler.Handle [L27–L105]
      └─ calls StandardAccountRepository.Add [L99]
      └─ calls MasterAccountRepository.WriteQuery [L53]
      └─ calls StandardAccountRepository.WriteQuery [L41]
  └─ impact_summary
    └─ requests 1
      └─ BulkCreateStandardAccountsCommand
    └─ handlers 1
      └─ BulkCreateStandardAccountsCommandHandler

