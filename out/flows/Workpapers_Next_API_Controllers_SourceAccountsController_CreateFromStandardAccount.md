[web] POST /api/source-accounts/from-standard-account  (Workpapers.Next.API.Controllers.SourceAccountsController.CreateFromStandardAccount)  [L159–L168] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request CreateSourceAccountFromStandardAccountCommand -> CreateSourceAccountFromStandardAccountCommandHandler [L164]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.SourceAccounts.CreateSourceAccountFromStandardAccountCommandHandler.Handle [L34–L104]
      └─ calls StandardAccountRepository.ReadQuery [L93]
      └─ calls SourceAccountRepository (methods: Add,WriteQuery) [L81]
      └─ calls StandardAccountRepository.ReadQuery [L67]
      └─ calls ClientRepository.ReadQuery [L63]
      └─ calls SourceRepository.ReadQuery [L62]
      └─ calls SourceAccountRepository.WriteQuery [L61]
      └─ uses_client ClientRepository [L63]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L65]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L64]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
  └─ impact_summary
    └─ clients 1
      └─ ClientRepository
    └─ requests 1
      └─ CreateSourceAccountFromStandardAccountCommand
    └─ handlers 1
      └─ CreateSourceAccountFromStandardAccountCommandHandler

