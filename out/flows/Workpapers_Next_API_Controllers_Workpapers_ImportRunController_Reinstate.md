[web] PUT /api/import-runs/reinstate  (Workpapers.Next.API.Controllers.Workpapers.ImportRunController.Reinstate)  [L111–L115] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request ReinstateImportRunCommand -> ReinstateImportRunCommandHandler [L114]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.ImportRuns.ReinstateImportRunCommandHandler.Handle [L25–L72]
      └─ calls ImportRunRepository.WriteQuery [L49]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L61]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method WriteQuery [L58]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ ReinstateImportRunCommand
    └─ handlers 1
      └─ ReinstateImportRunCommandHandler

