[web] PUT /api/import-runs/reinstate  (Workpapers.Next.API.Controllers.Workpapers.ImportRunController.Reinstate)  [L111–L115] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request ReinstateImportRunCommand [L114]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.ImportRuns.ReinstateImportRunCommandHandler.Handle [L25–L72]
      └─ uses_service IControlledRepository<Binder>
        └─ method WriteQuery [L58]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<ImportRun>
        └─ method WriteQuery [L43]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L61]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

