[web] POST /api/accounting/ledger/standard-accounts/header/accept-recommendation  (Cirrus.Api.Controllers.Accounting.Ledger.StandardAccountController.ImplementRecommendedHeader)  [L184–L189] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request CreateHeaderFromStandardHeaderCommand -> CreateHeaderFromStandardHeaderCommandHandler [L187]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.CreateHeaderFromStandardHeaderCommandHandler.Handle [L48–L113]
      └─ uses_service IControlledRepository<StandardAccount> (Scoped (inferred))
        └─ method ReadQuery [L79]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardAccountRepository.ReadQuery
      └─ uses_service IControlledRepository<File> (Scoped (inferred))
        └─ method ReadQuery [L74]
          └─ implementation Cirrus.Data.Repository.Accounting.FileRepository.ReadQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L72]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method WriteQuery [L68]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ CreateHeaderFromStandardHeaderCommand
    └─ handlers 1
      └─ CreateHeaderFromStandardHeaderCommandHandler

