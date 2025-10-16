[web] DELETE /api/standard-accounts/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.Delete)  [L207–L216] status=204 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request DeleteStandardAccountCommand -> DeleteStandardAccountCommandHandler [L213]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.StandardAccounts.DeleteStandardAccountCommandHandler.Handle [L25–L51]
      └─ calls StandardAccountRepository (methods: ReadQuery,Remove,WriteQuery) [L49]
  └─ impact_summary
    └─ requests 1
      └─ DeleteStandardAccountCommand
    └─ handlers 1
      └─ DeleteStandardAccountCommandHandler

